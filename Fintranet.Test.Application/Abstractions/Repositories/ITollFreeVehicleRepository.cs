using Fintranet.Test.Domain.Entities;
using Fintranet.Test.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fintranet.Test.Application.Abstractions.Repositories
{
    public interface ITollFreeVehicleRepository
    {
        Task AddTollFreeVehicle(TollFreeVehicle tollFreeVehicle);

        void AddRangeTollFreeVehicle(IEnumerable<VehicleType> vehicleTypes);

        bool IsVehicleTollFree(int vehicleId);
    }
}
