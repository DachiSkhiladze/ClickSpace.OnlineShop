﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace ClickSpace.DataAccess.Database
{
    public partial class Product
    {
        public Product()
        {
            CartProduct = new HashSet<CartProduct>();
            ProductPicture = new HashSet<ProductPicture>();
        }

        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? PostDate { get; set; }
        public long? CategoryId { get; set; }

        public virtual ICollection<CartProduct> CartProduct { get; set; }
        public virtual ICollection<ProductPicture> ProductPicture { get; set; }
    }
}