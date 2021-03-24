using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SPX.Models
{
    public class BucketTeamConnection
    {
        [Key]
        public Guid Id { get; set; }
        public Bucket Bucket { get; set; }
        public Team Team { get; set; }
    }
}
