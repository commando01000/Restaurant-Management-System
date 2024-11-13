using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository_Layer.Repositories.Order
{
    public interface IOrderRepository
    {
        void addOrder(Entity entity);
        void updateOrder(Entity entity);
        void deleteOrder(Entity entity);
        DataCollection<Entity> getOrders();
        Entity getOrderById(Guid id);
    }
}
