using System;
using System.Collections.Generic;
using System.Text;

using System.Globalization;

namespace Share.Generators
{
    public static class DateTimeGenerator
    {
        public static string GetShamsiDate(DateTime dateTime)
        {
            // 0000/00/00

            PersianCalendar pc = new PersianCalendar();

            return pc.GetYear(dateTime).ToString("0000") + "/" +
                   pc.GetMonth(dateTime).ToString("00") + "/" +
                   pc.GetDayOfMonth(dateTime).ToString("00");
        }

        public static string GetShamsiTime()
        {  
            PersianCalendar pc = new PersianCalendar();

            return pc.GetHour(DateTime.Now).ToString("00") + ":" +
                   pc.GetMinute(DateTime.Now).ToString("00") + ":" +
                   pc.GetSecond(DateTime.Now).ToString("00");
        }

        public static DateTime ConvertShamsiToMilady(string date)
        {
            DateTime dt= DateTime.Now;
            if (!string.IsNullOrEmpty(date))
            {
                PersianCalendar pc = new PersianCalendar();
                int year = Convert.ToInt32(date.Substring(0, 4));
                int month = Convert.ToInt32(date.Substring(5, 2));
                int day = Convert.ToInt32(date.Substring(8, 2));

                dt = new DateTime(year, month, day, pc);
            } 

            return dt;
        }
    }
}
