using Microsoft.Win32;
using LogicCircuitSimulation.Model;
using LogicCircuitSimulation.ViewModel;
using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Stencil;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace LogicCircuitSimulation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            /* d*/
            diagram.Nodes = new ObservableCollection<BaseGateViewModel>();
            diagram.Connectors = new ObservableCollection<WireViewModel>();

            diagram.DefaultConnectorType = ConnectorType.Orthogonal;

            //Adding quick command to the nodes.
            (diagram.SelectedItems as SelectorViewModel).SelectorConstraints = SelectorConstraints.Tooltip | SelectorConstraints.QuickCommands;

            //Adding known types to the diagram
            diagram.KnownTypes = () => new List<Type>()
            {
                typeof(ComboBoxInputContent), typeof(TimerContent),
            };

            (diagram.Info as IGraphInfo).ConnectorTargetChangedEvent += MainWindow_ConnectorTargetChangedEvent;
            (diagram.Info as IGraphInfo).ItemDeleted += MainWindow_ItemDeleted;
            (diagram.Info as IGraphInfo).ItemTappedEvent += MainWindow_ItemTappedEvent;
            (diagram.Info as IGraphInfo).DragOver += MainWindow_DragOver;
            (diagram.Info as IGraphInfo).DragEnter += MainWindow_DragEnter;
            (diagram.Info as IGraphInfo).ItemDropEvent += MainWindow_ItemDropEvent;
            (diagram.Info as IGraphInfo).ObjectDrawn += MainWindow_ObjectDrawn;

            #region stencil

            var symbolfilters = new SymbolFilters();
            symbolfilters.Add(new SymbolFilterProvider { Content = "All", SymbolFilter = FilterProvider, IsChecked = true });
            symbolfilters.Add(new SymbolFilterProvider { Content = "Inputs/Outputs", SymbolFilter = FilterProvider, IsChecked = true });
            symbolfilters.Add(new SymbolFilterProvider { Content = "Gates", SymbolFilter = FilterProvider, IsChecked = true });
            stencil.SymbolFilters = symbolfilters;
            stencil.SelectedFilter = symbolfilters[0];
            stencil.Constraints &= ~StencilConstraints.AllowDragDrop;

            #endregion
        }

        private void MainWindow_ItemDropEvent(object sender, ItemDropEventArgs args)
        {
            if (args.Source is PortsGateViewModel)
            {
                //Showing dynamic ports panel after node dropped into diagram. 
                InputBox.Visibility = Visibility.Visible;
            }
        }

        private void MainWindow_DragEnter(object sender, ItemDropEventArgs args)
        {
            if (args.Source is TemplatedNodeViewModel && (args.Source as TemplatedNodeViewModel).Name.Equals("Input"))
            {
                (args.Source as TemplatedNodeViewModel).ContentTemplate = App.Current.Resources["Input"] as DataTemplate;
                (args.Source as TemplatedNodeViewModel).Content = (args.Source as TemplatedNodeViewModel).CustomContent;
            }
            else if (args.Source is TemplatedNodeViewModel && (args.Source as TemplatedNodeViewModel).Name.Equals("Timer"))
            {
                (args.Source as TemplatedNodeViewModel).ContentTemplate = App.Current.Resources["Timer"] as DataTemplate;
                (args.Source as TemplatedNodeViewModel).Content = (args.Source as TemplatedNodeViewModel).CustomContent;
            }
        }

        private void MainWindow_DragOver(object sender, ItemDropEventArgs args)
        {
            if (args.Source is TemplatedNodeViewModel && (args.Source as TemplatedNodeViewModel).Name.Equals("Input"))
            {
                (args.Source as TemplatedNodeViewModel).ContentTemplate = App.Current.Resources["Input"] as DataTemplate;
                (args.Source as TemplatedNodeViewModel).Content = (args.Source as TemplatedNodeViewModel).CustomContent;
            }
            else if (args.Source is TemplatedNodeViewModel && (args.Source as TemplatedNodeViewModel).Name.Equals("Timer"))
            {
                (args.Source as TemplatedNodeViewModel).ContentTemplate = App.Current.Resources["Timer"] as DataTemplate;
                (args.Source as TemplatedNodeViewModel).Content = (args.Source as TemplatedNodeViewModel).CustomContent;
            }
        }

        /// <summary>
        /// Filter symbols across the different symbol groups
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="symbol"></param>
        /// <returns></returns>
        private bool FilterProvider(SymbolFilterProvider sender, object symbol)
        {
            if (sender.Content.ToString() == "All")
            {
                return true;
            }
            else if (symbol is BaseGateViewModel)
            {
                return sender.Content.ToString() == (symbol as BaseGateViewModel).Key.ToString();
            }
            else if (symbol is NodeViewModel)
            {
                return sender.Content.ToString() == (symbol as NodeViewModel).Key.ToString();
            }
            else if (symbol is WireViewModel)
            {
                return sender.Content.ToString() == (symbol as WireViewModel).Key.ToString();
            }


            return false;
        }

        /// <summary>
        /// Raise when a node is tapped
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void MainWindow_ItemTappedEvent(object sender, ItemTappedEventArgs args)
        {
            if (args.Item is PortsGateViewModel)
            {
                //Showing dynamic ports panel when is selected.
                InputBox.Visibility = Visibility.Visible;
            }
            else
            {
                //hiding dynamic ports panel when tap on other element and diagram. And resetting input and output txtbox values to null.
                InputBox.Visibility = Visibility.Collapsed;
                (inputPortCount as TextBox).Text = null;
                (outputPortCount as TextBox).Text = null;
            }

            if (args.Item is InputViewModel gate)
            {
                gate.State = gate.State == BinaryState.One ? BinaryState.Zero : BinaryState.One;
            }
        }

        /// <summary>
        /// Raises when a connection is drawn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void MainWindow_ObjectDrawn(object sender, ObjectDrawnEventArgs args)
        {
            if (args.State == DragState.Completed)
            {
                // Removing connector when either of its end remains disconnected
                if (args.Item is WireViewModel connector && (connector.SourceNode == null || connector.TargetNode == null))
                {
                    (this.diagram.Connectors as ObservableCollection<WireViewModel>).Remove(args.Item as WireViewModel);
                }
            }
        }

        /// <summary>
        /// Raises when new input is established to a node
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void MainWindow_ConnectorTargetChangedEvent(object sender, ChangeEventArgs<object, ConnectorChangedEventArgs> args)
        {
            if (args.NewValue.DragState == DragState.Completed && args.Item is WireViewModel connector && connector.TargetNode is BaseGateViewModel)
            {
                (connector.TargetNode as BaseGateViewModel).ResetState();
            }
        }

        /// <summary>
        /// Raise when a diagram element is deleted
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void MainWindow_ItemDeleted(object sender, ItemDeletedEventArgs args)
        {
            if (args.Item is WireViewModel connector)
            {
                connector.State = BinaryState.Zero;
                if (connector.TargetNode is BaseGateViewModel targetNode)
                {
                    targetNode.ResetState();
                }
            }
            else if (args.Item is BaseGateViewModel node)
            {
                node.State = BinaryState.Zero;
            }
        }

        //Method to promote the save dialouge box when diagram has any unsaved changes.
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (diagram != null && diagram.HasChanges)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show(
                                    "Do you want to save the Diagram?",
                                    "SfDiagram",
                                    MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    SaveDiagram();
                }
            }
        }

        //Method to save the diagram.
        private void SaveDiagram()
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Title = "Save XAML";
            dialog.Filter = "XAML File (*.xaml)|*.xaml";
            if (dialog.ShowDialog() == true)
            {
                File.Delete(dialog.FileName);
                using (Stream s = File.Open(dialog.FileName, FileMode.OpenOrCreate))
                {
                    diagram.Save(s);
                }
            }
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            //Load from saved XAML file
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == true)
            {
                using (Stream myStream = dialog.OpenFile())
                {
                    diagram.Load(myStream);
                }
            }

            //Load from saved memory stream
          //  str.Position = 0;
          //  sfDiagram.Load(str);
        }

        //Method to add dynamic ports collection after press on yes button of the panel.
        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            InputBox.Visibility = Visibility.Collapsed;
            int inports = Int16.Parse((inputPortCount as TextBox).Text);
            int outports = Int16.Parse((outputPortCount as TextBox).Text);
            //Logic for adding input ports based on in ports count.
            if (inports < 9)
            {
                var node = ((diagram.SelectedItems as SelectorViewModel).Nodes as IEnumerable<object>).FirstOrDefault();
                if (node is PortsGateViewModel)
                {
                    (node as PortsGateViewModel).UnitHeight = (inports+1) * 30;
                    (node as PortsGateViewModel).Ports = new PortCollection();
                    for(int i=1; i <= inports; i++)
                    {
                        CustomNodePort port = new CustomNodePort()
                        {
                            NodeOffsetX = 0,
                            NodeOffsetY = (1.0/(inports + 1) )* i,
                            Shape = App.Current.MainWindow.Resources["Ellipse"],
                            Constraints = PortConstraints.InConnect,
                            PortVisibility = PortVisibility.Visible,
                        };
                        ((node as PortsGateViewModel).Ports as PortCollection).Add(port);
                    }
                }
            }
            //Logic for adding output ports based on out ports count.
            if (outports < 3)
            {
                var node = ((diagram.SelectedItems as SelectorViewModel).Nodes as IEnumerable<object>).FirstOrDefault();
                if (node is PortsGateViewModel)
                {
                    if (outports == 1)
                    {
                        CustomNodePort port = new CustomNodePort()
                        {
                            NodeOffsetX = 1,
                            NodeOffsetY = 0.5,
                            Shape = App.Current.MainWindow.Resources["Ellipse"],
                            Constraints = PortConstraints.OutConnect,
                            PortVisibility = PortVisibility.Visible,
                        };
                        ((node as PortsGateViewModel).Ports as PortCollection).Add(port);
                    }
                    else
                    {
                        CustomNodePort port1 = new CustomNodePort()
                        {
                            NodeOffsetX = 1,
                            NodeOffsetY = 0.4,
                            Shape = App.Current.MainWindow.Resources["Ellipse"],
                            Constraints = PortConstraints.OutConnect,
                            PortVisibility = PortVisibility.Visible,
                        };
                        CustomNodePort port2 = new CustomNodePort()
                        {
                            NodeOffsetX = 1,
                            NodeOffsetY = 0.6,
                            Shape = App.Current.MainWindow.Resources["Ellipse"],
                            Constraints = PortConstraints.OutConnect,
                            PortVisibility = PortVisibility.Visible,
                        };
                        ((node as PortsGateViewModel).Ports as PortCollection).Add(port1);
                        ((node as PortsGateViewModel).Ports as PortCollection).Add(port2);
                    }
                    
                }
            }
            //Clearing in and out ports text box values.
            (inputPortCount as TextBox).Text = null; 
            (outputPortCount as TextBox).Text = null;
        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            //Collapsing input grid panel when press on no buton.
            InputBox.Visibility = Visibility.Collapsed;
            (inputPortCount as TextBox).Text = null;
            (outputPortCount as TextBox).Text = null;
        }
    }

    public class CustomDiagram : SfDiagram
    {
        // Validate and choose required tool.
        protected override void SetTool(SetToolArgs args)
        {
            base.SetTool(args);
            if (args.Source is IPort)
            {
                args.Action = ActiveTool.Draw;
            }

            if (args.Source is CustomNodePort && args.Action == ActiveTool.Draw)
            {
                var port = args.Source as CustomNodePort;
                if (!port.CanCreateConnection(null))
                {
                    args.Action = ActiveTool.None;
                }
            }
        }

        // Validate the connection when connector endpoints are dragged.
        protected override void ValidateConnection(ConnectionParameter args)
        {
            if (args.TargetPort is CustomNodePort)
            {
                var port = args.TargetPort as CustomNodePort;
                if (!port.CanCreateConnection(args.Connector as IConnector))
                {
                    args.TargetPort = null;
                }
            }
            base.ValidateConnection(args);
        }
    }

    public class CustomNodePort : NodePortViewModel
    {
        public CustomNodePort()
        {
            UnitWidth = 10;
            UnitHeight = 10;
            MaxConnection = 1;
        }
        public int MaxConnection { get; set; }
        public bool CanCreateConnection(IConnector ignore)
        {
            var info = this.Info as INodePortInfo;
            if (info.Connectors != null)
            {
                var count = info.Connectors.Where(c => c != ignore).Count();

                // Validate number of connections.
                if (MaxConnection >= 0 && count >= MaxConnection)
                {
                    return false;
                }
            }
            else if (MaxConnection == 0)
            {
                return false;
            }
            return true;
        }
    }
}
