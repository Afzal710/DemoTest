using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoTest.Models
{
    public class StateModel
    {
        public int state_id { get; set; }
        public string state_name { get; set; }
        public int country_id { get; set; }
        public string country_name { get; set; }
    }
}