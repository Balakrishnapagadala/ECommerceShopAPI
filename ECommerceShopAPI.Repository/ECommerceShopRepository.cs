using AutoMapper;
using ECommerceShopAPI.Entities.DTO;
using ECommerceShopAPI.Entities.Entities;
using ECommerceShopAPI.Entities.Models;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace ECommerceShopAPI.Repository
{
    /// <summary>
    /// Ecommerce shop repository
    /// </summary>
    public class ECommerceShopRepository : IECommerceShopRepository
    {
        private readonly EcommerceShopDbContext _dbContext;
        private readonly IMapper _mapper;
        public ECommerceShopRepository()
        {
        }
        /// <summary>
        /// Parameterized constructor
        /// </summary>
        /// <param name="dbContext"></param>
        public ECommerceShopRepository(IMapper mapper, EcommerceShopDbContext dbContext)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        /// <summary>
        /// Create cart for the selected products
        /// </summary>
        /// <param name="cart"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> CreateCart(CartEntity cartEntity)
        {
            if (_dbContext != null)
            {
                var cart = _mapper.Map<Cart>(cartEntity);
                _dbContext.Carts.Add(cart);
                 await _dbContext.SaveChangesAsync();
                return true;
            }
            else { return false; }
        }

        /// <summary>
        /// Create order
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public async Task<bool> CreateOrder(PurchaseOrderEntity order)
        {
            if (_dbContext != null)
            {
                foreach (var orderItem in order.OrderItems)
                {
                    var orderData = _mapper.Map<Order>(order);

                    _dbContext.Orders.Add(orderData);
                    await _dbContext.SaveChangesAsync();
                    var orderId = _dbContext.Orders.Where(x => x.CustomerId == order.CustomerId).OrderByDescending(x => x.CreatedDate).Select(x => x.OrderId).Take(1).ToList();
                    var orderDetails = new OrderDetail()
                    {
                        OrderId = orderId[0],
                        ProductId = orderItem.ProductId,
                        NetPrice = orderItem.Price,
                        CreatedDate = DateTime.Now,
                        IsActive = true
                    };
                    _dbContext.OrderDetails.Add(orderDetails);
                    await _dbContext.SaveChangesAsync();

                }
                return true;
            }
            else
            { return false; }

        }

        /// <summary>
        /// Gets products list
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ProductEntity>> GetProducts()
        {
            if (_dbContext != null)
            {
                var productList = _dbContext.Products.Where(x => x.IsActive == true);
                var products = _mapper.Map<List<ProductEntity>>(productList.ToList());
                return products;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Create Product
        /// </summary>
        /// <param name="cart"></param>
        /// <returns></returns>
        public async Task<bool> CreateProduct(ProductEntity productEntity)
        {
            if (_dbContext != null)
            {
                var product = _mapper.Map<Product>(productEntity);
                var result = _dbContext.Add(product);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            else { return false; }
        }

    }
}