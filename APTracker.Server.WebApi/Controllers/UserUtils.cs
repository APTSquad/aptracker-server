using System.Security.Claims;
using System.Threading.Tasks;
using APTracker.Server.WebApi.Persistence;
using APTracker.Server.WebApi.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace APTracker.Server.WebApi.Controllers
{
    public class UserUtils
    {
        public static string GetUserEmail(ClaimsPrincipal user)
        {
            return user.FindFirst("preferred_username")?.Value;
        }

        public static  string GetUserName(ClaimsPrincipal user)
        {
            return user.FindFirst("name")?.Value;
        }

        public static async Task<User> GetUser(AppDbContext context, ClaimsPrincipal user)
        {
            var foundUser = await context.Users.FirstOrDefaultAsync(u => u.Email == GetUserEmail(user));
            return foundUser;
        }
    }
}