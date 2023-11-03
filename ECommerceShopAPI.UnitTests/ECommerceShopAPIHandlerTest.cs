using ECommerceShopAPI.Command;
using ECommerceShopAPI.Controllers;
using ECommerceShopAPI.Entities.DTO;
using ECommerceShopAPI.Entities.Entities;
using ECommerceShopAPI.Entities.Models;
using ECommerceShopAPI.Queries;
using ECommerceShopAPI.Repository;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestPlatform.Common;
using Moq;
using Serilog.Configuration;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceShopAPI.UnitTests
{
    [TestClass]
    public class ECommerceShopAPIHandlerTest
    {
        private Mock<IECommerceShopRepository> mockECommerceShopRepository;
        private Mock<ILogger<AddProductsToCartHandler>> mockLogger;
        private AddProductsToCartHandler addProductsToCartHandler;
        private CreateOrderHandler createOrderHandler;
        private GetProductsHandler getProductsHandler;

        [TestInitialize]
        public void TestInit()
        {
            mockECommerceShopRepository = new Mock<IECommerceShopRepository>();
            mockLogger = new Mock<ILogger<AddProductsToCartHandler>>();
            addProductsToCartHandler = new AddProductsToCartHandler(mockECommerceShopRepository.Object);
            createOrderHandler = new CreateOrderHandler(mockECommerceShopRepository.Object);
            getProductsHandler = new GetProductsHandler(mockECommerceShopRepository.Object);
        }


        [TestMethod]
        public void AddProductsToCartHandler_Handle_VerifyCall()
        {
            var requestData = InitializeData();
            mockECommerceShopRepository.Setup(x => x.CreateCart(It.IsAny<CartEntity>())).ReturnsAsync(It.IsAny<bool>());

            var response = addProductsToCartHandler.Handle(requestData,default(CancellationToken));
            mockECommerceShopRepository.Verify(x => x.CreateCart(It.IsAny<CartEntity>()), Times.Once());
        }

        [TestMethod]
        public void AddProductsToCartHandler_Handle_ReturnsSuccess()
        {
            var requestData = InitializeData();
            mockECommerceShopRepository.Setup(x => x.CreateCart(It.IsAny<CartEntity>())).ReturnsAsync(It.IsAny<bool>());

            var response = addProductsToCartHandler.Handle(requestData, default(CancellationToken));

            Assert.IsTrue(response.Result);
        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public async Task AddProductsToCartHandler_Handle_ThrowsError()
        {
            var response = addProductsToCartHandler.Handle(null, default(CancellationToken)).Result;

        }

        [TestMethod]
        public void CreateOrder_Handle_VerifyCall()
        {
            var requestData = InitializeOrderRequestdata();
            mockECommerceShopRepository.Setup(x => x.CreateOrder(It.IsAny<PurchaseOrderEntity>())).ReturnsAsync(It.IsAny<bool>());

            var response = createOrderHandler.Handle(requestData, default(CancellationToken));
            mockECommerceShopRepository.Verify(x => x.CreateOrder(It.IsAny<PurchaseOrderEntity>()), Times.Once());
        }

        [TestMethod]
        public void CreateOrder_Handle_ReturnsSuccess()
        {
            var requestData = InitializeOrderRequestdata();
            mockECommerceShopRepository.Setup(x => x.CreateOrder(It.IsAny<PurchaseOrderEntity>())).ReturnsAsync(It.IsAny<bool>());

            var response = createOrderHandler.Handle(requestData, default(CancellationToken));

            Assert.IsTrue(response.Result);
        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public async Task CreateOrder_Handle_ThrowsError()
        {
            var response = createOrderHandler.Handle(null, default(CancellationToken)).Result;

        }


        [TestMethod]
        public void GetProducts_Handler_VerifyCall()
        {
            var requestData = InitializeGetProductsRequestData();
            mockECommerceShopRepository.Setup(x => x.GetProducts()).ReturnsAsync(It.IsAny<IEnumerable<ProductEntity>>());

            var response = getProductsHandler.Handle(requestData, default(CancellationToken));
            mockECommerceShopRepository.Verify(x => x.GetProducts(), Times.Once());
        }

        [TestMethod]
        public void GetProducts_Handler_ReturnsValidProducts()
        {
            var requestData = InitializeGetProductsRequestData();
            var responseDate = ResponseProducts();
            mockECommerceShopRepository.Setup(x => x.GetProducts()).ReturnsAsync(responseDate);

            var response = getProductsHandler.Handle(requestData, default(CancellationToken));

            Assert.AreEqual(response.Result.Count(),1);
        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public async Task GetProducts_Handler_ThrowsError()
        {
            var response = getProductsHandler.Handle(null, default(CancellationToken)).Result;

        }

        /// <summary>
        /// Intitialize add products to cart data
        /// </summary>
        /// <returns></returns>
        private AddProductsToCartCommand InitializeData()
        {
            var productDto = new ProductEntity()
            {
                ProductId = 1000,
                Name = "Test",
                Price = 1000,
                ProductType = 1,
                Quantity = 2
            };
            var productList = new List<ProductEntity>();
            productList.Add(productDto);
            var addProductsToCart = new AddProductsToCartCommand(1000, productList);
            return addProductsToCart;
        }

        /// <summary>
        /// Initialize order data
        /// </summary>
        /// <returns></returns>
        private CreateOrderCommand InitializeOrderRequestdata()
        {
            var product = new ProductEntity()
            {
                ProductId = 1000,
                Name = "Test",
                Price = 1000,
                ProductType = 1,
                Quantity = 2
            };
            var productList = new List<ProductEntity>();
            productList.Add(product);
            var PurchaseOrderDto = new PurchaseOrderEntity()
            {
                CustomerId = 1000,
                OrderId = 10001,
                TotalAmount = 100000,
                OrderItems = productList
            };

            return new CreateOrderCommand(PurchaseOrderDto);

        }

        /// <summary>
        /// Intitialize data
        /// </summary>
        /// <returns></returns>
        private GetProducts InitializeGetProductsRequestData()
        {
            return new GetProducts();
        }

        /// <summary>
        /// Products response
        /// </summary>
        /// <returns></returns>
        private IEnumerable<ProductEntity> ResponseProducts()
        {
            var product = new ProductEntity { ProductId = 999,Name = "Book",Price =100};
            var listProducts = new List<ProductEntity>();
            listProducts.Add(product);
            return listProducts;
        }
    }
}
