using SPX.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPX.ViewModels
{
    public class InvestmentsListViewModel
    {
        public IEnumerable<Interest> Interests { get; set; }
        public IEnumerable<Bucket> Buckets { get; set; }
    }
}
