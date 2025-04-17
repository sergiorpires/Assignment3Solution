using Assignment3WcfService.Models;
using System.Collections.Generic;

namespace Assignment3WcfService
{
    public class ServiceWcf : IServiceWcf
    {
        // method to get WSDL operations
        public List<WsdlOperation> GetWsdlOperations(string wsdlUrl)
        {
            try
            {
                // Create an instance of WsdlAnalyzer
                WsdlAnalyzer analyzer = new WsdlAnalyzer();
                // Call the method to get WSDL operations
                return analyzer.GetWsdlOperations(wsdlUrl);
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
