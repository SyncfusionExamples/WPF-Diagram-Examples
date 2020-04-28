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
        /// Gets or sets the scroll limit command
        /// </summary>
        public ICommand ScrollLimitCommand { get; set; }       

        public ScrollSettingsViewModel()
        {
            //Initialize the command methods.
           
            ScrollLimitCommand = new Command(OnScrollLimitCommandExecute);

            //Initialize the Nodes Collection 
            this.Nodes = new NodeCollection();

            //Initialize the scroll settings
            this.ScrollSettings = new ScrollSettings()
            {                
                ScrollLimit = ScrollLimit.Infinity,                
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

            //creating source node
            NodeViewModel simpleNode1 = new NodeViewModel()
            {
                UnitWidth = 100,
                UnitHeight = 50,
                OffsetX = 1150,
                OffsetY = 700,
                Shape = App.Current.Resources["Rectangle"],
            };

            //Adding the nodes into Collection
            (this.Nodes as NodeCollection).Add(simpleNode);
            (this.Nodes as NodeCollection).Add(simpleNode1);
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
      
    }
}
