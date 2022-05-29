using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickSpace.OnlineShop.BAL.Models
{
    public partial class PictureModel
    {
        public long ProductId { get; set; }
        public IFormFile[] files { get; set; }
    }
}
