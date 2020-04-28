using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace ScrollSettingsSample
{
    public class ScrollSettingsViewModel : DiagramViewModel
    {
        /// <summary>
        /// Gets or sets the auto scroll limit command
        /// </summary>
        public ICommand AutoScrollLimitCommand { get; set; }

        /// <summary>
        /// Gets or sets the scroll limit command
        /// </summary>
        public ICommand ScrollLimitCommand { get; set; }

        /// <summary>
        /// Gets or sets the drag limit command
        /// </summary>
        public ICommand DragLimitCommand { get; set; }

        public ScrollSettingsViewModel()
        {
            //Initialize the command methods.
            AutoScrollLimitCommand = new Command(OnAutoScrollLimitCommandExecute);
            ScrollLimitCommand = new Command(OnScrollLimitCommandExecute);
            DragLimitCommand = new Command(OnDragLimitCommandExecute);
            SelectorChangedCommand = new Command(OnSelectorChangedCommandExecute);

            //Initialize the Nodes Collection 
            this.Nodes = new ObservableCollection<NodeViewModel>();

            //Initialize the scroll settings
            this.ScrollSettings = new ScrollSettings()
            {
                AutoScrollLimit = ScrollLimit.Infinity,
                ScrollLimit = ScrollLimit.Infinity,
                DragLimit = ScrollLimit.Infinity,
            };

            //Initialize the rulers
            this.HorizontalRuler = new Ruler();
            this.VerticalRuler = new Ruler() { Orientation = Orientation.Vertical };

            //creating source node
            NodeViewModel simpleNode = new NodeViewModel()
            {
                UnitWidth = 100,
                UnitHeight = 50,
                OffsetX = 150,
                OffsetY = 300,
                Shape = App.Current.Resources["Rectangle"],
            };

            //Adding the nodes into Collection
            (this.Nodes as ObservableCollection<NodeViewModel>).Add(simpleNode);
        }

        /// <summary>
        /// To update the auto-scroll limit value of the scroll settings.
        /// </summary>
        /// <param name="parameter">parameter value.</param>
        private void OnAutoScrollLimitCommandExecute(object parameter)
        {
            if (this != null)
            {
                if (parameter.ToString() == "diagram")
                {
                    this.ScrollSettings.AutoScrollLimit = ScrollLimit.Diagram;
                }
                else if (parameter.ToString() == "limited")
                {
                    this.ScrollSettings.AutoScrollLimit = ScrollLimit.Limited;
                    this.ScrollSettings.ScrollableArea = new System.Windows.Rect(0, 0, 1500, 1500);
                }
                else
                {
                    this.ScrollSettings.AutoScrollLimit = ScrollLimit.Infinity;
                }
            }
        }

        /// <summary>
        /// To update the scroll limit value fo the scroll settings.
        /// </summary>
        /// <param name="parameter">parameter value</param>
        private void OnScrollLimitCommandExecute(object parameter)
        {
            if (this != null)
            {
                if (parameter.ToString() == "diagram")
                {
                    this.ScrollSettings.ScrollLimit = ScrollLimit.Diagram;
                }
                else if (parameter.ToString() == "limited")
                {
                    this.ScrollSettings.ScrollLimit = ScrollLimit.Limited;
                    this.ScrollSettings.ScrollableArea = new System.Windows.Rect(0, 0, 1500, 1500);
                }
                else
                {
                    this.ScrollSettings.ScrollLimit = ScrollLimit.Infinity;
                }
            }
        }

        private void OnDragLimitCommandExecute(object parameter)
        {
            if (this != null)
            {
                if (parameter.ToString() == "diagram")
                {
                    this.ScrollSettings.DragLimit = ScrollLimit.Diagram;
                }
                else if (parameter.ToString() == "limited")
                {
                    this.ScrollSettings.DragLimit = ScrollLimit.Limited;
                    this.ScrollSettings.EditableArea = new System.Windows.Rect(100, 100, 500, 500);
                }
                else
                {
                    this.ScrollSettings.DragLimit = ScrollLimit.Infinity;
                }
            }
        }

        /// <summary>
        /// Selector changed event to enale or disbale the drag limit behaviour.
        /// </summary>
        /// <param name="parameter">Event arg.</param>
        private void OnSelectorChangedCommandExecute(object parameter)
        {
            if (this != null)
            {
                //Changing the cursor to indicate the dragging restriction 
                (parameter as SelectorChangedEventArgs).BlockCursor = Cursors.No;
                //Enabling the dragging limit
                (parameter as SelectorChangedEventArgs).Block = true;
            }
        }
    }
}
