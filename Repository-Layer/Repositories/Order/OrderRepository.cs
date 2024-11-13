using CRM.Repository;
using CRM.Repository.Helpers;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository_Layer.Repositories.Order
{
    public class OrderRepository : BasePlugin, IOrderRepository
    {
        public OrderRepository()
        {
            _Service = OrganizationServiceFactory.GetCrmService();
        }
        public void addOrder(Entity entity)
        {
            _Service.Create(entity);
        }

        public void deleteOrder(Entity entity)
        {
            _Service.Delete(entity.LogicalName, entity.Id);
        }

        public Entity getOrderById(Guid id)
        {
            string fetchXml = $@"<fetch>
                                  < entity name = 'initiumc_order_ss' >
                                    < attribute name = 'initiumc_orderstatus' />
                                    < attribute name = 'initiumc_customerid' />
                                    < attribute name = 'initiumc_name' />
                                    < attribute name = 'createdon' />
                                    < attribute name = 'initiumc_orderprice' />
                                    < attribute name = 'initiumc_order_ssid' />
                                    < attribute name = 'initiumc_discount' />
                                    <filter>
                                          <condition attribute='initiumc_order_ssid' operator='eq' value= '{id}' />
                                    </filter>
                                  </ entity >
                                </fetch>";
            return XrmExtensions.FetchSingle(_Service, fetchXml);
        }

        public DataCollection<Entity> getOrders()
        {
            string fetchXml = @"
                                <fetch>
                                  <entity name='initiumc_order_ss'>
                                    <attribute name='initiumc_orderstatus' />
                                    <attribute name='initiumc_customerid' />
                                    <attribute name='initiumc_name' />
                                    <attribute name='createdon' />
                                    <attribute name='initiumc_orderprice' />
                                    <attribute name='initiumc_order_ssid' />
                                    <attribute name='initiumc_discount' />
                                  </entity>
                                </fetch>
                            ";

            return XrmExtensions.FetchMultiple(_Service, fetchXml);
        }

        public void updateOrder(Entity entity)
        {
            throw new NotImplementedException();
        }
    }
}
