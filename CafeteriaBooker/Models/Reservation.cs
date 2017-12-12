using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CafeteriaBooker.Models
{
    public class Reservation
    {
        public int ID { get; set; }
        public DateTime Date { get; set; } // We assume that reservation is for one hour
        public String Name { get; set; }
        public int TableID { get; set; }

        [JsonIgnore]
        public virtual Table Table { get; set; }

    }
}