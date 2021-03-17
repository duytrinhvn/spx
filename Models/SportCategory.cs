using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SPX.Models
{
    public class SportCategory
    {
        [Key]
        public Guid Id { get; set; }
        public String Name { get; set; }
        public League[] Leagues { get; set; }
    }
}
