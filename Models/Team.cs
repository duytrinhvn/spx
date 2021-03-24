using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SPX.Models
{
    public class Team
    {
        [Key]
        public Guid Id { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public SportCategory SportCategory { get; set; }
        public League League { get; set; }
    }
}
