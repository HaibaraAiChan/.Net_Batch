using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    [Table("Review") ]
    public class Review
    {
        public int MovieId { get; set; } // # foreign key
        public int UserId { get; set; } // # foreign key
        [Column(TypeName = "decimal(3,2)")]
        public decimal Rating { get; set; }
        public string ReviewText { get; set; }= string.Empty;


    }
}
