using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CafeteriaBooker.Models
{
    public class Table
    {
        public int ID { get; set; }
        public int Number { get; set; }
        public bool Smoke { get; set; }
        public int Seats { get; set; }

        [JsonIgnore]
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}