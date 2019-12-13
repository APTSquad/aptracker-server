using System.Collections;
using System.Collections.Generic;
using APTracker.Server.WebApi.ViewModels.Commands.Hierarchy.Views;

namespace APTracker.Server.WebApi.ViewModels.Commands.Hierarchy
{
    public class GetHierarchyResponse
    {
        public ICollection<ClientView> Clients { get; set; }
    }
}