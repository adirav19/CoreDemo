using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class CartItem
    {
        [Key]
        public int Id { get; set; }
        public Product product { get; set; }
        public int ProductId { get; set; }
        public Cart cart { get; set; }
        public int CartId { get; set; }
        public int  Quantity { get; set; }
    }
}
