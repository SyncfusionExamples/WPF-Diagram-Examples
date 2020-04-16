using Syncfusion.UI.Xaml.Diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portconstraints.ViewModel
{
   public class DiagramVM:DiagramViewModel
    {
        #region Fields

        private bool _connect = true;
        private bool _inconnect = true;
        private bool _outconnect = true;
       

        #endregion

        NodePortViewModel anno;
        public DiagramVM()
        {
            Nodes = new NodeCollection();
            Connectors = new ConnectorCollection();

            PortVisibility = PortVisibility.Visible;

            anno = new NodePortViewModel()
            {
               UnitHeight=10,UnitWidth=10,
            };

            NodeViewModel node = new NodeViewModel()
            {
                OffsetX = 500,
                OffsetY = 250,
                UnitHeight = 100,
                UnitWidth = 100,
                Ports = new PortCollection()
                {
                    anno,
                },
            };
            (Nodes as NodeCollection).Add(node);
        }

        #region Properties

        /// <summary>
        /// Gets or sets the value for Connect
        /// </summary>
        public bool Connect
        {
            get
            {
                return _connect;
            }
            set
            {
                if (value != _connect)
                {
                    _connect = value;
                    OnPropertyChanged("Connect");
                    OnConnectionChanged(_connect);
                }
            }
        }

        /// <summary>
        /// Gets or sets the value for InConnect
        /// </summary>
        public bool InConnect
        {
            get
            {
                return _inconnect;
            }
            set
            {
                if (value != _inconnect)
                {
                    _inconnect = value;
                    OnPropertyChanged("InConnect");
                    OnInConnectionChanged(_inconnect);
                }
            }
        }

        /// <summary>
        /// Gets or sets the value for OutConnect
        /// </summary>
        public bool OutConnect
        {
            get
            {
                return _outconnect;
            }
            set
            {
                if (value != _outconnect)
                {
                    _outconnect = value;
                    OnPropertyChanged("OutConnect");
                    OnOutConnectionChanged(_outconnect);
                }
            }
        }

      
        #endregion

        #region Helper Methods

        /// <summary>
        /// Method to change the value of Connect
        /// </summary>
        /// <param name="connect"></param>
        private void OnConnectionChanged(bool connect)
        {
            if (connect)
            {
                anno.Constraints = PortConstraints.Connectable|PortConstraints.InheritPortVisibility;
            }
            else
            {
                anno.Constraints &= ~(PortConstraints.Connectable|PortConstraints.InheritConnectable);
            }
        }

        /// <summary>
        /// Method to change the value of InConnect
        /// </summary>
        /// <param name="inconnect"></param>
        private void OnInConnectionChanged(bool inconnect)
        {
            if (inconnect)
            {
                anno.Constraints = PortConstraints.InConnect | PortConstraints.Connectable | PortConstraints.InheritPortVisibility;
                anno.Constraints &= ~(PortConstraints.OutConnect | PortConstraints.InheritConnectable);
            }
            else
            {
                anno.Constraints = PortConstraints.InheritPortVisibility| PortConstraints.OutConnect | PortConstraints.Connectable&~PortConstraints.InConnect;
            }
        }

        /// <summary>
        /// Method to change the value of OutConnnect
        /// </summary>
        /// <param name="outconnect"></param>
        private void OnOutConnectionChanged(bool outconnect)
        {
            if (outconnect)
            {
                anno.Constraints = PortConstraints.OutConnect | PortConstraints.Connectable | PortConstraints.InheritPortVisibility;               
            }
            else
            {               
                anno.Constraints = PortConstraints.InheritPortVisibility|PortConstraints.InConnect | PortConstraints.Connectable & ~PortConstraints.OutConnect;
            }
        }        
        #endregion
    }
}
