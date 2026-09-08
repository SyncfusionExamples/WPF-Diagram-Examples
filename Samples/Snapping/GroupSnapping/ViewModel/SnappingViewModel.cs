using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GroupSnapping
{
    /// <summary>
    /// ViewModel for managing group snapping behavior in SfDiagram
    /// </summary>
    public class SnappingViewModel : DiagramViewModel
    {
        
        #region  Fields

        //Holds the snap interval value
        private object snapIntervalChanged = "20";
        //Holds the stroke color of the snap indicator line
        private Brush strokecolor = (SolidColorBrush)(new BrushConverter().ConvertFrom("#83F6F0"));
        //Holds the stroke thickness of the snap indicator line
        private double strokethickness = 2;
        //Holds the snapping angle value
        private object snapAngleChanged = "5";
        //Holds the snap to object value
        private SnapToObject snaptoObject = SnapToObject.All;

        private bool _isSnappingEnabled = true;

        //Holds commands to show or hide the gridlines
        //Holds commands to show or hide the gridlines
        public ICommand ShowGridlinesCommand { get; set; }
        //Holds commands to enable or disable snapping on gridlines
        public ICommand SnappingToGridlinesCommand { get; set; }

        //Gets or sets the snap interval value.
        public object SnapIntervalChanged
        {
            get
            {
                return snapIntervalChanged;
            }

            set
            {
                if (value != snapIntervalChanged)
                {
                    snapIntervalChanged = value;
                    OnPropertyChanged("SnapIntervalChanged");
                    this.OnSnapIntervalChanged(snapIntervalChanged);
                }
            }
        }

        public bool IsSnappingEnabled
        {
            get { return _isSnappingEnabled; }
            set
            {
                if (_isSnappingEnabled != value)
                {
                    _isSnappingEnabled = value;
                    UpdateSnapSettings();
                    OnPropertyChanged("IsSnappingEnabled");
                }
            }
        }

        /// <summary>
        /// Gets or sets the Strokecolor snap indicator lines
        /// </summary>
        public Brush Strokecolor
        {
            get
            {
                return strokecolor;
            }

            set
            {
                if (strokecolor != value)
                {
                    strokecolor = value;
                    OnPropertyChanged("Strokecolor");
                    OnStrokeColorChanged(strokecolor);
                }
            }
        }

        /// <summary>
        /// Gets or sets the Strokethickness snap indicator lines
        /// </summary>
        public double Strokethickness
        {
            get
            {
                return strokethickness;
            }

            set
            {
                if (strokethickness != value)
                {
                    strokethickness = value;
                    OnPropertyChanged("Strokethickness");
                    OnStrokeThicknessChanged(strokethickness);
                }
            }
        }


        /// <summary>
        /// Gets or sets the snap angle value.
        /// </summary>
        public object SnapAngleChanged
        {
            get
            {
                return snapAngleChanged;
            }

            set
            {
                if (value != snapAngleChanged)
                {
                    snapAngleChanged = value;
                    OnPropertyChanged("SnapAngleChanged");
                    this.OnSnapAngleChanged(snapAngleChanged);
                }
            }
        }

        /// <summary>
        /// Gets ore sets the snap to object value of snapping
        /// </summary>
        public SnapToObject SnapToObjectValue
        {
            get
            {
                return snaptoObject;
            }

            set
            {
                if (value != snaptoObject)
                {
                    snaptoObject = value;
                    OnPropertyChanged("SnapAngleChanged");
                    this.OnSnapToObjectChanged(snaptoObject);
                }
            }
        }

       
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes the new instance of <see cref="SnappingViewModel"/> class.
        /// </summary>
        public SnappingViewModel()
        {
            //Intialize the nodes and connectors collection
            this.Nodes = new NodeCollection();
            this.Connectors = new ConnectorCollection();

            //Initialize the horizontal ruler
            this.HorizontalRuler = new Syncfusion.UI.Xaml.Diagram.Controls.Ruler()
            {
                Orientation = Orientation.Horizontal,
            };

            //Initialize the vertical ruler
            this.VerticalRuler = new Syncfusion.UI.Xaml.Diagram.Controls.Ruler()
            {
                Orientation = Orientation.Vertical,
            };

            //Initialize the snap settings class.
            this.SnapSettings = new SnapSettings()
            {
                SnapConstraints = SnapConstraints.All,
                SnapToObject = SnapToObject.All,
            };


            //Initialize the selector view model and disbale the quick coammnds.
            this.SelectedItems = new SelectorViewModel();
            (this.SelectedItems as SelectorViewModel).SelectorConstraints &= ~(SelectorConstraints.QuickCommands);
            //Initialize the command to show gridlines and enable the snapping on gridlines 
            ShowGridlinesCommand = new Command(OnShowGridlinesCommandExecute);
            SnappingToGridlinesCommand = new Command(OnSnappingCommandExecute);

        }

        #endregion

        #region Helper Methods

        private void UpdateSnapSettings()
        {
          
            if (_isSnappingEnabled)
            {
                this.SnapSettings.SnapConstraints = SnapConstraints.SnapToLines | SnapConstraints.ShowLines;
                this.SnapSettings.SnapToObject = SnapToObject.All;
            }
            else
            {
                this.SnapSettings.SnapConstraints = SnapConstraints.None;
                this.SnapSettings.SnapToObject = SnapToObject.None;
            }
        }

        private void OnSnapToObjectChanged(SnapToObject snaptoObjectvalues)
        {
            if (this.SnapSettings != null)
            {
                this.SnapSettings.SnapToObject = snaptoObjectvalues;
            }
        }

        /// <summary>
        /// Method to change the stroke color of snap line
        /// </summary>
        /// <param name="strokecolor">color of the stroke.</param>
        private void OnStrokeColorChanged(Brush strokecolor)
        {
            Brush value = strokecolor;
            Style pathStyle = new Style(typeof(Shape));
            double thickness = double.Parse(Strokethickness.ToString());
            pathStyle.Setters.Add(new Setter(Shape.StrokeThicknessProperty, thickness));
            pathStyle.Setters.Add(new Setter(Shape.StrokeProperty, value));
            if (this.SnapSettings != null)
            {
                this.SnapSettings.SnapIndicatorStyle = pathStyle as Style;
            }
            pathStyle = null;
        }

        /// <summary>
        /// Method to change the strokethickness of snaplines
        /// </summary>
        /// <param name="strokethickness">Stroke thickness value.</param>
        private void OnStrokeThicknessChanged(double strokethickness)
        {
            double value = double.Parse(strokethickness.ToString());
            Style pathStyle = new Style(typeof(Shape));
            pathStyle.Setters.Add(new Setter(Shape.StrokeProperty, Strokecolor));
            pathStyle.Setters.Add(new Setter(Shape.StrokeThicknessProperty, value));
            if (this.SnapSettings != null)
            {
                this.SnapSettings.SnapIndicatorStyle = pathStyle as Style;
            }

            pathStyle = null;
        }



        /// <summary>
        /// To show or hide the gridlines on the diagrma page.
        /// </summary>
        /// <param name="parameter">Boolean command to change the grid lines visibility.</param>
        private void OnShowGridlinesCommandExecute(object parameter)
        {
            if (this != null)
            {
                if ((bool)parameter)
                {
                    this.SnapSettings.SnapConstraints |= SnapConstraints.ShowLines;
                }
                else
                {
                    this.SnapSettings.SnapConstraints &= ~SnapConstraints.ShowLines;
                }
            }
        }

        /// <summary>
        /// To enable or disable the snapping on gridlines.
        /// </summary>
        /// <param name="parameter">Boolean command to enable or disable the snapping on gridlines.</param>
        private void OnSnappingCommandExecute(object parameter)
        {
            if (this != null)
            {
                if ((bool)parameter)
                {
                    this.SnapSettings.SnapConstraints |= SnapConstraints.SnapToLines;
                }
                else
                {
                    this.SnapSettings.SnapConstraints &= ~SnapConstraints.SnapToLines;
                }
            }
        }

        /// <summary>
        /// To change the snap interval.
        /// </summary>
        /// <param name="sizechanged">Snap interval value.</param>
        private void OnSnapIntervalChanged(object sizechanged)
        {
            double value = double.Parse(sizechanged.ToString());
            if (this.SnapSettings != null)
            {
                this.SnapSettings.HorizontalGridlines.SnapInterval = new List<double>() { value };
                this.SnapSettings.VerticalGridlines.SnapInterval = new List<double>() { value };
            }
        }

        /// <summary>
        /// To change the snap angle.
        /// </summary>
        /// <param name="sizechanged">Snap angle value.</param>
        private void OnSnapAngleChanged(object sizechanged)
        {
            double value = double.Parse(sizechanged.ToString());
            if (this.SnapSettings != null)
            {
                this.SnapSettings.SnapAngle = value;
            }
        }

    }
}
    #endregion

internal class Command : ICommand
{
    /// <summary>
    /// Occurs when changes occur that affect whether the command should execute.
    /// </summary>
    public event EventHandler CanExecuteChanged;

    Func<object, bool> canExecute;
    Action<object> executeAction;
    bool canExecuteCache;

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
