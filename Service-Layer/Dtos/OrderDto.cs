using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_Layer.Dtos
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public CustomerDto customer { get; set; }
        public List<OrderItemDto> orderItems { get; set; }
        public decimal discount { get; set; }
        public decimal orderPrice { get; set; }
        public int? orderStatus { get; set; }
        public DateTime orderDate { get; set; }
    }
}
