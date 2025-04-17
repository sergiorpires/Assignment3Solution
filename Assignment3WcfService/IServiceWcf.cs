using Assignment3WcfService.Models;
using System.Collections.Generic;
using System.ServiceModel;

namespace Assignment3WcfService
{

    [ServiceContract]
    public interface IServiceWcf
    {
        // operation contract to get WSDL operations
        [OperationContract]
        List<WsdlOperation> GetWsdlOperations(string wsdlUrl);

    }

}
