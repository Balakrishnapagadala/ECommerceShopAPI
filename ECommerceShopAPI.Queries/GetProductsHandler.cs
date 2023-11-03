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

namespace ECommerceShopAPI.Queries
{
    /// <summary>
    /// Get products
    /// </summary>
    public class GetProducts : IRequest<IEnumerable<ProductEntity>>
    {

        public GetProducts( )
        {

        }
    }

    /// <summary>
    /// Gets Products Handler
    /// </summary>
    public class GetProductsHandler : IRequestHandler<GetProducts, IEnumerable<ProductEntity>>
    {
        private readonly IECommerceShopRepository _eCommerceShopRepository;

        /// <summary>
        /// Param Constructor
        /// </summary>
        /// <param name="eCommerceShopRepository"></param>
        public GetProductsHandler(IECommerceShopRepository eCommerceShopRepository)
        {
            _eCommerceShopRepository = eCommerceShopRepository;
        }

        /// <summary>
        /// Handler for Get Products
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ProductEntity>> Handle(GetProducts request, CancellationToken cancellationToken)
        {
            if (request == null) { throw new ArgumentNullException(nameof(request));}
            var products = await  _eCommerceShopRepository.GetProducts();

              return products;
        }
    }
}
