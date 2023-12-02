using System;
using System.Collections.Generic;
using System.Text;

namespace Fintranet.Test.Application.Tools.Calculator
{
    public class CongestionTaxCalculatorOptions
    {
        public DateTime?[] Dates { get; set; }

        public bool IsVehicleTollFree { get; set; }

        public int MaxCongestionTaxLimitForOneDay { get; set; }

        public int SeveralTollingStationsLimitInMinutes { get; set; }

        public int CongestionYearLimit { get; set; }

        public List<CongestionFeeRule> CongestionFeeRule { get; set; }
    }
}
