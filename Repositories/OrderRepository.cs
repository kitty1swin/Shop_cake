using Microsoft.EntityFrameworkCore;
using Shop_cake.Data.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_cake.Data.Repositories
{
    public class OrderRepository
    {
        private readonly AppDBContext _dbContext;

        public OrderRepository(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddOrderToCartAsync(int productId, int userId, string address)
        {
            // Проверяем, существует ли продукт
            var product = await _dbContext.Products.FindAsync(productId);
            if (product == null)
            {
                throw new InvalidOperationException("Продукт не найден");
            }

            // Получаем все заказы текущего пользователя, находящиеся в корзине
            var userOrders = _dbContext.Orders.Where(o => o.UserId == userId && o.Status == "В корзине").ToList();

            // Подсчитываем итоговую сумму всех продуктов в корзине
            decimal total = (decimal)userOrders.Sum(o => o.Product.Price);

            // Обновляем статус всех заказов пользователя на "Создан" перед оформлением нового заказа
            for (int i = 0; i < userOrders.Count; i++)
            {
                userOrders[i].Status = "Создан";
            }

            // Создаем новый заказ для корзины
            var order = new Order
            {
                ProductId = productId,
                UserId = userId,
                Address = address,
                Status = "Выполняется", // Обновляем статус нового заказа
                TotalPrice = total // Устанавливаем итоговую сумму продуктов в корзине
            };

            // Добавляем заказ в базу данных
            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync();

            //Симуляция оплаты заказа 
             SimulatePayment(order);

            // Обновление базы данных после оплаты (помечаем заказ как "Выполнен")
            order.Status = "Выполнен";
            await _dbContext.SaveChangesAsync();
        }

        // Пример метода для симуляции оплаты
        public void SimulatePayment(Order order)
        {
            // Здесь можно добавить логику для симуляции оплаты заказа
            Console.WriteLine($"Оплата заказа №{order.Id} на сумму {order.TotalPrice} рублей прошла успешно.");
        }
    }
}