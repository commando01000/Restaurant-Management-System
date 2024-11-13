using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository_Layer.Repositories.OrderItem
{
    public interface IOrderItemRepository
    {
        void AddOrderItem(Entity orderItem);
        void UpdateOrderItem(Entity orderItem);
        void DeleteOrderItem(Entity orderItem);
        DataCollection<Entity> GetAllOrderItems();
        Entity GetOrderItemById(int id);
    }
}
