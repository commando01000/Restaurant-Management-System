using Service_Layer.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_Layer.Services
{
    public interface IOrderItemService
    {
        void AddOrderItem(OrderItemDto orderItem);
        void UpdateOrderItem(OrderItemDto orderItem);
        void DeleteOrderItem(OrderItemDto orderItemDto);
        List<OrderItemDto> GetAllOrderItems();
        OrderItemDto GetOrderItemById(Guid id);
    }
}
