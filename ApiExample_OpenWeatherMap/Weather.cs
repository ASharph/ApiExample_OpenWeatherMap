using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiExample_OpenWeatherMap
{
    class Weather
    {
        public MainInfo main { get; set; }
        public Sys sys { get; set; }
        public string Name { get; set; }
    }
}
