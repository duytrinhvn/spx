using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SPX.Models
{
    public class BucketTeamConnection
    {
        public Guid Id { get; set; }
        public Bucket Bucket { get; set; }
        public Team Team { get; set; }
        [ForeignKey("Team")]
        public Guid TeamFK { get; set; }
        [ForeignKey("Bucket")]
        public Guid BucketFK { get; set; }
    }
}
