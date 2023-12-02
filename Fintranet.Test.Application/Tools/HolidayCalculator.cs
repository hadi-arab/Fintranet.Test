using Nager.Date;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Fintranet.Test.Application.Tools
{
    public class HolidayCalculator
    {
        public class Holiday : IComparable
        {
            public DateTime Date;

            public string Name;

            public int CompareTo(object obj)
            {
                if (obj is Holiday)
                {
                    Holiday holiday = (Holiday)obj;
                    return Date.CompareTo((object)holiday.Date);
                }

                throw new ArgumentException("Object is not a Holiday");
            }
        }

        private ArrayList orderedHolidays;

        private XmlDocument xHolidays;

        private DateTime startingDate;

        public ArrayList OrderedHolidays => orderedHolidays;

        public HolidayCalculator(DateTime startDate, string xmlPath)
        {
            startingDate = startDate;
            orderedHolidays = new ArrayList();
            xHolidays = new XmlDocument();
            xHolidays.Load(xmlPath);
            processXML();
        }

        private void processXML()
        {
            foreach (XmlNode item in xHolidays.SelectNodes("/Holidays/Holiday"))
            {
                Holiday holiday = processNode(item);
                if (holiday.Date.Year > 1)
                {
                    orderedHolidays.Add(holiday);
                }
            }

            orderedHolidays.Sort();
        }

        private Holiday processNode(XmlNode n)
        {
            Holiday holiday = new Holiday();
            holiday.Name = n.Attributes["name"].Value.ToString();
            ArrayList arrayList = new ArrayList();
            foreach (XmlNode childNode in n.ChildNodes)
            {
                arrayList.Add(childNode.Name.ToString());
            }

            if (arrayList.Contains("WeekOfMonth"))
            {
                int month = int.Parse(n.SelectSingleNode("./Month").InnerXml.ToString());
                int week = int.Parse(n.SelectSingleNode("./WeekOfMonth").InnerXml.ToString());
                int weekday = int.Parse(n.SelectSingleNode("./DayOfWeek").InnerXml.ToString());
                holiday.Date = getDateByMonthWeekWeekday(month, week, weekday, startingDate);
            }
            else if (arrayList.Contains("DayOfWeekOnOrAfter"))
            {
                int num = int.Parse(n.SelectSingleNode("./DayOfWeekOnOrAfter/DayOfWeek").InnerXml.ToString());
                if (num > 6 || num < 0)
                {
                    throw new Exception("DOW is greater than 6");
                }

                int m = int.Parse(n.SelectSingleNode("./DayOfWeekOnOrAfter/Month").InnerXml.ToString());
                int d = int.Parse(n.SelectSingleNode("./DayOfWeekOnOrAfter/Day").InnerXml.ToString());
                holiday.Date = getDateByWeekdayOnOrAfter(num, m, d, startingDate);
            }
            else if (arrayList.Contains("WeekdayOnOrAfter"))
            {
                int num2 = int.Parse(n.SelectSingleNode("./WeekdayOnOrAfter/Month").InnerXml.ToString());
                int num3 = int.Parse(n.SelectSingleNode("./WeekdayOnOrAfter/Day").InnerXml.ToString());
                DateTime dateTime = DateTime.Parse(num2 + "/" + num3 + "/" + startingDate.Year);
                if (dateTime < startingDate)
                {
                    dateTime = dateTime.AddYears(1);
                }

                while (dateTime.DayOfWeek.Equals(DayOfWeek.Saturday) || dateTime.DayOfWeek.Equals(DayOfWeek.Sunday))
                {
                    dateTime = dateTime.AddDays(1.0);
                }

                holiday.Date = dateTime;
            }
            else if (arrayList.Contains("LastFullWeekOfMonth"))
            {
                int num4 = int.Parse(n.SelectSingleNode("./LastFullWeekOfMonth/Month").InnerXml.ToString());
                int num5 = int.Parse(n.SelectSingleNode("./LastFullWeekOfMonth/DayOfWeek").InnerXml.ToString());
                DateTime dateByMonthWeekWeekday = getDateByMonthWeekWeekday(num4, 5, num5, startingDate);
                if (dateByMonthWeekWeekday.AddDays(6 - num5).Month == num4)
                {
                    holiday.Date = dateByMonthWeekWeekday;
                }
                else
                {
                    holiday.Date = dateByMonthWeekWeekday.AddDays(-7.0);
                }
            }
            else if (arrayList.Contains("DaysAfterHoliday"))
            {
                XmlNode n2 = xHolidays.SelectSingleNode("/Holidays/Holiday[@name='" + n.SelectSingleNode("./DaysAfterHoliday").Attributes["Holiday"].Value.ToString() + "']");
                Holiday holiday2 = processNode(n2);
                int num6 = int.Parse(n.SelectSingleNode("./DaysAfterHoliday/Days").InnerXml.ToString());
                holiday.Date = holiday2.Date.AddDays(num6);
            }
            else if (arrayList.Contains("Easter"))
            {
                holiday.Date = easter();
            }
            else if (arrayList.Contains("Month") && arrayList.Contains("Day"))
            {
                int num7 = int.Parse(n.SelectSingleNode("./Month").InnerXml.ToString());
                int num8 = int.Parse(n.SelectSingleNode("./Day").InnerXml.ToString());
                DateTime dateTime2 = DateTime.Parse(num7 + "/" + num8 + "/" + startingDate.Year);
                if (dateTime2 < startingDate)
                {
                    dateTime2 = dateTime2.AddYears(1);
                }

                if (arrayList.Contains("EveryXYears"))
                {
                    int num9 = int.Parse(n.SelectSingleNode("./EveryXYears").InnerXml.ToString());
                    int num10 = int.Parse(n.SelectSingleNode("./StartYear").InnerXml.ToString());
                    if ((dateTime2.Year - num10) % num9 == 0)
                    {
                        holiday.Date = dateTime2;
                    }
                }
                else
                {
                    holiday.Date = dateTime2;
                }
            }

            return holiday;
        }

        private DateTime easter()
        {
            DateTime firstDayOfMonth = getFirstDayOfMonth(startingDate);
            int num = firstDayOfMonth.Year;
            if (firstDayOfMonth.Month > 4)
            {
                num++;
            }

            return easter(num);
        }

        private DateTime easter(int y)
        {
            int num = y % 19;
            int num2 = y / 100;
            int num3 = y % 100;
            int num4 = num2 / 4;
            int num5 = num2 % 4;
            int num6 = (num2 + 8) / 25;
            int num7 = (num2 - num6 + 1) / 3;
            int num8 = (19 * num + num2 - num4 - num7 + 15) % 30;
            int num9 = num3 / 4;
            int num10 = num3 % 4;
            int num11 = (32 + 2 * num5 + 2 * num9 - num8 - num10) % 7;
            int num12 = (num + 11 * num8 + 22 * num11) / 451;
            int month = (num8 + num11 - 7 * num12 + 114) / 31;
            int num13 = (num8 + num11 - 7 * num12 + 114) % 31;
            int day = num13 + 1;
            DateTime dateTime = new DateTime(y, month, day);
            if (dateTime < startingDate)
            {
                return easter(y + 1);
            }

            return new DateTime(y, month, day);
        }

        private DateTime getDateByWeekdayOnOrAfter(int weekday, int m, int d, DateTime startDate)
        {
            DateTime dateTime = getFirstDayOfMonth(startDate);
            while (dateTime.Month != m)
            {
                dateTime = dateTime.AddMonths(1);
            }

            dateTime = dateTime.AddDays(d - 1);
            while (weekday != (int)dateTime.DayOfWeek)
            {
                dateTime = dateTime.AddDays(1.0);
            }

            if (dateTime < startingDate)
            {
                return getDateByWeekdayOnOrAfter(weekday, m, d, startDate.AddYears(1));
            }

            return dateTime;
        }

        private DateTime getDateByMonthWeekWeekday(int month, int week, int weekday, DateTime startDate)
        {
            DateTime dateTime = getFirstDayOfMonth(startDate);
            while (dateTime.Month != month)
            {
                dateTime = dateTime.AddMonths(1);
            }

            while (dateTime.DayOfWeek != (DayOfWeek)weekday)
            {
                dateTime = dateTime.AddDays(1.0);
            }

            DateTime dateTime2;
            if (week == 1)
            {
                dateTime2 = dateTime;
            }
            else
            {
                int num = week * 7 - 7;
                int num2 = dateTime.Day + num;
                if (num2 > DateTime.DaysInMonth(dateTime.Year, dateTime.Month))
                {
                    num2 -= 7;
                }

                dateTime2 = new DateTime(dateTime.Year, dateTime.Month, num2);
            }

            if (dateTime2 >= startingDate)
            {
                return dateTime2;
            }

            return getDateByMonthWeekWeekday(month, week, weekday, startDate.AddYears(1));
        }

        private DateTime getFirstDayOfMonth(DateTime dt)
        {
            return DateTime.Parse(dt.Month + "/1/" + dt.Year);
        }
    }
}
