using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using Newtonsoft.Json;


namespace ApiExample_OpenWeatherMap
{
    class Program
    {
        static void Main(string[] args)
        {
            string cityName = Console.ReadLine();
            Console.WriteLine();
            string DataLine = null;
            try
            {
                string url = $"http://api.openweathermap.org/data/2.5/weather?q={cityName}&units=metric&APPID=29335250d49f5b97dcea0451c626ef47";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                string responseStr;

                using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                {
                    responseStr = sr.ReadToEnd();

                }
                Weather weatherResponse = JsonConvert.DeserializeObject<Weather>(responseStr);

                DateTime sunset = UnixTimeStampToDateTime(weatherResponse.sys.Sunset);
                DateTime sunrise = UnixTimeStampToDateTime(weatherResponse.sys.Sunrise);

                DataLine = $"Температура в {weatherResponse.Name} = {weatherResponse.main.Temp} °C \nВлажность {weatherResponse.main.Humidity}% \nВосход {sunrise.Hour}:{sunrise.Minute} \nЗакат {sunset.Hour}:{sunset.Minute}";
                Console.WriteLine(DataLine);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return;
            }

            // current directory
            using (StreamWriter sw = new StreamWriter($"{ DateTime.Now.Day }.{ DateTime.Now.Month}.{ DateTime.Now.Year}.txt"))
            {
                sw.WriteLine(DataLine);
            }
        }

        public static DateTime UnixTimeStampToDateTime(int unixTimeStamp)
        {
            DateTime dtDateTime = new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

    }
}
