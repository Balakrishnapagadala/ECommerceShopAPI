using AutoMapper;
using ECommerceShopAPI.Command;
using ECommerceShopAPI.Entities.DTO;
using ECommerceShopAPI.Queries;
using ECommerceShopAPI.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceShopAPI.Controllers
{
    /// <summary>
    /// Ecommerce shop controller
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ECommerceShopController : ControllerBase
    {
        private readonly ILogger<ECommerceShopController> _logger;
        private readonly IECommerceShopService _service;

        /// <summary>
        /// Controller Constructor
        /// </summary>
        /// <param name="mediator"></param>
        public ECommerceShopController(ILogger<ECommerceShopController> logger, IECommerceShopService service)

        {
            this._logger = logger;
            this._service = service;
        }

        /// <summary>
        /// Gets the products list
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetProducts")]
        public async Task<IActionResult> GetProducts()
        {
            try
            {
                _logger.LogInformation("GetProducts : Execution started");
                var products = _service.GetProducts();
                _logger.LogInformation("GetProducts : Execution ended");
                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error Occured while executing GetProducts" + ex.InnerException);
                throw;
            }
        }

        /// <summary>
        /// Add products to cart
        /// </summary>
        /// <param name="PurchaseOrderDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("AddProductsToCart")]
        public async Task<IActionResult> AddProductsToCart(PurchaseOrderDto PurchaseOrderDto)
        {
            try
            {
                if (PurchaseOrderDto == null) { throw new ArgumentNullException(nameof(PurchaseOrderDto)); }

                _logger.LogInformation("AddProductsToCart : Execution started");
                var response = _service.AddProductsToCart(PurchaseOrderDto);
                _logger.LogInformation("AddProductsToCart : Execution ended");
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error Occured while executing AddProductsToCart" + ex.InnerException);
                throw;
            }
        }

        [HttpPost]
        [ActionName("CreateOrder")]
        public async Task<IActionResult> CreateOrder(PurchaseOrderDto PurchaseOrderDto)
        {
            try
            {
                if (PurchaseOrderDto == null) { throw new ArgumentNullException(nameof(PurchaseOrderDto)); }
                _logger.LogInformation("CreateOrder : Execution started");
                var response = await _service.CreateOrder(PurchaseOrderDto);
                _logger.LogInformation("CreateOrder : Execution ended");
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error Occured while executing CreateOrder" + ex.InnerException);
                throw;
            }
        }
    }
}
