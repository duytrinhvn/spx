using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SPX.Models
{
    public class League
    {
        [Key]
        public Guid Id { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public Team[] Teams;
    }
}
