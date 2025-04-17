using Assignment3RestService.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Assignment3RestService
{
    public class ServiceRest : IServiceRest
    {
        // implementation of IServiceRest

        // geocode service
        public async Task<Coordinate> GeoCodeAsync(string address)
        {
            try
            {
                // google geocode api key
                string apiKey = "AIzaSyCyq45wnX95eswqX_DhaB816cJJnjvtgXU";
                // request uri
                string requestUri = $"https://maps.googleapis.com/maps/api/geocode/json?address={Uri.EscapeDataString(address)}&key={apiKey}";
                // http client to send request
                using (var httpClient = new HttpClient())
                {
                    // send request
                    var response = await httpClient.GetAsync(requestUri);
                    // read response
                    var json = await response.Content.ReadAsStringAsync();
                    // parse json
                    dynamic result = JObject.Parse(json);
                    // check if status is ok
                    if (result.status == "OK")
                    {
                        // get latitude and longitude
                        var location = result.results[0].geometry.location;

                        Coordinate coordinate = new Coordinate
                        {
                            Latitude = location.lat,
                            Longitude = location.lng
                        };

                        // return coordinate
                        return coordinate;

                    }
                    // if status is not ok, return null
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<WeatherForecastRoot> WeatherForecastAsync(double latitude, double longitude)
        {
            try
            {
                // tomorrow.io weather api key
                string apiKey = "6u6Gtw3ZdR8zKdJEydUevg2uE5eaifu7";
                // request uri
                string baseUrl = "https://api.tomorrow.io/v4/weather/forecast";
                // add query parameters
                baseUrl += $"?location={latitude},{longitude}&apikey={apiKey}";

                // create http client
                var clientHandler = new HttpClientHandler
                {
                    // allow automatic decompression
                    AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip,
                };

                // create http client
                var client = new HttpClient(clientHandler);
                // set request parameters
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(baseUrl),
                    Headers =
                        {
                            { "accept", "application/json" },
                        },
                };

                // send request
                using (var response = await client.SendAsync(request))
                {
                    // check if response is successful
                    if (response.IsSuccessStatusCode)
                    {
                        // read response content
                        string content = await response.Content.ReadAsStringAsync();
                        // deserialize json response
                        WeatherForecastRoot weatherData = JsonConvert.DeserializeObject<WeatherForecastRoot>(content);
                        // return weather data
                        return weatherData;
                    }
                    else
                    {
                        // handle error response
                        throw new Exception($"Error: {response.StatusCode}");
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<AirQualityRoot> AirQualityAsync(double latitude, double longitude)
        {
            try
            {
                // tomorrow.io weather api key
                string apiKey = "0b24081f36e0b751873e52930e136f51";
                // request uri
                string baseUrl = "https://api.openweathermap.org/data/2.5/air_pollution";
                // add query parameters
                baseUrl += $"?lat={latitude}&lon={longitude}&appid={apiKey}";

                // create http client
                var clientHandler = new HttpClientHandler
                {
                    // allow automatic decompression
                    AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip,
                };

                // create http client
                var client = new HttpClient(clientHandler);
                // set request parameters
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(baseUrl),
                    Headers =
                        {
                            { "accept", "application/json" },
                        },
                };

                // send request
                using (var response = await client.SendAsync(request))
                {
                    // check if response is successful
                    if (response.IsSuccessStatusCode)
                    {
                        // read response content
                        string content = await response.Content.ReadAsStringAsync();
                        // Deserialize JSON string to C# object
                        var result = JsonConvert.DeserializeObject<AirQualityRoot>(content);
                        // return weather data
                        return result;
                    }
                    else
                    {
                        // handle error response
                        throw new Exception($"Error: {response.StatusCode}");
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}