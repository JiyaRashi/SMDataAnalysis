using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace ShareMarketData.Converter
{
   public class PreviousHighConverter : IMultiValueConverter
    {
        //public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        //{
            

        //}

        //public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        //{
        //    throw new NotImplementedException();
        //}

        object IMultiValueConverter.Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            int pre = System.Convert.ToInt32(values[0]);
            int close = System.Convert.ToInt32(values[1]);

            int diffval = pre - close;
            if (diffval>10)
                return Brushes.Gold;
            if (diffval > 5 && diffval < 10)
                return Brushes.Brown;
            if (pre == close)
            {
                return Brushes.Blue;
            }
            if (pre <= close)
            {
                return Brushes.Green;
            }
            if (pre >= close)
            {
                return Brushes.Red;
            }
            //if (value.ToString().Length == 0)
            //    return false;
            //if (System.Convert.ToDouble(value) <= 100)
            //    return true;

            return false;
        }

        object[] IMultiValueConverter.ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
