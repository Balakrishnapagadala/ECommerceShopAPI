using Azure;
using ECommerceShopAPI.Common;
using ECommerceShopAPI.Entities.DTO;
using ECommerceShopAPI.Entities.Entities;
using ECommerceShopAPI.Entities.Models;
using ECommerceShopAPI.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceShopAPI.Command
{
    public class AddProductsToCartCommand : IRequest<bool>
    {
        public int CustomerId { get; set; }
        public ICollection<ProductEntity> OrderItems { get; set; }
        public AddProductsToCartCommand(int customerId, ICollection<ProductEntity> orderItems)
        {
            CustomerId = customerId;
            OrderItems = orderItems;
        }
    }

    /// <summary>
    /// Add products to cart
    /// </summary>
    public class AddProductsToCartHandler : IRequestHandler<AddProductsToCartCommand, bool>
    {
        private readonly IECommerceShopRepository _eCommerceShopRepository;

        public AddProductsToCartHandler(IECommerceShopRepository eCommerceShopRepository)
        {
            _eCommerceShopRepository = eCommerceShopRepository;
        }

        /// <summary>
        /// Handler for Add products to cart
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> Handle(AddProductsToCartCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            foreach (var product in request.OrderItems)
            {
                await CreateCart(request.CustomerId, product);
            }

            var response = new Common.Response() { IsSuccess = true, Message = "Added to Cart successfully" };

            return true;
        }


        /// <summary>
        /// Create cart
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <param name="TotalPrice"></param>
        /// <param name="productDto"></param>
        /// <returns></returns>
        private async Task CreateCart(int CustomerId,ProductEntity productDto)
        {
            var cart = new CartEntity()
            {
                CustomerId = CustomerId,
                ProductId = productDto.ProductId,
                Quantity = productDto.Quantity,
                CreatedDate = DateTime.Now,
                IsActive = true
            };

            var response = _eCommerceShopRepository.CreateCart(cart);
        }
    }
}
