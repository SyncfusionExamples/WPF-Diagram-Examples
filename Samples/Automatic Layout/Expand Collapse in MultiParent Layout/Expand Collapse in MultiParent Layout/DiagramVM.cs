using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Controls;
using Syncfusion.UI.Xaml.Diagram.Layout;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Expand_Collapse_in_MultiParent_Layout
{
    public class MultiParent : DiagramViewModel
    {
        public Button prevbutton = null;
        public MultiParent()
        {
            //Initialize Diagram Properties
            Connectors = new ObservableCollection<ConnectorVM>();
            Menu = null;
            Tool = Tool.MultipleSelect;
            HorizontalRuler = new Ruler { Orientation = Orientation.Horizontal };
            VerticalRuler = new Ruler { Orientation = Orientation.Vertical };
            DefaultConnectorType = ConnectorType.Orthogonal;

            // Initialize DataSourceSettings for SfDiagram
            DataSourceSettings = new DataSourceSettings()
            {
                ParentId = "ReportingPerson",
                Id = "Name",
                DataSource = GetData(),
            };

            // Initialize LayoutSettings for SfDiagram
            LayoutManager = new LayoutManager()
            {
                Layout = new DirectedTreeLayout()
                {
                    Type = LayoutType.Hierarchical,
                    Orientation = TreeOrientation.TopToBottom,
                    AvoidSegmentOverlapping = true,
                    HorizontalSpacing = 40,
                    VerticalSpacing = 40,
                },
            };
        }

        #region Private Variables

        private ICommand _Orientation_Command;

        #endregion

        #region Commands
        public ICommand Orientation_Command
        {
            get { return _Orientation_Command; }
            set
            {
                if (_Orientation_Command != value)
                {
                    _Orientation_Command = value;
                    onPropertyChanged("Orientation_Command");
                }
            }
        }
        #endregion


        public event PropertyChangedEventHandler PropertyChanged;

        private void onPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }


        /// <summary>
        /// Method to Get Data for DataSource
        /// </summary>
        /// <param name="data"></param>
        private DataItems GetData()
        {
            DataItems data = new DataItems();

            data.Add(new ItemInfo("n11", "#ff6329") { ReportingPerson = new List<string>() });

            data.Add(new ItemInfo("n12", "#ff6329") { ReportingPerson = new List<string>() });

            data.Add(new ItemInfo("n13", "#ff6329") { ReportingPerson = new List<string>() });

            data.Add(new ItemInfo("n21", "#941100") { ReportingPerson = new List<string> { "n11", "n12", "n13" } });

            return data;
        }
    }

    public class ConnectorVM : ConnectorViewModel
    {
        public ConnectorVM()
            : base()
        {
            this.CornerRadius = 10;
        }

        private bool _isVisible = true;

        public bool IsVisible
        {
            get => _isVisible;
            set
            {
                if (_isVisible == value) return;
                _isVisible = value;
                OnPropertyChanged(nameof(IsVisible));
            }
        }

        public override string ToString()
        {
            return $"Source {(SourceNode as NodeViewModel).Content} - Target {(TargetNode as NodeViewModel).Content} - Visible {IsVisible}";
        }
    }


    public class ItemInfo
    {
        public ItemInfo(string name, string color)
        {
            this.Name = name;
            this.RatingColor = color;
        }

        public string RatingColor { get; set; }

        public string Name { get; set; }

        public List<string> ReportingPerson { get; set; }
    }

    public class DataItems : ObservableCollection<ItemInfo>
    {

    }
}
