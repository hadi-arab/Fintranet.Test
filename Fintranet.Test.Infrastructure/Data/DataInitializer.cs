using Fintranet.Test.Application.Abstractions.Repositories;
using Fintranet.Test.Domain.Entities;
using Fintranet.Test.Domain.Enums;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fintranet.Test.Infrastructure.Data
{
    // old initializer - not being used after adding outside data part
    public class DataInitializer
    {
        public static void AddCongestionRules(ICongestionRuleRepository congestionRuleRepository , IConfiguration configuration)
        {
            List<CongestionRule> congestionRules = new List<CongestionRule>();
            
            string MaxCongestionTaxLimitInSEK = getMaxCongestionTaxLimitInSEKFromConfiguration(configuration);
            congestionRules.Add(new CongestionRule()
            {
                Key = "MaxCongestionTaxLimitInSEK",
                Value = MaxCongestionTaxLimitInSEK,
                RegisterDate = DateTime.UtcNow,
            });

            string SeveralTollingStationsPeriodInMinutes = getSeveralTollingStationsPeriodInMinutesFromConfiguration(configuration);
            congestionRules.Add(new CongestionRule()
            {
                Key = "SeveralTollingStationsPeriodInMinutes",
                Value = SeveralTollingStationsPeriodInMinutes,
                RegisterDate = DateTime.UtcNow,
            });

            string CongestionCalculationYearLimit = getCongestionCalculationYearLimitFromConfiguration(configuration);
            congestionRules.Add(new CongestionRule()
            {
                Key = "CongestionCalculationYearLimit",
                Value = CongestionCalculationYearLimit,
                RegisterDate = DateTime.UtcNow,
            });

            congestionRuleRepository.AddRangeCongestionRule(congestionRules);
        }

        private static string getMaxCongestionTaxLimitInSEKFromConfiguration(IConfiguration configuration)
        {
            var MaxCongestionTaxLimitInSEK = configuration.GetSection("CongestionRules:MaxCongestionTaxLimitInSEK").Get<string>();
            if (string.IsNullOrWhiteSpace(MaxCongestionTaxLimitInSEK))
            {
                throw new Exception("CongestionRules:MaxCongestionTaxLimitInSEK is not set.");
            }
            return MaxCongestionTaxLimitInSEK;
        }

        private static string getSeveralTollingStationsPeriodInMinutesFromConfiguration(IConfiguration configuration)
        {
            var SeveralTollingStationsPeriodInMinutes = configuration.GetSection("CongestionRules:SeveralTollingStationsPeriodInMinutes").Get<string>();
            if (string.IsNullOrWhiteSpace(SeveralTollingStationsPeriodInMinutes))
            {
                throw new Exception("CongestionRules:SeveralTollingStationsPeriodInMinutes is not set.");
            }
            return SeveralTollingStationsPeriodInMinutes;
        }

        private static string getCongestionCalculationYearLimitFromConfiguration(IConfiguration configuration)
        {
            var CongestionCalculationYearLimit = configuration.GetSection("CongestionRules:CongestionCalculationYearLimit").Get<string>();
            if (string.IsNullOrWhiteSpace(CongestionCalculationYearLimit))
            {
                throw new Exception("CongestionRules:CongestionCalculationYearLimit is not set.");
            }
            return CongestionCalculationYearLimit;
        }


        public static void AddVehicles(IVehicleRepository vehicleRepository)
        {
            List<Vehicle> vehicles = new List<Vehicle>();

            foreach (int i in Enum.GetValues(typeof(VehicleType)))
            {
                String name = Enum.GetName(typeof(VehicleType), i);

                vehicles.Add(new Vehicle()
                {
                    Name = name,
                    RegisterDate = DateTime.UtcNow,
                    VehicleType = (VehicleType)i
                });
            }

            vehicleRepository.AddRangeVehicle(vehicles);
        }

        public static void AddTollFreeVehicles(ITollFreeVehicleRepository tollFreeVehicleRepository)
        {
            List<VehicleType> vehicles = new List<VehicleType>
            {
                VehicleType.Emergency,
                VehicleType.Bus,
                VehicleType.Diplomat,
                VehicleType.Motorcycle,
                VehicleType.Military,
                VehicleType.Foreign
            };

            tollFreeVehicleRepository.AddRangeTollFreeVehicle(vehicles);
        }

        public static void AddCongestionFees(ICongestionFeeRepository congestionFeeRepository)
        {
            List<CongestionFee> congestionFees = new List<CongestionFee>()
            {
                new CongestionFee()
                {
                    StartTime = "06:00",
                    EndTime = "06:29",
                    Fee = 8,
                    RegisterDate = DateTime.UtcNow,
                    Year = 2013
                },
                new CongestionFee()
                {
                    StartTime = "06:30",
                    EndTime = "06:59",
                    Fee = 13,
                    RegisterDate = DateTime.UtcNow,
                    Year = 2013
                },
                new CongestionFee()
                {
                    StartTime = "07:00",
                    EndTime = "07:59",
                    Fee = 18,
                    RegisterDate = DateTime.UtcNow,
                    Year = 2013
                },
                new CongestionFee()
                {
                    StartTime = "08:00",
                    EndTime = "08:29",
                    Fee = 13,
                    RegisterDate = DateTime.UtcNow,
                    Year = 2013
                },
                new CongestionFee()
                {
                    StartTime = "08:30",
                    EndTime = "14:59",
                    Fee = 8,
                    RegisterDate = DateTime.UtcNow,
                    Year = 2013
                },
                new CongestionFee()
                {
                    StartTime = "15:00",
                    EndTime = "15:29",
                    Fee = 13,
                    RegisterDate = DateTime.UtcNow,
                    Year = 2013
                },
                new CongestionFee()
                {
                    StartTime = "15:30",
                    EndTime = "16:59",
                    Fee = 18,
                    RegisterDate = DateTime.UtcNow,
                    Year = 2013
                },
                new CongestionFee()
                {
                    StartTime = "17:00",
                    EndTime = "17:59",
                    Fee = 13,
                    RegisterDate = DateTime.UtcNow,
                    Year = 2013
                },
                new CongestionFee()
                {
                    StartTime = "18:00",
                    EndTime = "18:29",
                    Fee = 8,
                    RegisterDate = DateTime.UtcNow,
                    Year = 2013
                },
                new CongestionFee()
                {
                    StartTime = "18:30",
                    EndTime = "05:59",
                    Fee = 0,
                    RegisterDate = DateTime.UtcNow,
                    Year = 2013
                }
            };

            congestionFeeRepository.AddRangeCongestionFee(congestionFees);
        }

    }
}
