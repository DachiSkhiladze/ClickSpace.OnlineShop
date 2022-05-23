using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickSpace.OnlineShop.BAL.Models
{
    public class CartProductModel
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public string UserId { get; set; }
        public long Quantity { get; set; }
    }
}
