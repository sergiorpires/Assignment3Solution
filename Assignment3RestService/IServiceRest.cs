using Assignment3RestService.Models;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Threading.Tasks;

namespace Assignment3RestService
{
    [ServiceContract]
    public interface IServiceRest
    {
        // operation contracts

        // geocode service
        [OperationContract]
        [WebInvoke(Method = "GET",
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json,
           BodyStyle = WebMessageBodyStyle.Wrapped)]
        Task<Coordinate> GeoCodeAsync(string address);

        // weather service
        [OperationContract]
        [WebInvoke(Method = "GET",
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json,
           BodyStyle = WebMessageBodyStyle.Wrapped)]
        Task<WeatherForecastRoot> WeatherForecastAsync(double latitude, double longitude);

        // weather service
        [OperationContract]
        [WebInvoke(Method = "GET",
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json,
           BodyStyle = WebMessageBodyStyle.Wrapped)]
        Task<AirQualityRoot> AirQualityAsync(double latitude, double longitude);

    }


}
