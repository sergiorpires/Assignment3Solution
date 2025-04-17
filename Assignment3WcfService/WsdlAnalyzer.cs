using Assignment3WcfService.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Services.Description;

namespace Assignment3WcfService
{
    // class to analyze WSDL content
    public class WsdlAnalyzer
    {
        // method to get WSDL operations
        public List<WsdlOperation> GetWsdlOperations(string wsdlUrl)
        {
            try
            {
                // Download the WSDL file
                using (WebClient webClient = new WebClient())
                {
                    // Read the WSDL content
                    using (var stream = webClient.OpenRead(wsdlUrl))
                    {
                        // Create a ServiceDescription from the WSDL content
                        ServiceDescription serviceDescription = ServiceDescription.Read(stream);
                        // List to hold WSDL operations
                        List<WsdlOperation> wsdlOperations = new List<WsdlOperation>();
                        // Iterate through the PortTypes in the WSDL
                        foreach (PortType portType in serviceDescription.PortTypes)
                        {
                            // Iterate through the Operations in the PortType
                            foreach (System.Web.Services.Description.Operation operation in portType.Operations)
                            {
                                // Create a WsdlOperation object to store operation details
                                WsdlOperation wsdlOperation = new WsdlOperation();
                                wsdlOperation.Operation = operation.Name;
                                // Get input and output parameters
                                var inputMessage = serviceDescription.Messages[operation.Messages.Input.Message.Name];
                                var inputParams = inputMessage.Parts.Cast<MessagePart>().Select(part => $"{part.Name}:{part.Type.Name}");
                                wsdlOperation.InputParameters = string.Join(", ", inputParams);
                                // Get output parameters
                                var outputMessage = serviceDescription.Messages[operation.Messages.Output.Message.Name];
                                var outputParams = outputMessage.Parts.Cast<MessagePart>().Select(part => $"{part.Name}:{part.Type.Name}");
                                wsdlOperation.OutputParameters = string.Join(", ", outputParams);
                                // Add the operation to the list
                                wsdlOperations.Add(wsdlOperation);
                            }
                        }
                        // Return the list of WSDL operations
                        return wsdlOperations;
                    }
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}