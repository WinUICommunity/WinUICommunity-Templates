﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace iNKORE.UI.WPF.Modern
{
    public class IsGridViewConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is System.Windows.Controls.GridView;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
