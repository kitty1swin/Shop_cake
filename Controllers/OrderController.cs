using Microsoft.AspNetCore.Mvc;
using Shop_cake.Data;
using Shop_cake.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace cakeStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly List<Order> orders = new List<Order>();
        private readonly AppDBContext _db;

        public OrderController(AppDBContext db)
        {
            _db = db;
        }

        // GET
        [HttpGet]
        public ActionResult<IEnumerable<Order>> Get()
        {
            return _db.Orders.ToArray();
        }

        // GET
        [HttpGet("{id}")]
        public ActionResult<Order> Get(int id)
        {
            var order = _db.Orders.FirstOrDefault(o => o.Id == id);
            if (order == null)
            {
                return NotFound();
            }
            return order;
        }

        // POST
        [HttpPost]
        public ActionResult<Order> Post(Order order)
        {
            var result = _db.Orders.Add(new Order
            {
                Status = order.Status,
                Address = order.Address,
                TotalPrice = order.TotalPrice,
                ProductId = order.ProductId,
                UserId = order.UserId,
                FeedbackId = order.FeedbackId,
            });
            _db.SaveChanges();
            return result.Entity;
        }

        // PUT
        [HttpPut("{id}")]
        public IActionResult Put(int id, Order updatedOrder)
        {
            var order = _db.Orders.FirstOrDefault(o => o.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            order.Status = updatedOrder.Status;
            order.Address = updatedOrder.Address;
            order.TotalPrice = updatedOrder.TotalPrice;
            order.ProductId = updatedOrder.ProductId;
            order.UserId = updatedOrder.UserId;
            order.FeedbackId = updatedOrder.FeedbackId;

            _db.SaveChanges();

            return NoContent();
        }
    }
}