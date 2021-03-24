using SPX.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPX.ViewModels
{
    public class ControlInterestsViewModel
    {
        public IEnumerable<Team> Teams { get; set; }
        public IEnumerable<Interest> Interests { get; set; }
        public IEnumerable<League> Leagues { get; set; }
        public IEnumerable<Guid> InterestedTeamsIds { get; set; }
    }
}
