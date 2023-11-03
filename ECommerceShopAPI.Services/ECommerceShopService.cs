using AutoMapper;
using ECommerceShopAPI.Command;
using ECommerceShopAPI.Entities.DTO;
using ECommerceShopAPI.Entities.Entities;
using ECommerceShopAPI.Queries;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceShopAPI.Services
{
    public class ECommerceShopService : IECommerceShopService
    {

        private readonly IMediator mediator;
        private readonly ILogger<ECommerceShopService> _logger;
        private readonly IMapper _mapper;

        /// <summary>
        /// Controller Constructor
        /// </summary>
        /// <param name="mediator"></param>
        public ECommerceShopService(IMediator mediator, ILogger<ECommerceShopService> logger, IMapper mapper)

        {
            this.mediator = mediator;
            this._logger = logger;
            this._mapper = mapper;
        }

        /// <summary>
        /// Gets product list
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            var productQuery = new GetProducts();
            var products = await mediator.Send(productQuery);
            var productDtos =  _mapper.Map<List<ProductDto>>(products.ToList());  
            return productDtos;
        }

        /// <summary>
        /// Add products to cart
        /// </summary>
        /// <param name="purchaseOrderDto"></param>
        /// <returns></returns>
        public async Task<Common.Response> AddProductsToCart(PurchaseOrderDto purchaseOrderDto)
        {
            var productDTos = purchaseOrderDto.OrderItems;
            var productEntity = _mapper.Map<List<ProductEntity>>(productDTos.ToList());  
            var addProducts = new AddProductsToCartCommand(purchaseOrderDto.CustomerId, productEntity);
            var handlerResponse = await mediator.Send(addProducts);
            var returnResponse = new Common.Response();
            if (handlerResponse)
            {
                returnResponse.IsSuccess = true;
                returnResponse.Message = "Order created successfully!";
            }

            return returnResponse;
        }

        /// <summary>
        /// Create order
        /// </summary>
        /// <param name="PurchaseOrderDto"></param>
        /// <returns></returns>
        public async Task<Common.Response> CreateOrder(PurchaseOrderDto purchaseOrderDto)
        {
            if (purchaseOrderDto == null) { throw new ArgumentNullException(); }
            var purchaseOrderEntity = _mapper.Map<PurchaseOrderEntity>(purchaseOrderDto);
            var addProducts = new CreateOrderCommand(purchaseOrderEntity);
            var handlerResponse = await mediator.Send(addProducts);
            var returnResponse = new Common.Response();
            if(handlerResponse)
            {
                returnResponse.IsSuccess = true;
                returnResponse.Message = "Order created successfully!";

            }
            return returnResponse;
        }
    }
}