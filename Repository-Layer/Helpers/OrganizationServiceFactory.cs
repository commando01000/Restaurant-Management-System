using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Repository.Helpers
{
    public static class OrganizationServiceFactory
    {
        public static IOrganizationService GetCrmService()
        {
            // Retrieve the connection string from the web.config
            string connectionString = ConfigurationManager.ConnectionStrings["CrmConnection"].ConnectionString;
            // Create a connection to the CRM service
            CrmServiceClient crmServiceClient = new CrmServiceClient(connectionString);
            // Return the IOrganizationService instance
            return crmServiceClient.OrganizationServiceProxy;
        }


    }
}
