using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceShopAPI.Entities.DTO
{
    public class CartDto
    {
        public int CartId { get; set; }

        public int? CustomerId { get; set; }

        public int? ProductId { get; set; }

        public int? Quantity { get; set; }

        public DateTime? CreatedDate { get; set; }

        public bool? IsActive { get; set; }
    }
}
