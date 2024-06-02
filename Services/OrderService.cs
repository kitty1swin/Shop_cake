using Shop_cake.Data.Models;
using Shop_cake.Data.Repositories;
using System;
using System.Threading.Tasks;

namespace Shop_cake.Services
{
    public class OrderService
    {
        private readonly OrderRepository _orderRepository;

        public OrderService(OrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        // Метод для добавления продукта в корзину
        public async Task AddProductToCartAsync(int productId, int userId, string address)
        {
            await _orderRepository.AddOrderToCartAsync(productId, userId, address);
        }

        // Метод для симуляции оплаты заказа
        public void SimulatePayment(Order order)
        {
            _orderRepository.SimulatePayment(order);
        }
    }
}