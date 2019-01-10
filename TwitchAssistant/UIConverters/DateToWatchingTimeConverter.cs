using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace TwitchAssistant.UIConverters
{
    class DateToWatchingTimeConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TimeSpan? streamUpTime = value as TimeSpan?;
            string timeInString = " ";
            if (streamUpTime == null)
            {
                return timeInString;
            }
            return $"{streamUpTime.Value.Hours} : {streamUpTime.Value.Minutes} : {streamUpTime.Value.Seconds}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new TimeSpan();
        }
    }


 
}
