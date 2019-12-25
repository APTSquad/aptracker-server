using System.Collections.Generic;
using APTracker.Server.WebApi.Commands.Hierarchy.Views;

namespace APTracker.Server.WebApi.Commands.Hierarchy
{
    public class GetHierarchyResponse
    {
        public ICollection<ClientView> Clients { get; set; }
    }
}