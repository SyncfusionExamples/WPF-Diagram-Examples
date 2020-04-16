using Syncfusion.UI.Xaml.Diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selectorconstraints.ViewModel
{
   public class DiagramVM:DiagramViewModel
    {
        #region Fields

        private bool _resize = true;
        private bool _quickcommand = true;
        private bool _rotate = true;       

        #endregion
       
        public DiagramVM()
        {
            Nodes = new NodeCollection();
            Connectors = new ConnectorCollection();

            this.SelectedItems = new SelectorViewModel();
        
            NodeViewModel node = new NodeViewModel()
            {
                OffsetX = 500,
                OffsetY = 250,
                UnitHeight = 100,
                UnitWidth = 100,               
            };
            (Nodes as NodeCollection).Add(node);
        }

        #region Properties

        /// <summary>
        /// Gets or sets the value for Resize
        /// </summary>
        public bool Resize
        {
            get
            {
                return _resize;
            }
            set
            {
                if (value != _resize)
                {
                    _resize = value;
                    OnPropertyChanged("Resize");
                    OnResizeChanged(_resize);
                }
            }
        }

        /// <summary>
        /// Gets or sets the value for Rotate
        /// </summary>
        public bool Rotate
        {
            get
            {
                return _rotate;
            }
            set
            {
                if (value != _rotate)
                {
                    _rotate = value;
                    OnPropertyChanged("Rotate");
                    OnRotateChanged(_rotate);
                }
            }
        }

        /// <summary>
        /// Gets or sets the value for QuickCommand
        /// </summary>
        public bool QuickCommand
        {
            get
            {
                return _quickcommand;
            }
            set
            {
                if (value != _quickcommand)
                {
                    _quickcommand = value;
                    OnPropertyChanged("QuickCommand");
                    OnQuickCommandChanged(_quickcommand);
                }
            }
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Method to change the value of QuickCommand
        /// </summary>
        /// <param name="quick"></param>
        private void OnQuickCommandChanged(bool quick)
        {
            if (quick)
            {
                (this.SelectedItems as SelectorViewModel).SelectorConstraints = (this.SelectedItems as SelectorViewModel).SelectorConstraints.Add(SelectorConstraints.QuickCommands);
            }
            else
            {
                (this.SelectedItems as SelectorViewModel).SelectorConstraints = (this.SelectedItems as SelectorViewModel).SelectorConstraints.Remove(SelectorConstraints.QuickCommands);
            }
        }

        /// <summary>
        /// Method to change the value of Resize
        /// </summary>
        /// <param name="resize"></param>
        private void OnResizeChanged(bool resize)
        {
            if (resize)
            {
                (this.SelectedItems as SelectorViewModel).SelectorConstraints = (this.SelectedItems as SelectorViewModel).SelectorConstraints.Add(SelectorConstraints.Resizer);
            }
            else
            {
                (this.SelectedItems as SelectorViewModel).SelectorConstraints = (this.SelectedItems as SelectorViewModel).SelectorConstraints.Remove(SelectorConstraints.Resizer);
            }
        }

       

        /// <summary>
        /// Method to change the value of Rotate
        /// </summary>
        /// <param name="rotate"></param>
        private void OnRotateChanged(bool rotate)
        {
            if (rotate)
            {
                (this.SelectedItems as SelectorViewModel).SelectorConstraints = (this.SelectedItems as SelectorViewModel).SelectorConstraints.Add(SelectorConstraints.Rotator);
            }
            else
            {
                (this.SelectedItems as SelectorViewModel).SelectorConstraints = (this.SelectedItems as SelectorViewModel).SelectorConstraints.Remove(SelectorConstraints.Rotator);
            }
        }       

        #endregion
    }
}
