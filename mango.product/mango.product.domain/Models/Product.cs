using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mango.product.domain.Models
{
    public class Product
    {
        private int id;

        internal Product(ProductBuilder builder)
        {
            Name = builder.Name;
            Price = builder.Price;
            ProductId = builder.ProductId;
            Description = builder.Description;
            CategoryName = builder.CategoryName;
            ImageUrl = builder.ImageUrl;

        }


        public int ProductId { get; private set; }
        public string Name { get; private set; }
        public double Price { get; private set; }
        public string Description { get; private set; }
        public string CategoryName { get; private set; }
        public string ImageUrl { get; private set; }

        public IBuilder ProductBuilder { get;private set;  }
    }

    internal class ProductBuilder: IBuilder
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

    internal interface IBuilder
    {
    }
}


