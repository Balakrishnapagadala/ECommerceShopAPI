using AutoMapper;
using ECommerceShopAPI.Command;
using ECommerceShopAPI.Common;
using ECommerceShopAPI.Controllers;
using ECommerceShopAPI.Entities.DTO;
using ECommerceShopAPI.Entities.Entities;
using ECommerceShopAPI.Entities.Models;
using ECommerceShopAPI.Queries;
using ECommerceShopAPI.Repository;
using ECommerceShopAPI.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace ECommerceShopAPI.UnitTests
{
    [TestClass] 
    public class ECommerceShopAPIControllerTest
    {
        private Mock<IECommerceShopRepository> mockECommerceShopRepository;
        private Mock<IECommerceShopService> mockECommerceShopService;
        private Mock<ILogger<ECommerceShopController>> mockLogger;
        private ECommerceShopController eCommerceController;
        private Mock<IMapper> mockMapper;

        [TestInitialize]
        public void TestInit()
        {
            mockECommerceShopService = new Mock<IECommerceShopService>();
            mockLogger = new Mock<ILogger<ECommerceShopController>>();
            eCommerceController = new ECommerceShopController(mockLogger.Object, mockECommerceShopService.Object);
        }

        [TestMethod]
        public void ProcessPurchaseOrder_GetProducts_VerifyCall()
        {
            mockECommerceShopService.Setup(x => x.GetProducts()).ReturnsAsync(It.IsAny<IEnumerable<ProductDto>>());

            var response = eCommerceController.GetProducts();
            mockECommerceShopService.Verify(x => x.GetProducts(), Times.Once());
        }

        [TestMethod]
        public void ProcessPurchaseOrder_GetProducts_ReturnsSuccess()
        {
            mockECommerceShopService.Setup(x => x.GetProducts()).ReturnsAsync(It.IsAny<IEnumerable<ProductDto>>());

            var response = (OkObjectResult)eCommerceController.GetProducts().Result;

            Assert.AreEqual(response.StatusCode, 200);
        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public void ProcessPurchaseOrder_GetProducts_ThrowsError()
        {
            mockECommerceShopService.Setup(x => x.GetProducts()).Throws<AggregateException>();

            var response = (OkObjectResult)eCommerceController.GetProducts().Result;

        }

        [TestMethod]
        public void ProcessPurchaseOrder_AddProductsToCart_VerifyCall()
        {
            var PurchaseOrderDto = InitializeData();
            mockECommerceShopService.Setup(x => x.AddProductsToCart(It.IsAny<PurchaseOrderDto>())).ReturnsAsync(It.IsAny<Common.Response>);

            var response = eCommerceController.AddProductsToCart(PurchaseOrderDto);
            mockECommerceShopService.Verify(x => x.AddProductsToCart(It.IsAny<PurchaseOrderDto>()), Times.Once());
        }

        [TestMethod]
        public void ProcessPurchaseOrder_AddProductsToCart_ReturnsSuccess()
        {
            var PurchaseOrderDto = InitializeData();
            mockECommerceShopService.Setup(x => x.AddProductsToCart(It.IsAny<PurchaseOrderDto>())).ReturnsAsync(It.IsAny<Common.Response>);

            var response = (OkObjectResult)eCommerceController.AddProductsToCart(PurchaseOrderDto).Result;

            Assert.AreEqual(response.StatusCode, 200);
        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public void ProcessPurchaseOrder_AddProductsToCart_ThrowsError()
        {
            var response = (OkObjectResult)eCommerceController.AddProductsToCart(null).Result;

        }


        [TestMethod]
        public void ProcessPurchaseOrder_CreateOrder_VerifyCall()
        {
            var PurchaseOrderDto = InitializeData();
            mockECommerceShopService.Setup(x => x.CreateOrder(It.IsAny<PurchaseOrderDto>())).ReturnsAsync(It.IsAny<Common.Response>);

            var response = eCommerceController.CreateOrder(PurchaseOrderDto);
            mockECommerceShopService.Verify(x => x.CreateOrder(It.IsAny<PurchaseOrderDto>()), Times.Once());
        }

        [TestMethod]
        public void ProcessPurchaseOrder_CreateOrder_ReturnsSuccess()
        {
            var PurchaseOrderDto = InitializeData();
            mockECommerceShopService.Setup(x => x.CreateOrder(It.IsAny<PurchaseOrderDto>())).ReturnsAsync(It.IsAny<Common.Response>);

            var response = (OkObjectResult)eCommerceController.CreateOrder(PurchaseOrderDto).Result;

            Assert.AreEqual(response.StatusCode, 200);
        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public void ProcessPurchaseOrder_CreateOrder_ThrowsError()
        {
            var response = (OkObjectResult)eCommerceController.CreateOrder(null).Result;

        }

        /// <summary>
        /// Initialize Data
        /// </summary>
        /// <returns></returns>
        private PurchaseOrderDto InitializeData()
        {
            var productDto = new ProductDto()
            {
                ProductId = 1000,
                Name = "Test",
                Price= 1000,
                ProductType = 1,
                Quantity =2
            };
            var productList = new List<ProductDto>();
            productList.Add(productDto);
            var products = new PurchaseOrderDto()
            { 
             CustomerId =1000,
             OrderId =10001,
             TotalAmount =100000,
             OrderItems = productList
            };

            return products;

        }
    }
}
