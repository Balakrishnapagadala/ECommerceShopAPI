using System;
using System.Collections.Generic;

namespace ECommerceShopAPI.Entities.Entities;

public partial class CustomerShippingDetailEntity
{
    public int ShippingId { get; set; }

    public int? CustomerId { get; set; }

    public string? Address1 { get; set; }

    public string? Address2 { get; set; }

    public bool? IsActive { get; set; }

    public virtual CustomerEntity? Customer { get; set; }
}
