using ECommerceShopAPI.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceShopAPI.Services
{
    public interface IECommerceShopService
    {
        Task<IEnumerable<ProductDto>> GetProducts();
        Task<Common.Response> AddProductsToCart(PurchaseOrderDto purchaseOrderDto);
        Task<Common.Response> CreateOrder(PurchaseOrderDto PurchaseOrderDto);
    }
}
