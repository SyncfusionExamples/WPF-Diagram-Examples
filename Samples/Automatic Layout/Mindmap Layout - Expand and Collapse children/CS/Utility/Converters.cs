using AutomaticLayout_MindmapLayout.Model;
using Syncfusion.UI.Xaml.Diagram.Layout;
using Syncfusion.UI.Xaml.Diagram;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace AutomaticLayout_MindmapLayout.Utility
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
    class DirectionToHorizontalAlignmentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            RootChildDirection type = (RootChildDirection)value;
            if (type == RootChildDirection.Left)
            {
                return HorizontalAlignment.Left;
            }
            else if (type == RootChildDirection.Right)
            {
                return HorizontalAlignment.Right;
            }
            else
            {
                return HorizontalAlignment.Center;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
}
