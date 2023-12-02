using Fintranet.Test.Application.Abstractions.Repositories;
using Fintranet.Test.Domain.Entities;
using Fintranet.Test.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fintranet.Test.Infrastructure.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        ApplicationDBContext _context;

        public VehicleRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public void AddRangeVehicle(IEnumerable<Vehicle> vehicles)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                foreach (var vehicle in vehicles)
                {
                    var vehicleRes = _context.Vehicles.FirstOrDefault(it => it.VehicleType == vehicle.VehicleType);
                    if (vehicleRes == null)
                    {
                        _context.Vehicles.Add(vehicle);
                        _context.SaveChanges();
                    }
                }
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
            }
        }

        public async Task AddVehicle(Vehicle vehicle)
        {
            await _context.Vehicles.AddAsync(vehicle);
            await _context.SaveChangesAsync();
        }
    }
}
