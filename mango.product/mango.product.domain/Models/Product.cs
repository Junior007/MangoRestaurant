using mango.product.domain.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mango.product.domain.Models
{
    public class Product
    {

        internal Product(ProductBuilder builder)
        {
            Name = builder.Name;
            Price = builder.Price;
            ProductId = builder.ProductId;
            Description = builder.Description;
            CategoryName = builder.CategoryName;
            ImageUrl = builder.ImageUrl;
            RowVersion = builder.RowVersion;

        }


        public int ProductId { get; private set; }
        public string Name { get; private set; }
        public double Price { get; private set; }
        public string Description { get; private set; }
        public string CategoryName { get; private set; }
        public string ImageUrl { get; private set; }
        public byte[] RowVersion { get; private set; }

    }
}


