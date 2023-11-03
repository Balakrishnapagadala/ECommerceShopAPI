using System;
using System.Collections.Generic;

namespace ECommerceShopAPI.Entities.DTO;

public class OrderDto
{
    public int OrderId { get; set; }

    public int? CustomerId { get; set; }

    public decimal? TotalAmount { get; set; }

    public DateTime? CreatedDate { get; set; }

    public bool? IsActive { get; set; }

}
