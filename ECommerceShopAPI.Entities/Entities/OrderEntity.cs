using System;
using System.Collections.Generic;

namespace ECommerceShopAPI.Entities.Entities;

public partial class OrderEntity
{
    public int OrderId { get; set; }

    public int? CustomerId { get; set; }

    public decimal? TotalAmount { get; set; }

    public DateTime? CreatedDate { get; set; }

    public bool? IsActive { get; set; }

    public virtual CustomerEntity? Customer { get; set; }

    public virtual ICollection<OrderDetailEntity> OrderDetails { get; set; } = new List<OrderDetailEntity>();
}
