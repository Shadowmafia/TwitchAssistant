using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace TwitchAssistant.UIConverters
{
    class TimespanToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            var time = (TimeSpan?)value;
            if (time == null)
            {
                return "20";
            }
            return time.Value.TotalSeconds;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {

            string x = value.ToString();           
            if (value != null)
            {
                int second=0;
                Int32.TryParse(x, out second);
                return new TimeSpan(0,0,second);
            }
            return  new TimeSpan();
        }
    }
}
