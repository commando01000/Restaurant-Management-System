using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_Layer.Dtos
{
    public class OrderItemDto
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public MenuItemDto MenuItem { get; set; }
        public OrderDto Order { get; set; }
        public decimal totalPrice { get; set; }
    }
}
