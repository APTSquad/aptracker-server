﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APTracker.Server.WebApi.Models
{
    public class Bags
    {
        public int id { set; get; }
        
        public string name { set; get; }

        public Projects projects { set; get; }


    }
}
