using ECommerceShopAPI.Entities.DTO;
using ECommerceShopAPI.Entities.Entities;
using ECommerceShopAPI.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceShopAPI.Repository
{
    public interface IECommerceShopRepository
    {
        public Task<IEnumerable<ProductEntity>> GetProducts();

        public Task<bool> CreateCart(CartEntity cart);

        public Task<bool> CreateOrder(PurchaseOrderEntity order);


    }
}
