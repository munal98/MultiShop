using MultiShop.Order.Application.Features.CQRS.Queries.OrderDetailQueries;
using MultiShop.Order.Application.Features.CQRS.Results.OrderDetailResults;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.OrderDetailHandlers
{
    public class GetOrderDetailByIdQueryHandler
    {
        private readonly IRepository<OrderDetail> _repository;

        // Dependency Injection ile repository'nin alındığı constructor
        public GetOrderDetailByIdQueryHandler(IRepository<OrderDetail> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<GetOrderDetailByIdQueryResult> Handle(GetOrderDetailByIdQuery query)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query), "The query cannot be null.");
            }

            // Repository'den veri çekme
            var values = await _repository.GetByIdAsync(query.Id);

            if (values == null)
            {
                // Eğer değer bulunamazsa, uygun bir hata fırlatılabilir.
                throw new InvalidOperationException($"Order detail with ID {query.Id} not found.");
            }

            // Veriyi dönme
            return new GetOrderDetailByIdQueryResult
            {
                OrderDetailId = values.OrderDetailId,
                ProductAmount = values.ProductAmount,
                ProductId = values.ProductId,
                ProductName = values.ProductName,
                OrderingId = values.OrderingId,
                ProductPrice = values.ProductPrice,
                ProductTotalPrice = values.ProductTotalPrice,
            };
        }
    }
}
