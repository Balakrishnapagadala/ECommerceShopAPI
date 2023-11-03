using System;
using System.Collections.Generic;

namespace ECommerceShopAPI.Entities.DTO;

public  class OrderDetailDto
{
    public int OrderDetailId { get; set; }

    public int? OrderId { get; set; }

    public int? ProductId { get; set; }

    public int? Quantity { get; set; }

    public decimal? DiscountAmount { get; set; }

    public decimal? NetPrice { get; set; }

    public DateTime? CreatedDate { get; set; }

    public bool? IsActive { get; set; }

    public virtual OrderDto? Order { get; set; }

    public virtual ProductDto? Product { get; set; }
}
