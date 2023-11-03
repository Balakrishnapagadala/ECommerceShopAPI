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
    public class ECommerceShopAPIServiceTest
    {
        private Mock<IMediator> mockMediator;
        private Mock<IECommerceShopRepository> mockECommerceShopRepository;
        private Mock<IECommerceShopService> mockECommerceShopService;
        private Mock<ILogger<ECommerceShopService>> mockLogger;
        private ECommerceShopService eCommerceService;
        private Mock<IMapper> mockMapper;

        [TestInitialize]
        public void TestInit()
        {
            mockMediator = new Mock<IMediator>();
            mockMapper = new Mock<IMapper>();
            mockLogger = new Mock<ILogger<ECommerceShopService>>();
            mockECommerceShopRepository = new Mock<IECommerceShopRepository>();
            eCommerceService = new ECommerceShopService(mockMediator.Object,mockLogger.Object, mockMapper.Object);
        }


        [TestMethod]
        public void ProcessPurchaseOrder_GetProducts_VerifyCall()
        {
            mockMediator.Setup(x => x.Send(It.IsAny<GetProducts>(), default(CancellationToken))).ReturnsAsync(It.IsAny<IEnumerable<ProductEntity>>());

            var response = eCommerceService.GetProducts();
            mockMediator.Verify(x => x.Send(It.IsAny<GetProducts>(), default(CancellationToken)), Times.Once());
        }

        [TestMethod]
        public void ProcessPurchaseOrder_GetProducts_ReturnsSuccess_IsNotNull()
        {
            var productDtos = GetProductDtos();
            var productEntities = GetProductEntities();
            mockMediator.Setup(x => x.Send(It.IsAny<GetProducts>(), default(CancellationToken))).ReturnsAsync(productEntities);
            mockMapper.Setup(x => x.Map<List<ProductDto>>(productEntities)).Returns(productDtos);

            var response = eCommerceService.GetProducts().Result;
            Assert.IsNotNull(response);
        }

        [TestMethod]
        public void ProcessPurchaseOrder_AddProductsToCart_VerifyCall()
        {
            var PurchaseOrderDto = InitializeData();
            mockMediator.Setup(x => x.Send(It.IsAny<AddProductsToCartCommand>(), default(CancellationToken))).ReturnsAsync(It.IsAny<bool>);

            var response = eCommerceService.AddProductsToCart(PurchaseOrderDto);
            mockMediator.Verify(x => x.Send(It.IsAny<AddProductsToCartCommand>(), default(CancellationToken)), Times.Once());
        }

        [TestMethod]
        public void ProcessPurchaseOrder_AddProductsToCart_ReturnsSuccess()
        {
            var PurchaseOrderDto = InitializeData();
            mockMediator.Setup(x => x.Send(It.IsAny<AddProductsToCartCommand>(), default(CancellationToken))).ReturnsAsync(true);

            var response = eCommerceService.AddProductsToCart(PurchaseOrderDto).Result;

            Assert.IsTrue(response.IsSuccess);
        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public void ProcessPurchaseOrder_AddProductsToCart_ThrowsError()
        {
            var response = eCommerceService.AddProductsToCart(null).Result;

        }


        [TestMethod]
        public void ProcessPurchaseOrder_CreateOrder_VerifyCall()
        {
            var PurchaseOrderDto = InitializeData();
            mockMediator.Setup(x => x.Send(It.IsAny<CreateOrderCommand>(), default(CancellationToken))).ReturnsAsync(It.IsAny<bool>);

            var response = eCommerceService.CreateOrder(PurchaseOrderDto);
            mockMediator.Verify(x => x.Send(It.IsAny<CreateOrderCommand>(), default(CancellationToken)), Times.Once());
        }

        [TestMethod]
        public void ProcessPurchaseOrder_CreateOrder_ReturnsSuccess()
        {
            var PurchaseOrderDto = InitializeData();
            mockMediator.Setup(x => x.Send(It.IsAny<CreateOrderCommand>(), default(CancellationToken))).ReturnsAsync(true);

            var response = eCommerceService.CreateOrder(PurchaseOrderDto).Result;

            Assert.IsTrue(response.IsSuccess);
        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public void ProcessPurchaseOrder_CreateOrder_ThrowsError()
        {
            var response =eCommerceService.CreateOrder(null).Result;

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
                Price = 1000,
                ProductType = 1,
                Quantity = 2
            };
            var productList = new List<ProductDto>();
            productList.Add(productDto);
            var products = new PurchaseOrderDto()
            {
                CustomerId = 1000,
                OrderId = 10001,
                TotalAmount = 100000,
                OrderItems = productList
            };

            return products;

        }

        /// <summary>
        /// Get products dtos
        /// </summary>
        /// <returns></returns>
        private List<ProductDto> GetProductDtos()
        {
            var productDto = new ProductDto()
            {
                ProductId = 1000,
                Name = "Test",
                Price = 1000,
                ProductType = 1,
                Quantity = 2
            };
            var productList = new List<ProductDto>();
            productList.Add(productDto);

            return productList;
        }

        /// <summary>
        /// Get products Entities
        /// </summary>
        /// <returns></returns>
        private List<ProductEntity> GetProductEntities()
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

            return productList;
        }
    }
}
