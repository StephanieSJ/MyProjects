using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Models
{
    public class DistanceLogger
    {
        public List<DistanceLog> distanceLogs { get; set; }
        public DistanceLog newLog { get; set; }
        public bool alreadyLogged { get; set; }
        public Route currentRoute { get; set; }
    }
}