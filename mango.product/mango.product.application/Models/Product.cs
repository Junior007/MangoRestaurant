using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mango.product.application.Models
{
    public class Product
    {
        public int ProductId { get;  set; }
        public string Name { get;  set; }
        public double Price { get;  set; }
        public string Description { get;  set; }
        public string CategoryName { get;  set; }
        public string ImageUrl { get;  set; }
        public byte[] RowVersion { get; set; }

    }
}
