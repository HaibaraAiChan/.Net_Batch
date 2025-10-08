using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    [Table("Purchase")]
    public class Purchase
    {
        public int Id { get; set; } // # primary key
        public int UserId { get; set; } // # foreign key
        public int MovieId { get; set; } // # foreign key
        [MaxLength(450)]
        public string PurchaseNumber { get; set; } = Guid.NewGuid().ToString();
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }
        [Column(TypeName = "datetime2(7)")]
        public DateTime PurchaseDateTime { get; set; } = DateTime.UtcNow;
        
    }
}
