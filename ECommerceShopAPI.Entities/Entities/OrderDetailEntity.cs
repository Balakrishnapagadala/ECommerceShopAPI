using System;
using System.Collections.Generic;

namespace ECommerceShopAPI.Entities.Entities;

public partial class OrderDetailEntity
{
    public int OrderDetailId { get; set; }

    public int? OrderId { get; set; }

    public int? ProductId { get; set; }

    public int? Quantity { get; set; }

    public decimal? DiscountAmount { get; set; }

    public decimal? NetPrice { get; set; }

    public DateTime? CreatedDate { get; set; }

    public bool? IsActive { get; set; }

    public virtual OrderEntity? Order { get; set; }

    public virtual ProductEntity? Product { get; set; }
}
