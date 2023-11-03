using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceShopAPI.Entities.Models;

namespace ECommerceShopAPI.Entities.DTO
{
    public class PurchaseOrderDto
    {
        public int CustomerId { get; set; }

        public int OrderId { get; set; }

        public decimal? TotalAmount { get; set; }

        public ICollection<ProductDto> OrderItems { get; set; }
    }
}
