using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Microsoft.Identity.Web;
using Newtonsoft.Json;

namespace APTracker.Server.WebApi.Controllers
{
    public class TodoItem
    {
        public string Owner { get; set; }
        public string Title { get; set; }
    }

    [Authorize]
    [Route("api/[controller]")]
    public class TodosController : Controller
    {
        private static readonly ConcurrentBag<TodoItem> TodoStore = new ConcurrentBag<TodoItem>();

        /// <summary>
        ///     The Web API will only accept tokens 1) for users, and
        ///     2) having the access_as_user scope for this API
        /// </summary>
        //private static readonly string[] ScopeRequiredByApi = {"access_as_user"};
        private readonly ITokenAcquisition _tokenAcquisition;

        public TodosController(ITokenAcquisition tokenAcquisition)
        {
            _tokenAcquisition = tokenAcquisition;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<TodoItem> Get()
        {
            //HttpContext.VerifyUserHasAnyAcceptedScope(ScopeRequiredByApi);
            var owner = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return TodoStore.Where(t => t.Owner == owner).ToList();
        }

        // POST api/values
        [HttpPost]
        public async void Post([FromBody] TodoItem todo)
        {
            //HttpContext.VerifyUserHasAnyAcceptedScope(ScopeRequiredByApi);
            var owner = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            string ownerName;
            // This is a synchronous call, so that the clients know, when they call Get, that the 
            // call to the downstream API (Microsoft Graph) has completed.
            try
            {
                ownerName = CallGraphApiOnBehalfOfUser().GetAwaiter().GetResult();
                var title = string.IsNullOrWhiteSpace(ownerName) ? todo.Title : $"{todo.Title} ({ownerName})";
                TodoStore.Add(new TodoItem {Owner = owner, Title = title});
            }
            catch (MsalException ex)
            {
                HttpContext.Response.ContentType = "text/plain";
                HttpContext.Response.StatusCode = (int) HttpStatusCode.Unauthorized;
                await HttpContext.Response.WriteAsync(
                    "An authentication error occurred while acquiring a token for downstream API\n" + ex.ErrorCode +
                    "\n" + ex.Message);
            }
            catch (Exception ex)
            {
                HttpContext.Response.ContentType = "text/plain";
                HttpContext.Response.StatusCode = (int) HttpStatusCode.BadRequest;
                await HttpContext.Response.WriteAsync("An error occurred while calling the downstream API\n" +
                                                      ex.Message);
            }
        }

        public async Task<string> CallGraphApiOnBehalfOfUser()
        {
            string[] scopes = {"user.read"};

            // we use MSAL.NET to get a token to call the API On Behalf Of the current user
            try
            {
                var accessToken = await _tokenAcquisition.GetAccessTokenOnBehalfOfUserAsync(scopes);
                var me = await CallGraphApiOnBehalfOfUser(accessToken);
                return me.userPrincipalName;
            }
            catch (MsalUiRequiredException ex)
            {
                _tokenAcquisition.ReplyForbiddenWithWwwAuthenticateHeader(scopes, ex);
                return string.Empty;
            }
        }

        private static async Task<dynamic> CallGraphApiOnBehalfOfUser(string accessToken)
        {
            // Call the Graph API and retrieve the user's profile.
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await client.GetAsync("https://graph.microsoft.com/v1.0/me");
            var content = await response.Content.ReadAsStringAsync();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                dynamic me = JsonConvert.DeserializeObject(content);
                return me;
            }

            throw new Exception(content);
        }
    }
}