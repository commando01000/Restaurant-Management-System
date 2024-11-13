using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_Layer.Dtos
{
    public class MenuItemDto
    {
        public Guid Id { get; set; }
        public string itemName { get; set; }
        public string description { get; set; }
        public decimal price { get; set; }
        public int? itemStatus { get; set; }
    }
}
