using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace AutomaticLayout_OrganizationLayout
{
    public class BoolToVisibiltyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool? val = value as bool?;
            if (val == true)
                return Visibility.Visible;
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var val = (Visibility)value;

            if (val == Visibility.Visible)
            {
                return true; ;

            }
            return false;

        }
        
    }
    public class EnumtoBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            object val = Enum.ToObject(typeof(State), value);
            if (value.ToString() == State.Expand.ToString())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
