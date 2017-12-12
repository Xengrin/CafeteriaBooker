using CafeteriaBooker.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CafeteriaBooker.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base() { }

        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Table> Tables { get; set; }
    }
}