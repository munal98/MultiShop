﻿using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;
using MultiShop.Order.Application.Features.CQRS.Commands;
using System.Threading.Tasks;
using MultiShop.Order.Application.Features.CQRS.Commands.OrderDetailCommands;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.OrderDetailHandlers
{
    public class CreateOrderDetailCommandHandler
    {
        private readonly IRepository<OrderDetail> _repository;

        public CreateOrderDetailCommandHandler(IRepository<OrderDetail> repository)
        {
            _repository = repository;
        }

        public async Task Handle(CreateOrderDetailCommand command)
        {
            await _repository.CreateAsync(new OrderDetail
            {
                ProductAmount = command.ProductAmount,
                OrderingId = command.OrderingId,
                ProductId = command.ProductId,
                ProductName = command.ProductName,
                ProductPrice = command.ProductPrice,
                ProductTotalPrice = command.ProductTotalPrice
            });
        }
    }
}
