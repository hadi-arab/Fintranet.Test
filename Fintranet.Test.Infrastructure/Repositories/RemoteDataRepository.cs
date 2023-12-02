using Fintranet.Test.Application.Abstractions.Repositories;
using Fintranet.Test.Domain.Entities;
using Fintranet.Test.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XAct;

namespace Fintranet.Test.Infrastructure.Repositories
{
    public class RemoteDataRepository : IRemoteDataRepository
    {
        ApplicationDBContext _context;

        public RemoteDataRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public void AddRangeCongestionFees(IEnumerable<CongestionFee> congestionFees)
        {
            DateTime date = DateTime.UtcNow;
            congestionFees.ForEach(congestionFee =>
            {
                congestionFee.RegisterDate = date;
            });
            _context.CongestionFees.AddRange(congestionFees);
            _context.SaveChanges();
        }

        public void AddRangeCongestionRules(IEnumerable<CongestionRule> congestionRules)
        {
            DateTime date = DateTime.UtcNow;
            congestionRules.ForEach(congestionRule =>
            {
                congestionRule.RegisterDate = date;
            });
            _context.CongestionRules.AddRange(congestionRules);
            _context.SaveChanges();
        }

        public void AddRangeTollFreeVehicles(IEnumerable<TollFreeVehicle> tollFreeVehicles)
        {
            DateTime date = DateTime.UtcNow;
            tollFreeVehicles.ForEach(tollFreeVehicle =>
            {
                tollFreeVehicle.RegisterDate = date;
            });
            _context.TollFreeVehicles.AddRange(tollFreeVehicles);
            _context.SaveChanges();
        }

        public void AddRangeVehicles(IEnumerable<Vehicle> vehicles)
        {
            DateTime date = DateTime.UtcNow;
            vehicles.ForEach(vehicle =>
            {
                vehicle.RegisterDate = date;
            });
            _context.Vehicles.AddRange(vehicles);
            _context.SaveChanges();
        }

        public void RemoveAllCongestionFees()
        {
            var congestionFees = _context.CongestionFees.AsEnumerable();
            _context.CongestionFees.RemoveRange(congestionFees);
            _context.SaveChanges();
        }

        public void RemoveAllCongestionRules()
        {
            var congestionRules = _context.CongestionRules.AsEnumerable();
            _context.CongestionRules.RemoveRange(congestionRules);
            _context.SaveChanges();
        }

        public void RemoveAllTollFreeVehicles()
        {
            var tollFreeVehicles = _context.TollFreeVehicles.AsEnumerable();
            _context.TollFreeVehicles.RemoveRange(tollFreeVehicles);
            _context.SaveChanges();
        }

        public void RemoveAllVehicles()
        {
            var vehicles = _context.Vehicles.AsEnumerable();
            _context.Vehicles.RemoveRange(vehicles);
            _context.SaveChanges();
        }
    }
}
