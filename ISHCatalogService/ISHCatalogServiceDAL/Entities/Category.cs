using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISHCatalogServiceDAL.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int? ParentCategoryId { get; set; }
        public Category ParentCategory { get; set; }
    }
}
