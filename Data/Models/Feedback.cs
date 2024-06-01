using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_cake.Data.Models
{
    public class Feedback
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime? CommentDate { get; set; }
        public int? ProductId { get; set; }
        public int? UserId { get; set; } // Добавляем столбец UserId

        public Order? Order { get; set; }
        public int? OrderId { get; set; }
    }
}
