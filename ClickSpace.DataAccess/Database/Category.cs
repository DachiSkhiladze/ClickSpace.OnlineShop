using ClickSpace.DataAccess.DB.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickSpace.DataAccess.Database
{
    public partial class Category
    {
        public Category()
        {
            Product = new HashSet<Product>();
        }

        public string Id { get; set; }
        public string Title { get; set; }
        public string PictureUrl { get; set; }
        public virtual ICollection<Product> Product { get; set; }
    }
}
