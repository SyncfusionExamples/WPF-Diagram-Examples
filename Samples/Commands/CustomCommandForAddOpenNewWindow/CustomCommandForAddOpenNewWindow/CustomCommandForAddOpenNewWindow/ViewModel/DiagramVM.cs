//using CustomCommand.Utility;
using Microsoft.Win32;
using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.Windows.Shared;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CustomCommand.ViewModel
{
    /// <summary>
    /// ViewModel class for the Diagram, inheriting from DiagramViewModel.
    /// This class handles commands and manages selected items.
    /// </summary>
    public class DiagramVM : DiagramViewModel
    {
        /// <summary>
        /// Command that opens a new window.
        /// </summary>
        public ICommand DemoCommand { get; set; }

        /// <summary>
        /// Initializes a new instance of the DiagramVM class.
        /// Sets up command management and adds a custom command for handling the F2 key gesture.
        /// </summary>
        public DiagramVM()
        {
            // Initialize selected items collection.
            SelectedItems = new SelectorViewModel();

            // Initialize custom command.
            DemoCommand = new Command(OnDemoCommand);
        }

        /// <summary>
        /// Method that is executed when the DemoCommand is triggered.
        /// Opens a new window if connectors are selected in the diagram.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        private void OnDemoCommand(object sender)
        {
            // Check if connectors are selected in the diagram.
            if (SelectedItems is SelectorViewModel && (SelectedItems as SelectorViewModel).Connectors != null && ((SelectedItems as SelectorViewModel).Connectors as IEnumerable<object>).Count() > 0)
            {
                // Open a new window.
                Window1 win = new Window1();
                win.Show();
            }
        }
    }
}
