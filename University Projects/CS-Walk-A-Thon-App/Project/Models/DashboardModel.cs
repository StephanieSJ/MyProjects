namespace Project.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Collections.Generic;

    public class DashboardModel
    {

        public List<Achievement> achievements { get; set; }
        public List<RouteLeaderboard> leaderboard { get; set; }
        public double distance { get; set; }
        public PointOfInterest currentPOI { get; set; }
        public Route currentRoute { get; set; }

    }
}