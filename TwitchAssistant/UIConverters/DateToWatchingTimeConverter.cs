﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace TwitchAssistant.UIConverters
{
    public class DateToWatchingTimeConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var watchingTime = value as DateTime?;      
            if (watchingTime == null)
            {
                return "";
            }
            DateTime? startingPoint = new DateTime();
            TimeSpan different =  watchingTime.Value.Subtract(startingPoint.Value);
            return $"{different.Days} days {different:hh\\:mm\\:ss} hours";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new DateTime();
        }

    }


 
}
