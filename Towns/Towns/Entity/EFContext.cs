using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Towns.Entity;

namespace Towns
{
    class EFContext : DbContext
    {
        public EFContext() : base("connectionName")
        {
        }
        public DbSet<Region> Regions { get; set; }
        public DbSet<TownType> TownTypes { get; set; }
        public DbSet<Town> Towns { get; set; }
    }
}
