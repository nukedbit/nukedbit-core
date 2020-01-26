using System;
using System.Globalization;
using Xamarin.Forms;

namespace NukedBit.Forms.Core
{
    public static class ConverterExtensions
    {
        public static void MakeConverter(this ResourceDictionary dic,
            string key,
            ConvertDelegate convert, ConvertBackDelegate convertBack = null)
        {
            var conv = new ValueConverterGenerator(convert, convertBack);
            dic.Add(key, conv);
        }

        public static void MakeConverter(this ResourceDictionary dic,
            string key,
            Func<object, object, object> convert, ConvertBackDelegate convertBack = null)
        {
            var conv = new ValueConverterGenerator((object value, Type targetType, object parameter, CultureInfo culture) =>
            {
                return convert(value, parameter);
            }
                , convertBack);
            dic.Add(key, conv);
        }

        public delegate object ConvertDelegate(object value, Type targetType, object parameter, CultureInfo culture);
        public delegate object ConvertBackDelegate(object value, Type targetType, object parameter, CultureInfo culture);


        sealed class ValueConverterGenerator : IValueConverter
        {
            private readonly ConvertDelegate convert;
            private readonly ConvertBackDelegate convertBack;


            public ValueConverterGenerator(ConvertDelegate convert, ConvertBackDelegate convertBack)
            {
                this.convert = convert;
                this.convertBack = convertBack;
            }

            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                return convert(value, targetType, parameter, culture);
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                return convertBack is null ?
                    throw new NotImplementedException() :
                    convertBack(value, targetType, parameter, culture);
            }
        }
    }
}
