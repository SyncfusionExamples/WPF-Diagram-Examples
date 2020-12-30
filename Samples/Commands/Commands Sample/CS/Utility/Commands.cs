using Syncfusion.UI.Xaml.Diagram;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace Commands.Utility
{
    internal class Command : ICommand
    {
        /// <summary>
        /// Occurs when changes occur that affect whether the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        Func<object, bool> canExecute;
        Action<object> executeAction;
        bool canExecuteCache;
        private object onItemSelectedCommand;

        /// <summary>
        /// Initializes a new instance of the <see cref="Command"/> class.
        /// </summary>
        /// <param name="executeAction">The execute action.</param>
        /// <param name="canExecute">The can execute.</param>
        public Command(Action<object> executeAction,
                               Func<object, bool> canExecute = null)
        {
            this.executeAction = executeAction;
            this.canExecute = canExecute;
        }

        public Command(object onItemSelectedCommand)
        {
            this.onItemSelectedCommand = onItemSelectedCommand;
        }

        #region ICommand Members
        /// <summary>
        /// Defines the method that determines whether the command 
        /// can execute in its current state.
        /// </summary>
        /// <param name="parameter">
        /// Data used by the command. 
        /// If the command does not require data to be passed,
        /// this object can be set to null.
        /// </param>
        /// <returns>
        /// true if this command can be executed; otherwise, false.
        /// </returns>
        public bool CanExecute(object parameter)
        {
            if (parameter == null || canExecute == null)
            {
                return true;
            }
            bool tempCanExecute = canExecute(parameter);

            if (canExecuteCache != tempCanExecute)
            {
                canExecuteCache = tempCanExecute;
                if (CanExecuteChanged != null)
                {
                    CanExecuteChanged(this, new EventArgs());
                }
            }

            return canExecuteCache;
        }

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">
        /// Data used by the command. 
        /// If the command does not require data to be passed, 
        /// this object can be set to null.
        /// </param>
        public void Execute(object parameter)
        {
            executeAction(parameter);
        }
        #endregion
    }

    internal class PointerToolConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo language)
    {
        bool? isTrue = value is Tool && (Tool)value == Tool.MultipleSelect || (Tool)value == Tool.SingleSelect ? true : false;
        return isTrue;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo language)
    {
        if (value is bool?)
        {
            bool? isTrue = (bool?)value;
            if (isTrue.Value == true)
                return Tool.MultipleSelect;
            else
                return Tool.None;
        }
        return Tool.None;
    }
}

internal class ConnectorToolConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo language)
    {
        bool? isTrue = value is DrawingTool && (DrawingTool)value == DrawingTool.Connector ? true : false;
        return isTrue;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo language)
    {
        if (value is bool?)
        {
            bool? isTrue = (bool?)value;
            if (isTrue.Value == true)
                return DrawingTool.Connector;
            else
                return DrawingTool.None;
        }
        return DrawingTool.None;
    }
}

internal class TextToolConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo language)
    {
        bool? isTrue = value is DrawingTool && (DrawingTool)value == DrawingTool.TextNode ? true : false;
        return isTrue;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo language)
    {
        if (value is bool?)
        {
            bool? isTrue = (bool?)value;
            if (isTrue.Value == true)
                return DrawingTool.TextNode;
            else
                return DrawingTool.None;
        }
        return DrawingTool.None;
    }
}

    internal class ZoomPanToolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo language)
        {
            bool? isTrue = value is Tool && (Tool)value == Tool.ZoomPan ? true : false;
            return isTrue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo language)
        {
            if (value is bool?)
            {
                bool? isTrue = (bool?)value;
                if (isTrue.Value == true)
                    return Tool.ZoomPan;
                else
                    return Tool.None;
            }
            return Tool.None;
        }
    }


}
