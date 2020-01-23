using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Models
{
    public class RouteAndSection
    {
        public Route Route { get; set; }
        public IEnumerable<RouteSection> sections { get; set; }
    }
}