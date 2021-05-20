using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using WeatherForecast.Models;

namespace WeatherForecast.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Weather()
        {

            return View();
        }

        public ActionResult Index()
        {
            return View();
        }    

        [HttpPost]
        public String WeatherDetail(string City)
        {    
            //unique API string
            string appId = "3b9dde8b948805a7d3619e767edb2982";

            //gather city and appId and insert it into the given url             
            string url = string.Format("http://api.openweathermap.org/data/2.5/weather?q={0}&units=metric&cnt=1&APPID={1}", City, appId);

            using (WebClient client = new WebClient())
            {
                try
                {
                    string json = client.DownloadString(url);
                    
                    //serialize and deserialize the data that is passed between the browser and the Web server
                    RootObject weatherInfo = (new JavaScriptSerializer()).Deserialize<RootObject>(json);

                    //instantiate
                    ResultViewModel result = new ResultViewModel();

                    //display information into defined fields
                    result.Country = weatherInfo.sys.country;
                    result.City = weatherInfo.name;
                    result.Lat = Convert.ToString(weatherInfo.coord.lat);
                    result.Lon = Convert.ToString(weatherInfo.coord.lon);
                    result.Description = weatherInfo.weather[0].description;
                    result.Humidity = Convert.ToString(weatherInfo.main.humidity);
                    result.Temp = Convert.ToString(weatherInfo.main.temp);
                    result.TempFeelsLike = Convert.ToString(weatherInfo.main.feels_like);
                    result.TempMax = Convert.ToString(weatherInfo.main.temp_max);
                    result.TempMin = Convert.ToString(weatherInfo.main.temp_min);
                    result.WeatherIcon = weatherInfo.weather[0].icon;
                    result.CountryFlag = weatherInfo.weather[0].flag;
                    
                    var jsonstring = new JavaScriptSerializer().Serialize(result);
                  
                    return jsonstring;
                }
                catch
                {
                    return "Does not exist";
                }

            }
        }

    }
}