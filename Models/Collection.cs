using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VerzamelWoede.Models
{
    public class Collection
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public ICollection<Category>? Categories { get; set; }


        public void Create() { }
        public void Update() { }
        public void Delete() { }
        public void Read() { }
    }
}
