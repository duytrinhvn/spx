using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SPX.Models
{
    public class Offer
    {
        [Key]
        public Guid Id { get; set; }

        [Column(TypeName="decimal(18,2)")]
        [DataType(DataType.Currency), Range(1, 1000000)]
        public Decimal Price { get; set; }
        public String Description { get; set; }
        public Bucket Bucket { get; set; }
    }
}
