using AutoMapper;
using ECommerceShopAPI.Command;
using ECommerceShopAPI.Entities.DTO;
using ECommerceShopAPI.Entities.Entities;
using ECommerceShopAPI.Entities.Models;
using ECommerceShopAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;
using Moq.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceShopAPI.UnitTests
{
    /// <summary>
    /// ECommerce API Repository Unit tests
    /// </summary>
    [TestClass]
    public class ECommerceShopAPIRepositoryTest
    {
        private ECommerceShopRepository mockECommerceRepos;
        private Mock<EcommerceShopDbContext> mockECommerceShopdbContext;
        private Mock<DbSet<Cart>> mockCartDbSet;
        private Mock<DbSet<Product>> mockProductDbSet;
        private Mock<DbSet<Order>> mockOrderDbSet;
        private Mock<IMapper> mockMapper;

        [TestInitialize]
        public void TestInit()
        {
            mockMapper = new Mock<IMapper>();
            mockECommerceShopdbContext = new Mock<EcommerceShopDbContext>();
            mockECommerceRepos = new ECommerceShopRepository(mockMapper.Object,mockECommerceShopdbContext.Object);

            // Cart 
            mockCartDbSet = new Mock<DbSet<Cart>>();
            var stub = GetCartData();
            var data = stub.AsQueryable();
            mockCartDbSet.As<IQueryable<Cart>>().Setup(x => x.Provider).Returns(data.Provider);
            mockCartDbSet.As<IQueryable<Cart>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockCartDbSet.As<IQueryable<Cart>>().Setup(x => x.Expression).Returns(data.Expression);
            mockECommerceShopdbContext.Setup(x => x.Set<Cart>()).Returns(mockCartDbSet.Object);
            mockECommerceShopdbContext.Setup(y => y.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            //Product entity
            mockProductDbSet = new Mock<DbSet<Product>>();
            var products = GetProducts();
            var productsData = products.AsQueryable();
            mockProductDbSet.As<IQueryable<Product>>().Setup(x => x.Provider).Returns(productsData.Provider);
            mockProductDbSet.As<IQueryable<Product>>().Setup(x => x.ElementType).Returns(productsData.ElementType);
            mockProductDbSet.As<IQueryable<Product>>().Setup(x => x.Expression).Returns(productsData.Expression);
            mockECommerceShopdbContext.Setup(x => x.Set<Product>()).Returns(mockProductDbSet.Object);
            mockECommerceShopdbContext.Setup(y => y.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            // Order Entity
            mockOrderDbSet = new Mock<DbSet<Order>>();
            var orders = GetOrder();
            var ordersData = orders.AsQueryable();
            mockOrderDbSet.As<IQueryable<Order>>().Setup(x => x.Provider).Returns(ordersData.Provider);
            mockOrderDbSet.As<IQueryable<Order>>().Setup(x => x.ElementType).Returns(ordersData.ElementType);
            mockOrderDbSet.As<IQueryable<Order>>().Setup(x => x.Expression).Returns(ordersData.Expression);
            mockOrderDbSet.As<IQueryable<Order>>().Setup(x => x.GetEnumerator()).Returns(ordersData.GetEnumerator());
            mockECommerceShopdbContext.Setup(x => x.Set<Order>()).Returns(mockOrderDbSet.Object);
            mockECommerceShopdbContext.Setup(y => y.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);
        }

        [TestMethod]
        public async Task ECommerceRepository_CreateCart_Returns_True()
        {
            var cart = GetCart();
            var cartList = new List<Cart>();
            cartList.Add(cart);
            var cartEntity = GetCartEntity();
            mockECommerceShopdbContext.Setup<DbSet<Cart>>(x => x.Carts).ReturnsDbSet(cartList);
            mockMapper.Setup(x => x.Map<Cart>(It.IsAny<CartEntity>())).Returns(cart);
            var response = await mockECommerceRepos.CreateCart(cartEntity);
            Assert.IsTrue(response);
        }

        [TestMethod]
        public async Task ECommerceRepository_CreateCart_Returns_False()
        {
            var data = GetCarts();
            var mockRepos = new ECommerceShopRepository(mockMapper.Object,null);
            var response = await mockRepos.CreateCart(data.FirstOrDefault());
            Assert.IsFalse(response);
        }

        [TestMethod]
        public async Task ECommerceRepository_CreateProduct_Returns_True()
        {
            var data = GetProductsEntity();
            var response = await mockECommerceRepos.CreateProduct(data);
            Assert.IsTrue(response);
        }

        [TestMethod]
        public async Task ECommerceRepository_CreateProduct_Returns_False()
        {
            var data = GetProductsEntity();
            var mockRepos = new ECommerceShopRepository(mockMapper.Object,null);
            var response = await mockRepos.CreateProduct(data);
            Assert.IsFalse(response);
        }

        [TestMethod]
        public async Task ECommerceRepository_GetProducts_ReturnsListofProducts()
        {
            var products = GetProducts();
            var productEntities = GetProductEntities();
            mockMapper.Setup(x=>x.Map<List<ProductEntity>>(products)).Returns(productEntities);
            mockECommerceShopdbContext.Setup<DbSet<Product>>(x => x.Products).ReturnsDbSet(products);
            var response = await mockECommerceRepos.GetProducts();
            var responseData = response.ToList();
            Assert.AreEqual(responseData.Count, 1);
            Assert.AreEqual(responseData[0].Name, products[0].Name);
        }

        [TestMethod]
        public async Task ECommerceRepository_GetProducts_ReturnsNull()
        {
            var products = GetProducts();
            var mockRepos = new ECommerceShopRepository(mockMapper.Object,null);
            var response = await mockRepos.GetProducts();
            Assert.IsNull(response);

        }

        [TestMethod]
        public async Task ECommerceRepository_CreatePurchaseOrder_ReturnsTrue()
        {
            var purchaseOrders = GetPurchaseOrder();
            mockECommerceShopdbContext.Setup<DbSet<Order>>(x => x.Orders).ReturnsDbSet(GetOrder());
            mockECommerceShopdbContext.Setup<DbSet<OrderDetail>>(x => x.OrderDetails).ReturnsDbSet(GetOrderDetail());
            var response = await mockECommerceRepos.CreateOrder(purchaseOrders);
            Assert.IsTrue(response);
        }

        [TestMethod]
        public async Task ECommerceRepository_CreatePurchaseOrder_ReturnsNull()
        {
            var PurchaseOrderDto = GetPurchaseOrder();
            var mockRepos = new ECommerceShopRepository(mockMapper.Object,null);
            var response = await mockRepos.CreateOrder(PurchaseOrderDto);
            Assert.IsFalse(response);
        }

        /// <summary>
        /// Gets products list
        /// </summary>
        /// <returns></returns>
        private List<Product> GetProducts()
        {
            var product = new Product { ProductId = 1, Name = "book", Price = 100, IsActive = true };
            var products = new List<Product>();
            products.Add(product);

            return products;

        }

        /// <summary>
        /// Gets product
        /// </summary>
        /// <returns></returns>
        private ProductEntity GetProductsEntity()
        {
            var product = new ProductEntity { ProductId = 1, Name = "book", Price = 100, IsActive = true };

            return product;

        }


        /// <summary>
        /// Gets products list
        /// </summary>
        /// <returns></returns>
        private List<ProductEntity> GetProductEntities()
        {
            var product = new ProductEntity { ProductId = 1, Name = "book", Price = 100};
            var products = new List<ProductEntity>();
            products.Add(product);

            return products;

        }

        /// <summary>
        /// Get carts
        /// </summary>
        /// <returns></returns>
        private List<CartEntity> GetCarts()
        {
            var listCart = new List<CartEntity>();
            var cartData = new CartEntity
            {
                CartId = 10000,
                CustomerId = 1000,
                CreatedDate = DateTime.Now,
                IsActive = true,
                ProductId = 100,
                Quantity = 100
            };
            listCart.Add(cartData);

            return listCart;
        }

        /// <summary>
        /// Get carts
        /// </summary>
        /// <returns></returns>
        private CartEntity GetCartEntity()
        {
            var cartData = new CartEntity
            {
                CartId = 10000,
                CustomerId = 1000,
                CreatedDate = DateTime.Now,
                IsActive = true,
                ProductId = 100,
                Quantity = 100
            };

            return cartData;
        }


        /// <summary>
        /// Get carts
        /// </summary>
        /// <returns></returns>
        private Cart GetCart()
        {
            var cartData = new Cart
            {
                CartId = 10000,
                CustomerId = 1000,
                CreatedDate = DateTime.Now,
                IsActive = true,
                ProductId = 100,
                Quantity = 100
            };

            return cartData;
        }
        /// <summary>
        /// Get carts
        /// </summary>
        /// <returns></returns>
        private List<Cart> GetCartData()
        {
            var cartData = new Cart
            {
                CartId = 10000,
                CustomerId = 1000,
                CreatedDate = DateTime.Now,
                IsActive = true,
                ProductId = 100,
                Quantity = 100
            };

            var listCart = new List<Cart>();
            listCart.Add(cartData);
            return listCart;
        }
        /// <summary>
        /// Get purchase order
        /// </summary>
        /// <returns></returns>
        private PurchaseOrderEntity GetPurchaseOrder()
        {
            var product = new ProductEntity() { Name = "Mobile", Price = 100000, ProductId = 10100101, ProductType = 1, Quantity = 20 };
            var productList = new List<ProductEntity>();
            productList.Add(product);

            var PurchaseOrderDto = new PurchaseOrderEntity() { CustomerId = 1000, OrderId = 1010, TotalAmount = 1000000, OrderItems = productList };
            return PurchaseOrderDto;

        }

        /// <summary>
        /// Get order
        /// </summary>
        /// <returns></returns>
        private List<Order> GetOrder()
        {
            var orderData = new Order()
            {
                CustomerId = 1000,
                TotalAmount = 1010,
                CreatedDate = DateTime.Now,
                IsActive = true
            };
            var orderList = new List<Order>();
            orderList.Add(orderData);
            return orderList;
        }

        /// <summary>
        /// Get order details
        /// </summary>
        /// <returns></returns>
        private List<OrderDetail> GetOrderDetail()
        {
            var detailData = new OrderDetail()
            {
                OrderId = 1000,
                OrderDetailId = 101010,
                DiscountAmount = 100,
                IsActive = true,
                ProductId = 999,
                Quantity = 20,
                CreatedDate = DateTime.Now
            };
            var detailList = new List<OrderDetail>();
            detailList.Add(detailData);
            return detailList;
        }
    }
}
