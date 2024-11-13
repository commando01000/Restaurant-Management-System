using Service_Layer.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_Layer.Services.Orders
{
    public interface IOrderService
    {
        void addOrder(OrderDto order);
        void updateOrder(OrderDto order);
        void deleteOrder(OrderDto order);
        List<OrderDto> getAllOrders();
        OrderDto getOrderById(Guid id);
    }
}
