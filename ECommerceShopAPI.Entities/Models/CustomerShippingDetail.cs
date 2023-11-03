using System;
using System.Collections.Generic;

namespace ECommerceShopAPI.Entities.Models;

public partial class CustomerShippingDetail
{
    public int ShippingId { get; set; }

    public int? CustomerId { get; set; }

    public string? Address1 { get; set; }

    public string? Address2 { get; set; }

    public bool? IsActive { get; set; }

    public virtual Customer? Customer { get; set; }
}
