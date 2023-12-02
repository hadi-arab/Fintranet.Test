using Fintranet.Test.Application.Abstractions.Repositories;
using Fintranet.Test.Domain.Entities;
using Fintranet.Test.Domain.Enums;
using Fintranet.Test.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fintranet.Test.Infrastructure.Repositories
{
    public class TollFreeVehicleRepository : ITollFreeVehicleRepository
    {
        ApplicationDBContext _context;

        public TollFreeVehicleRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public void AddRangeTollFreeVehicle(IEnumerable<VehicleType> vehicleTypes)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                foreach (var vehicleType in vehicleTypes)
                {
                    var vehicleRes = _context.Vehicles.FirstOrDefault(it => it.VehicleType == vehicleType);
                    if (vehicleRes != null)
                    {
                        var tollFreeVehicleRes = _context.TollFreeVehicles.FirstOrDefault(it => it.VehicleId == vehicleRes.Id);
                        if (tollFreeVehicleRes == null)
                        {
                            _context.TollFreeVehicles.Add(new TollFreeVehicle()
                            {
                                RegisterDate = DateTime.UtcNow,
                                VehicleId = vehicleRes.Id,
                            });
                            _context.SaveChanges();
                        }
                    }
                }
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
            }
        }

        public async Task AddTollFreeVehicle(TollFreeVehicle tollFreeVehicle)
        {
            await _context.TollFreeVehicles.AddAsync(tollFreeVehicle);
            await _context.SaveChangesAsync();
        }

        public bool IsVehicleTollFree(int vehicleId)
        {
            var tollFreeVehicle = _context.TollFreeVehicles.FirstOrDefault(it => it.VehicleId.Equals(vehicleId));
            if (tollFreeVehicle != null)
            {
                return true;
            }
            return false;
        }
    }
}
