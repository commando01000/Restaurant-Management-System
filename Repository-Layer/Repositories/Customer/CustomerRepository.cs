using CRM.Repository;
using CRM.Repository.Helpers;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository_Layer.CustomerRepository
{
    public class CustomerRepository : BasePlugin, ICustomerRepository
    {
        public CustomerRepository()
        {
            _Service = OrganizationServiceFactory.GetCrmService();
        }
        public DataCollection<Entity> GetAllCustomers()
        {
            string fetchXml = @"
                <fetch>
                  <entity name='initiumc_customer_ss'>
                    <attribute name='createdon' />
                    <attribute name='initiumc_customer_ssid' />
                    <attribute name='initiumc_name' />
                    <attribute name='initiumc_phonenumber' />
                  </entity>
                </fetch>";

            return XrmExtensions.FetchMultiple(_Service, fetchXml);
        }

        public Entity GetCustomerById(Guid id)
        {
            string fetchXml = $@"
                <fetch>
                  <entity name='initiumc_customer_ss'>
                    <attribute name='createdon' />
                    <attribute name='initiumc_customer_ssid' />
                    <attribute name='initiumc_name' />
                    <attribute name='initiumc_phonenumber' />
                    <filter>
                          <condition attribute='initiumc_customer_ssid' operator='eq' value='{id}'/>
                    </filter>
                  </entity>
                </fetch>";

            return XrmExtensions.Fetch(_Service, fetchXml);
        }
        public void AddCustomer(Entity Customer)
        {
            _Service.Create(Customer);
        }
        public void UpdateCustomer(Entity Customer)
        {
            _Service.Update(Customer);
        }

        public void DeleteCustomer(Entity Customer)
        {
            _Service.Delete(Customer.LogicalName, Customer.Id);
        }
    }
}
