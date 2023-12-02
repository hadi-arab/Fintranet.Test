using System;
using System.Collections.Generic;
using System.Linq;
using Fintranet.Test.Domain.Entities;
using Fintranet.Test.Domain.Enums;
using Microsoft.Extensions.Options;

namespace Fintranet.Test.Application.Tools.Calculator
{
    public class CongestionTaxCalculator
    {
        private CongestionTaxCalculatorOptions _options;

        /**
             * Calculate the total toll fee for one day and more than one day
             *
             * @param vehicle - the vehicle
             * @param dates   - date and time of all passes on one day
             * @return - the total congestion tax for that day
             */
        public CongestionTaxCalculator(CongestionTaxCalculatorOptions options)
        {
            _options = options;
        }

        public int GetTax()
        {
            if (_options.CongestionFeeRule == null || !_options.CongestionFeeRule.Any())
            {
                throw new ArgumentNullException("CongestionFeeRule is null.");
            }

            if (_options.Dates == null || _options.Dates.Length == 0)
            {
                throw new ArgumentNullException("GetTax => Vehicle and Dates parameters are required.");
            }

            if (_options.IsVehicleTollFree)
            {
                return 0;
            }

            int totalFee = 0;
            var firstDate = _options.Dates[0].Value;
            int firstFee = GetTollFee(firstDate);
            long multiTollingLimit = 0;
            int maxFee = firstFee;
            totalFee += maxFee;
            int oneDayFee = firstFee;
            for (int i = 1; i < _options.Dates.Length; i++)
            {
                var secondDate = _options.Dates[i].Value;
                int secondFee = GetTollFee(secondDate);

                long diffBetweenTwoStationsInMillies = firstDate.GetDiffInMilliseconds(secondDate);
                long diffBetweenTwoStationsInMinutes = DateTimeExtension.GetMinutesFromMilliseconds(diffBetweenTwoStationsInMillies);
                multiTollingLimit += diffBetweenTwoStationsInMinutes;

                if (multiTollingLimit <= _options.SeveralTollingStationsLimitInMinutes)
                {
                    if (secondFee >= maxFee)
                    {
                        if (totalFee > 0)
                        {
                            totalFee -= maxFee;
                            oneDayFee -= maxFee;
                        }
                        maxFee = secondFee;
                        totalFee += maxFee;
                        oneDayFee += maxFee;
                    }
                }
                else
                {
                    maxFee = secondFee;
                    totalFee += secondFee;
                    oneDayFee += secondFee;
                    firstDate = secondDate;
                    multiTollingLimit = 0;
                }

                if (firstDate.IsOnTheSameDay(secondDate))
                {
                    oneDayFee -= secondFee;
                }
                else
                {
                    oneDayFee = secondFee;
                }

                if (oneDayFee > _options.MaxCongestionTaxLimitForOneDay)
                {
                    var onDayFeeDiff = oneDayFee - _options.MaxCongestionTaxLimitForOneDay;
                    totalFee -= onDayFeeDiff;
                    oneDayFee = _options.MaxCongestionTaxLimitForOneDay;
                }
                
            }

            return totalFee;
        }

        private int GetTollFee(DateTime date)
        {
            if (IsTollFreeDate(date)) return 0;

            int hour = date.Hour;
            int minute = date.Minute;

            foreach (var rule in _options.CongestionFeeRule)
            {
                if (rule.IsInBound(hour, minute))
                {
                    return rule.Fee;
                }
            }
            return 0;
        }

        private bool IsTollFreeDate(DateTime date)
        {
            if (date.IsWeekend()
                || date.IsHoliday()
                || date.AddDays(1).IsHoliday()
                || date.Month == 7
                || date.Year != _options.CongestionYearLimit)
            {
                return true;
            }
            return false;
        }
    }


}