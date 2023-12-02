using Fintranet.Test.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fintranet.Test.Infrastructure.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }
        
        public DbSet<CongestionRule> CongestionRules { get; set; }
        public DbSet<CongestionFee> CongestionFees { get; set; }
        public DbSet<CongestionTaxCalculation> CongestionTaxCalculations { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<TollFreeVehicle> TollFreeVehicles { get; set; }

    }
}
