using SPX.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPX.ViewModels
{
    public class BucketsDetailsViewModel
    {
        public List<Bucket> Buckets { get; set; }
        public List<Team> Teams { get; set; }
    }
}
