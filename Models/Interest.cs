using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SPX.Models
{
    public class Interest
    {
        [Key]
        public Guid Id { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public Team Team { get; set; }

        [ForeignKey("ApplicationUser")]
        public String ApplicationUserFK { get; set; }

        [ForeignKey("Team")]
        public Guid TeamFK { get; set; }
    }
}
