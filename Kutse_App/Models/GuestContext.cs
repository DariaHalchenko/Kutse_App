using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Kutse_App.Models
{
    public class GuestContext : DbContext
    {
        public DbSet<Guest> Guests { get; set; }
        public DbSet<Puhad> Puhads { get; set; } // Таблица для праздников
    }
}