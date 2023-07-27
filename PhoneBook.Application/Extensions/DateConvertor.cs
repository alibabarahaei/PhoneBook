using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Application.Extensions
{
    public static class DateConvertor
    {

        public static string ConvertCreationDataToPeriodDate(this DateTime time)
        {
            var subtime = DateTime.Now.Subtract(time).TotalMinutes;

             
            if (subtime<60)
            {
                return $"{(int)subtime} دقیفه";
            }
            else if(subtime is >= 60 and < 24*60)
            {
                return $"{(int)subtime/60} ساعت";
            }
            else if(subtime is >=24*60 and <24*60*365 )
            {
                return $"{(int)subtime/  (24 * 60)} روز";
            }
            else if (subtime >= 24 * 60 * 365)
            {
                return $"{(int)subtime / (24 * 60*365)} سال";
            }
            else
            {
                return null;
            }

        }
        public static string ToShamsi(this DateTime value)
        {
            PersianCalendar pc = new PersianCalendar();
            return pc.GetYear(value) + "/" +
                   pc.GetMonth(value).ToString("00") + "/" +
                   pc.GetDayOfMonth(value).ToString("00");
        }
    }
}
