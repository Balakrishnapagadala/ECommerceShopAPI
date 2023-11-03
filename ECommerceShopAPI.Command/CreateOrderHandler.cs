using ECommerceShopAPI.Common;
using ECommerceShopAPI.Entities.DTO;
using ECommerceShopAPI.Entities.Entities;
using ECommerceShopAPI.Entities.Models;
using ECommerceShopAPI.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceShopAPI.Command
{
    public class CreateOrderCommand : IRequest<bool>
    {
        public PurchaseOrderEntity PurchaseOrderEntity { get; set; }

        public CreateOrderCommand(PurchaseOrderEntity purchaseOrderEntity)
        {
            PurchaseOrderEntity = purchaseOrderEntity;
        }
    }

    public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, bool>
    {
        private readonly IECommerceShopRepository _eCommerceShopRepository;

        public CreateOrderHandler(IECommerceShopRepository eCommerceShopRepository)
        {
            _eCommerceShopRepository = eCommerceShopRepository;
        }
        /// <summary>
        /// Handler for create order
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<bool> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var purchaseOrderData = request.PurchaseOrderEntity;

            var IsDiscountApplicable = purchaseOrderData.OrderItems.Where(x => x.ProductType == (int)ProductType.NonPhysicalProduct).Any();

            if (IsDiscountApplicable)
            {
                decimal discountPerct = 0.8m;
                purchaseOrderData.OrderItems.Where(x => x.ProductType == (int)ProductType.PhysicalProduct).ToList()
                    .ForEach(x => x.Price = x.Price * discountPerct);
                purchaseOrderData.TotalAmount = purchaseOrderData.OrderItems.Sum(x => x.Price);
                await _eCommerceShopRepository.CreateOrder(purchaseOrderData);
            }

            return true;
        }
    }
}
