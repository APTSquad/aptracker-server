using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APTracker.Server.WebApi.Models
{
    public class Employee
    {
        public int id { set; get; }

        public string name { set; get; }

        public bool responsable { set; get; }

        public string position { set; get; }

        public Bags bags { set; get; }
    }
}
