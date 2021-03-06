using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mango.product.data.Models
{

    public class Product : IEntity
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        public string Name { get; set; }
        [Range(1, 1000)]
        public double Price { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public string ImageUrl { get; set; }

        [ConcurrencyCheck]
        [Timestamp]
        public byte[] RowVersion { get; set; }

        public bool Equals(Product product)
        {
            return this.ProductId == product.ProductId;
        }


    }
}
