using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VerzamelWoede.Models
{
    public class Item
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        [Column(TypeName = "decimal(6, 2)")]
        public decimal Price { get; set; }
        public string Worth { get; set; } = null!;
        public int YearOfPurchase { get; set; }
        public string Condition { get; set; } = null!;
        public string Origin { get; set; } = null!;
        public bool IsSold { get; set; }
        public bool IsLost { get; set; }
        public int CategoryId { get; set; }
        public virtual Category? Category { get; set; } = null!;

        public void CalculatePriceDifference(){}

        public void MarkAsLost(){}

        public void MarkAsSold(){}

        public void Create() { }
        public void Update() { }
        public void Delete() { }
        public void Read() { }



    }
    
}
