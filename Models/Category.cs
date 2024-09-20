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

        //foreign key naar de collection
        public int CollectionId { get; set; }

        public virtual Collection? Collection { get; set; } = null!;

        //navigatie property naar de collection
        public ICollection<Collection>? Collections { get; set; }
        public ICollection<Item>? Items { get; set; }
    }
}
