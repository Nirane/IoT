using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Bibliography;
using GUI.Models;
using Newtonsoft.Json;

namespace GUI.api
{
    public class ApiService
    {
        public List<SensorData> GetTempSensorData()
        {
            var responseBody = _makeHttpGet("http://api/sensors/temperature");

            List<SensorData> parsedList = JsonConvert.DeserializeObject<List<SensorData>>(responseBody);

            return parsedList;
        }

        public List<SensorData> GetHumiditySensorData()
        {
            var responseBody = _makeHttpGet("http://api/sensors/humidity");

            List<SensorData> parsedList = JsonConvert.DeserializeObject<List<SensorData>>(responseBody);

            return parsedList;
        }

        public List<SensorData> GetEthylenSensorData()
        {
            var responseBody = _makeHttpGet("http://api/sensors/methanol");

            List<SensorData> parsedList = JsonConvert.DeserializeObject<List<SensorData>>(responseBody);

            return parsedList;
        }

        public List<SensorData> GetPressureSensorData()
        {
            
            var responseBody = _makeHttpGet("http://api/sensors/pressure");

            List<SensorData> parsedList = JsonConvert.DeserializeObject<List<SensorData>>(responseBody);

            return parsedList;

        }
        
        public string _makeHttpGet(string url)
        {
            try
            {
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
                    true;

                HttpClient client = new HttpClient(clientHandler);
                Task<HttpResponseMessage> futureResponse = client.GetAsync(url);
                futureResponse.Wait();
                HttpResponseMessage response = futureResponse.Result;

                response.EnsureSuccessStatusCode();

                Task<string> futureString = response.Content.ReadAsStringAsync();
                futureString.Wait();
                return futureString.Result;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }

            return null;
        }
    }
}