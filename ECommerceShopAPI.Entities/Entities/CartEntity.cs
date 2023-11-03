using System;
using System.Collections.Generic;

namespace ECommerceShopAPI.Entities.Entities;

public partial class CartEntity
{
    public int CartId { get; set; }

    public int? CustomerId { get; set; }

    public int? ProductId { get; set; } 

    public int? Quantity { get; set; }

    public DateTime? CreatedDate { get; set; }

    public bool? IsActive { get; set; }

}
