using Service_Layer.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_Layer.Services.Customer
{
    public interface ICustomerService
    {
        void addCustomer(CustomerDto Customer);
        void updateCustomer(CustomerDto Customer);
        void deleteCustomer(CustomerDto Customer);
        List<CustomerDto> getAllCustomers();
        CustomerDto getCustomerById(Guid id);
    }
}
