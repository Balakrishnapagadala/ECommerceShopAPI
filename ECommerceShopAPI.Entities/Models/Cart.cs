using System;
using System.Collections.Generic;

namespace ECommerceShopAPI.Entities.Models;

public partial class Cart
{
    public int CartId { get; set; }

    public int? CustomerId { get; set; }

    public int? ProductId { get; set; } 

    public int? Quantity { get; set; }

    public DateTime? CreatedDate { get; set; }

    public bool? IsActive { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Product? Product { get; set; }
}
