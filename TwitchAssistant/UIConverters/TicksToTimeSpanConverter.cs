using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace TwitchAssistant.UIConverters
{
    public class TicksToTimeSpanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var ticks = (long?)value;
            if (ticks == null)
            {
                return new TimeSpan();
            }
            return new TimeSpan((long)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string x = value as string;
            TimeSpan time;
            if (TimeSpan.TryParse(x, out time))
            {
                return time.Ticks;
            }
            else
            {
                return  new TimeSpan();
            }
           
        }
    }
}
