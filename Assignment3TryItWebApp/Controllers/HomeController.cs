using Assignment3RestService.Models;
using Assignment3WcfService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Assignment3TryItWebApp.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            ViewBag.Message = "This is my identification page.";

            return View();
        }

        [HttpGet]
        public ActionResult TryIt()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> GeoCode(string address)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(address))
                {
                    // Handle the case when the address is empty
                    ViewBag.Error = "Please enter a valid address.";
                    return View();
                }

                // instantiate rest client
                Assignment3RestService.ServiceRest client = new Assignment3RestService.ServiceRest();
                // get coordinates
                var coordinate = await client.GeoCodeAsync(address);
                // check if coordinate is not null
                if (coordinate != null)
                {
                    // Store variables in Session
                    Session["Address"] = address;
                    Session["Latitude"] = coordinate.Latitude;
                    Session["Longitude"] = coordinate.Longitude;
                }
                else
                {
                    // Handle the case when geocoding fails
                    ViewBag.Error = "Geocoding failed. Please check the address and try again.";
                }

                return View("TryIt");
            }
            catch (Exception)
            {
                ViewBag.Error = "Geocoding failed. Please check the address and try again.";
                return null;
            }
        }

        [HttpPost]
        public ActionResult ClearSession()
        {
            // Clear specific session variables
            Session.Clear();
            Session["Address"] = "Boise Center, ID";
            Session["WsdlUrl"] = "https://digital.weather.gov/xml/SOAP_server/ndfdXMLserver.php?wsdl";

            return RedirectToAction("TryIt");
        }

        [HttpGet]
        public async Task<ActionResult> WeatherForecast()
        {
            try
            {
                // instantiate rest client
                Assignment3RestService.ServiceRest client = new Assignment3RestService.ServiceRest();

                // get weather forecast
                WeatherForecastRoot resultJSON = await client.WeatherForecastAsync(
                    Convert.ToDouble(Session["Latitude"]),
                    Convert.ToDouble(Session["Longitude"])
                );

                if (resultJSON != null)
                {
                    // Store the result in Session
                    Session["WeatherForecast"] = resultJSON;
                }
                else
                {
                    ViewBag.Error = "WeatherForecast failed. Please get a valid coordinate and try again.";
                }

                return View("TryIt");

            }
            catch (Exception)
            {
                ViewBag.Error = "WeatherForecast failed. Please get a valid coordinate and try again.";
                return null;
            }
        }

        [HttpGet]
        public ActionResult WsdlOperations(string wsdlUrl)
        {
            try
            {
                // instantiate WSDL client
                WsdlAnalyzerService.ServiceWcfClient client = new WsdlAnalyzerService.ServiceWcfClient();
                // get WSDL operations
                List<WsdlOperation> operations = client.GetWsdlOperations(wsdlUrl).ToList();

                if (operations != null)
                {
                    // Store the WSDL URL and operations in Session
                    Session["WsdlUrl"] = wsdlUrl;
                    Session["WsdlOperations"] = operations;
                }
                else
                {
                    ViewBag.Error = "WsdlOperations failed. Type a valid wsdl address and try again.";
                }

                return View("TryIt");
            }
            catch (Exception)
            {
                ViewBag.Error = "WsdlOperations failed. Type a valid wsdl address and try again.";
                return null;
            }
        }

        [HttpGet]
        public async Task<ActionResult> AirQuality(string wsdlUrl)
        {
            try
            {
                // instantiate rest client
                Assignment3RestService.ServiceRest client = new Assignment3RestService.ServiceRest();

                // get weather forecast
                AirQualityRoot data = await client.AirQualityAsync(
                    Convert.ToDouble(Session["Latitude"]),
                    Convert.ToDouble(Session["Longitude"])
                );

                if (data != null)
                {
                    // Prepare list of name-value pairs
                    var resultList = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("Longitude", data.coord.lon.ToString()),
                        new KeyValuePair<string, string>("Latitude", data.coord.lat.ToString()),
                        new KeyValuePair<string, string>("AQI", data.list[0].main.aqi.ToString()),
                        new KeyValuePair<string, string>("CO", data.list[0].components.co.ToString()),
                        new KeyValuePair<string, string>("NO", data.list[0].components.no.ToString()),
                        new KeyValuePair<string, string>("NO2", data.list[0].components.no2.ToString()),
                        new KeyValuePair<string, string>("O3", data.list[0].components.o3.ToString()),
                        new KeyValuePair<string, string>("SO2", data.list[0].components.so2.ToString()),
                        new KeyValuePair<string, string>("PM2.5", data.list[0].components.pm2_5.ToString()),
                        new KeyValuePair<string, string>("PM10", data.list[0].components.pm10.ToString()),
                        new KeyValuePair<string, string>("NH3", data.list[0].components.nh3.ToString()),
                        new KeyValuePair<string, string>("DateTime (UTC)", DateTimeOffset.FromUnixTimeSeconds(data.list[0].dt).UtcDateTime.ToString("u"))
                    };

                    // Store the result in Session
                    Session["AirQuality"] = resultList;
                }
                else
                {
                    ViewBag.Error = "AirQuality failed. Please get a valid coordinate and try again.";
                }

                return View("TryIt");

            }
            catch (Exception)
            {
                ViewBag.Error = "AirQuality failed. Please get a valid coordinate and try again.";
                return null;
            }
        }
    }
}