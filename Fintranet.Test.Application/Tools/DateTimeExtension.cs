using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Fintranet.Test.Application.Tools
{
    public static class DateTimeExtension
    {
        public static bool IsHoliday(this DateTime dateTime)
        {
            DateTime now = DateTime.Now;
            DateTime firstDayOFYear = new DateTime(dateTime.Year, 1, 1);

            try
            {
                string xmlFile = Path.Combine(Directory.GetCurrentDirectory(), @"Holidays.xml");
                HolidayCalculator hc = new HolidayCalculator(firstDayOFYear, xmlFile);
                int day = dateTime.Day;
                int month = dateTime.Month;
                foreach (HolidayCalculator.Holiday h in hc.OrderedHolidays)
                {
                    if (day == h.Date.Day
                        && month == h.Date.Month)
                    {
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("IsHoliday", e);
            }
            return false;
        }

        public static long GetDiffInMilliseconds(this DateTime startDate, DateTime toDate)
        {
            long res = 0;
            if(startDate >= toDate)
            {
                return res;
            }

            res = (long)(toDate - startDate).TotalMilliseconds;
            return res;
        }

        public static long GetMinutesFromMilliseconds(long milliseconds)
        {
            long minutes = milliseconds / 1000 / 60;
            return minutes;
        }
        public static bool IsWeekend(this DateTime date)
        {
            if(date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
            {
                return true;
            }
            return false;
        }

        public static bool IsOnTheSameDay(this DateTime first, DateTime second)
        {
            if(first.Year == second.Year
                && first.Month == second.Month
                && first.Day == second.Day)
            {
                return true;
            }
            return false;
        }
    }
}
