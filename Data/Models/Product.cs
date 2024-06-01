using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_cake.Data.Models
{
    public class Product
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public ushort? Price { get; set; }
        public string? Category { get; set; }
        public byte[]? Image { get; set; }

        public List<Order>? Orders { get; set; }
    }
}