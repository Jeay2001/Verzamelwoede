using VerzamelWoede.Models;

namespace Verzamelwoede.Models
{
    public class ItemViewModel
    {

        public Item? Item { get; set; }
        // Add a property for total price calculation
        public decimal TotalPrice => Item.Price* Item.Quantity;
    }
}
