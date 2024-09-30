using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VerzamelWoede.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        // Add a list of categories for many-to-many relationship
        public List<Collection> Collections { get; set; } = new List<Collection>();

        //navigatie property naar de collection
        public ICollection<Item>? Items { get; set; }


        public void Create() { }
        public void Update() { }
        public void Delete() { }
        public void Read() { }
    }
}
