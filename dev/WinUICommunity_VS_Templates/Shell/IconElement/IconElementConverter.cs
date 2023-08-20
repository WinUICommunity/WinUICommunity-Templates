using System;
using System.ComponentModel;
using System.Globalization;

namespace WinUICommunity.Shell
{
    public class IconElementConverter : TypeConverter
    {
        public IconElementConverter()
        {
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return false;
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            throw GetConvertFromException(value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            throw GetConvertToException(value, destinationType);
        }
    }
}
