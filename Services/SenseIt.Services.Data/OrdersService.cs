namespace SenseIt.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using SenseIt.Data.Common.Repositories;
    using SenseIt.Data.Models;
    using SenseIt.Data.Models.Enumerations;
    using SenseIt.Services.Mapping;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class OrdersService : IOrdersService
    {
        private readonly IDeletableEntityRepository<Order> ordersRepository;
        private readonly IAddressService addressService;
        private readonly ICartItemsService cartItemsService;

        public OrdersService(
            IDeletableEntityRepository<Order> ordersRepository,
            IAddressService addressService,
            ICartItemsService cartItemsService)
        {
            this.ordersRepository = ordersRepository;
            this.addressService = addressService;
            this.cartItemsService = cartItemsService;
        }

        public async Task<int> CreateOrder(string userId, string town, string street, string number, string zipCode)
        {
            var addressId = await this.addressService.GetAddressId(userId, town, street, number, zipCode);

            var order = new Order
            {
                RecipientId = userId,
                DeliveryAddressId = addressId,
                OrderStatus = OrderStatus.Pending,
            };

            var items = await this.cartItemsService.GetAllByUserId(userId);

            foreach (var item in items)
            {
                order.OrderItems
                    .Add(new OrderItem
                    {
                        Name = item.Product.Name,
                        Quantity = item.Quantity,
                        Price = item.Product.Price,
                        OrderId = order.Id,
                    });
            }

            order.TotalSum = order.OrderItems.Sum(oi => oi.Price * oi.Quantity);

            await this.ordersRepository.AddAsync(order);
            var result = await this.ordersRepository.SaveChangesAsync();

            return order.Id;
        }

        public async Task<IEnumerable<T>> GetAllByUser<T>(string userId)
        {
            var orders = await this.ordersRepository
                .AllAsNoTracking()
                .Where(o => o.RecipientId == userId)
                .To<T>()
                .ToListAsync();

            return orders;
        }

        public async Task<T> GetById<T>(int orderId)
        {
            var order = await this.ordersRepository
                .AllAsNoTracking()
                .Include(o => o.OrderItems)
                .Where(o => o.Id == orderId)
                .To<T>()
                .FirstOrDefaultAsync();

            return order;
        }
    }
}
