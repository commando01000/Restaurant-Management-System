using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository_Layer.CustomerRepository
{
    public interface ICustomerRepository
    {
        DataCollection<Entity> GetAllCustomers();
        Entity GetCustomerById(Guid id);
        void AddCustomer(Entity Customer);
        void UpdateCustomer(Entity Customer);
        void DeleteCustomer(Entity Customer);
    }
}
