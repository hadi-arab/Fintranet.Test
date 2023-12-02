using Fintranet.Test.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fintranet.Test.Application.Abstractions.Repositories
{
    public interface IRemoteDataRepository
    {
        void RemoveAllCongestionRules();
        void AddRangeCongestionRules(IEnumerable<CongestionRule> congestionRules);

        void RemoveAllCongestionFees();
        void AddRangeCongestionFees(IEnumerable<CongestionFee> congestionFees);

        void RemoveAllVehicles();
        void AddRangeVehicles(IEnumerable<Vehicle> vehicles);

        void RemoveAllTollFreeVehicles();
        void AddRangeTollFreeVehicles(IEnumerable<TollFreeVehicle> tollFreeVehicles);
    }
}
