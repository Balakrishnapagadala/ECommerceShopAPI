using ECommerceShopAPI.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;  
using System.Threading.Tasks;

namespace ECommerceShopAPI.Entities.DTO
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        public string? Name { get; set; }
        public int ProductType { get; set; }
        public int Quantity { get; set; }
        public decimal? Price { get; set; }
        public bool? IsActive { get; set; }
    }
}
