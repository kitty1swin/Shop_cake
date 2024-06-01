using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Shop_cake.Data.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string? Status { get; set; }
        public string? Address { get; set; }
        public decimal TotalPrice { get; set; }

        public int? ProductId { get; set; }
        public Product? Product { get; set; }
        public int? FeedbackId { get; set; }
        public Feedback? Feedback { get; set; }
        public int? UserId { get; set; }
        public User? User { get; set; }
    }
}
