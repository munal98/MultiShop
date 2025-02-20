using MultiShop.Order.Application.Features.CQRS.Commands.AddressCommands;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;
using System.Threading.Tasks;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.AddressHandlers
{
    public class RemoveAddressCommandHandler
    {
        private readonly IRepository<Address> _repository;

        // Constructor ile repository'i alıyoruz
        public RemoveAddressCommandHandler(IRepository<Address> repository)
        {
            _repository = repository;
        }

        // Handle metoduna RemoveAddressCommand parametresi ekliyoruz
        public async Task Handle(RemoveAddressCommand command)
        {
            // command.Id üzerinden adresi alıyoruz
            var address = await _repository.GetByIdAsync(command.Id);

            // Eğer adres bulunursa, silme işlemi yapıyoruz
            if (address != null)
            {
                await _repository.DeleteAsync(address);
            }
        }
    }
}
