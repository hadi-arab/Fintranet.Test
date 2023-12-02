using Fintranet.Test.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fintranet.Test.Application.Tools
{
    public class CongestionFeeRule
    {
        public static List<CongestionFeeRule> ConvertCongestionFeeRule(List<CongestionFee> congestionFees)
        {
            if(congestionFees == null || !congestionFees.Any())
            {
                throw new ArgumentNullException("ConvertCongestionFeeRule => Congestion Fee in Empty.");
            }

            List<CongestionFeeRule> list = new List<CongestionFeeRule>();

            foreach (var congestionFee in congestionFees)
            {
                var startTimeSplit = congestionFee.StartTime.Split(':');
                var endTimeSplit = congestionFee.EndTime.Split(':');

                CongestionFeeRule fee = new CongestionFeeRule()
                {
                    Fee = congestionFee.Fee,
                    FromHour = int.Parse(startTimeSplit[0]),
                    FromMinute = int.Parse(startTimeSplit[1]),
                    ToHour = int.Parse(endTimeSplit[0]),
                    ToMinute = int.Parse(endTimeSplit[1]),
                };
                list.Add(fee);
            }

            return list;
        }

        public int FromHour { get; set; }
        public int FromMinute { get; set; }
        public int ToHour { get; set; }
        public int ToMinute { get; set; }
        public int Fee { get; set; }

        public bool IsInBound(int hour, int minute)
        {
            if (hour >= FromHour && hour <= ToHour
                && minute >= FromMinute && minute <= ToMinute)
            {
                return true;
            }
            return false;
        }
    }
}
