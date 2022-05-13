using Syncfusion.UI.Xaml.Diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplePortCreation
{
    /// <summary>
    /// Represents a custom class to NodeViewModel to add custom tooltip properties to it.
    /// </summary>
    class CustomNodeVM : NodeViewModel
    {
        private string toolTipContent;

        /// <summary>
        /// Gets or sets tool tip content to the node ports.
        /// </summary>
        public string ToolTipContent
        {
            get
            {
                return toolTipContent;
            }
            set
            {
                if (toolTipContent != value)
                {
                    toolTipContent = value;
                    OnPropertyChanged("ToolTipContent");
                }
            }
        }
    }
}
