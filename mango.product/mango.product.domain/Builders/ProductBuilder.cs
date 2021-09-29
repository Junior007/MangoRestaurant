using mango.product.domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mango.product.domain.Builders
{
    public class ProductBuilder
    {
        public string Name { get; private set; }
        public double Price { get; private set; }
        public string CategoryName { get; private set; }
        public string ImageUrl { get; private set; }
        public string Description { get; private set; }
        public int ProductId { get; private set; }

        public ProductBuilder SetName(string name)
        {
            Name = name;
            return this;
        }
        public ProductBuilder SetPrice(double price)
        {
            Price = price;
            return this;

        }

        public ProductBuilder SetProductId(int productId)
        {
            ProductId = productId;
            return this;
        }

        public ProductBuilder SetCategoryName(string categoryName)
        {
            CategoryName = categoryName;
            return this;
        }

        public ProductBuilder SetImageUrl(string imageUrl)
        {
            ImageUrl = imageUrl;
            return this;
        }

        public ProductBuilder SetDescription(string description)
        {
            Description = description;
            return this;
        }

        public Product Build()
        {
            return new Product(this);
        }

    }
}
