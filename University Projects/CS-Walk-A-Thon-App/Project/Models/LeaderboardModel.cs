using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Models
{
    public class LeaderboardModel
    {
        public List<User> users { get; set; }
        public List<RouteLeaderboard> leaderboard { get; set; }
    }
}