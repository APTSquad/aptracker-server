using System.Collections.Generic;
using System.Threading.Tasks;
using APTracker.Server.WebApi.Commands.Hierarchy.Views;
using APTracker.Server.WebApi.Persistence;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APTracker.Server.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class HierarchyController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public HierarchyController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ICollection<ClientView>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllHierarchy()
        {
            return Ok(_context.Clients.ProjectTo<ClientView>(_mapper.ConfigurationProvider));
        }
    }
}