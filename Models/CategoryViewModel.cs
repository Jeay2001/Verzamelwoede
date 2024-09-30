using VerzamelWoede.Models;

namespace Verzamelwoede.Models
{
    public class CategoryViewModel
    {
        public List<Category> Categories { get; set; } = new List<Category>();
        public List<Collection> Collections { get; set; } = new List<Collection>();
        public List<int>? SelectedCollectionIds { get; set; } = new List<int>();
    }
}