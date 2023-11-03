using System;
using System.Collections.Generic;

namespace ECommerceShopAPI.Entities.Models;

public partial class OrderDetail
{
    public int OrderDetailId { get; set; }

    public int? OrderId { get; set; }

    public int? ProductId { get; set; }

    public int? Quantity { get; set; }

    public decimal? DiscountAmount { get; set; }

    public decimal? NetPrice { get; set; }

    public DateTime? CreatedDate { get; set; }

    public bool? IsActive { get; set; }

    public virtual Order? Order { get; set; }

    public virtual Product? Product { get; set; }
}
