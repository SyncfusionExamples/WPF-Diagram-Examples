using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Controls;
using Syncfusion.UI.Xaml.Diagram.Layout;
using Syncfusion.Windows.Shared;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;

namespace Force_directed_tree.ViewModel
{
    public class ForceDirectedViewModel : DiagramViewModel
    {
        #region fields

        double _connectorlength = 110;
        private int repulsionStrength = 25000;
        private double attractionStrength = 0.6;
        private int maxIteration = 1500;
        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the value for the Lenght of the Connectors
        /// </summary>
        public double ConnectorLength
        {
            get
            {
                return _connectorlength;
            }
            set
            {
                if (!_connectorlength.Equals(value))
                {
                    _connectorlength = value;

                    OnPropertyChanged(nameof(ConnectorLength));
                    (this.LayoutManager.Layout as ForceDirectedTree).ConnectorLength = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the value for the AttractionStrength
        /// </summary>
        public double AttractionStrength
        {
            get
            {
                return attractionStrength;
            }
            set
            {
                if (!attractionStrength.Equals(value))
                {
                    attractionStrength = value;
                    OnPropertyChanged(nameof(AttractionStrength));
                    (this.LayoutManager.Layout as ForceDirectedTree).AttractionStrength = value;
                }
            }
        }

        /// <summary>
        /// Gets or Sets the value of the RepulsionStrength
        /// </summary>
        public int RepulsionStrength
        {
            get
            {
                return repulsionStrength;
            }
            set
            {
                if (!repulsionStrength.Equals(value))
                {
                    repulsionStrength = value;
                    OnPropertyChanged(nameof(RepulsionStrength));
                    (this.LayoutManager.Layout as ForceDirectedTree).RepulsionStrength = value;
                }
            }
        }

        /// <summary>
        /// Gets or Sets the value for the maximum number of Iteration
        /// </summary>
        public int MaximumIteration
        {
            get
            {
                return maxIteration;
            }
            set
            {
                if (!maxIteration.Equals(value))
                {
                    maxIteration = value;
                    OnPropertyChanged(nameof(MaximumIteration));
                    (this.LayoutManager.Layout as ForceDirectedTree).MaximumIteration = value;
                }
            }
        }
        #endregion 

        public ForceDirectedViewModel()
        {

            ScrollSettings = new ScrollSettings()
            {
                ScrollLimit = ScrollLimit.Diagram,
            };

            PageSettings = new PageSettings()
            {
                PageBackground = new SolidColorBrush(Colors.White),
                PageBorderBrush = new SolidColorBrush(Colors.Transparent),
            };


            // Initialize the DataSourceSettings 
            DataSourceSettings = new DataSourceSettings()
            {
                ParentId = "Manager",
                Id = "Id",
                DataSource = DataSources(),

            };

            // Initialize the Layout Manager
            LayoutManager = new LayoutManager()
            {
                Layout = new ForceDirectedTree()
                {
                    AttractionStrength = 0.6,
                    RepulsionStrength = 25000,
                    MaximumIteration = 1000,
                }
            };
        }

        // Create a data source for the force directed tree 
        public List<ForceDirectedDetails> DataSources()
        {
            List<ForceDirectedDetails> ForceDirectedSource = new List<ForceDirectedDetails>()
              {

              new ForceDirectedDetails { Id = "Dev", Role = "Team", Color = "Orange", Width = 140, Height = 140 },

              new ForceDirectedDetails { Id = "Dept", Role = "PO-5", Manager = "Dev", Color = "Blue", Width = 100, Height = 100 },
              new ForceDirectedDetails { Id = "PO1", Role = "PO-1", Manager = "Dev", Color = "Blue", Width = 100, Height = 100 },
              new ForceDirectedDetails { Id = "PO2", Role = "PO-2", Manager = "Dev", Color = "Blue", Width = 100, Height = 100 },
              new ForceDirectedDetails { Id = "PO3", Role = "PO-3", Manager = "Dev", Color = "Blue", Width = 100, Height = 100 },
              new ForceDirectedDetails { Id = "PO4", Role = "PO-4", Manager = "Dev", Color = "Blue", Width = 100, Height = 100 },

              new ForceDirectedDetails { Id = "PO1_T1", Role = "Team-1", Manager = "PO1", Color = "Green", Width = 80, Height = 80 },
              new ForceDirectedDetails { Id = "PO1_T2", Role = "Team-2", Manager = "PO1", Color = "Green", Width = 80, Height = 80 },
              new ForceDirectedDetails { Id = "PO1_T3", Role = "Team-3", Manager = "PO1", Color = "Green", Width = 80, Height = 80 },
              new ForceDirectedDetails { Id = "PO1_T4", Role = "Team-4", Manager = "PO1", Color = "Green", Width = 80, Height = 80 },

              new ForceDirectedDetails { Id = "PO2_T1", Role = "Team-1", Manager = "PO2", Color = "Green", Width = 80, Height = 80 },
              new ForceDirectedDetails { Id = "PO2_T2", Role = "Team-2", Manager = "PO2", Color = "Green", Width = 80, Height = 80 },
              new ForceDirectedDetails { Id = "PO2_T3", Role = "Team-3", Manager = "PO2", Color = "Green", Width = 80, Height = 80 },
              new ForceDirectedDetails { Id = "PO2_T4", Role = "Team-4", Manager = "PO2", Color = "Green", Width = 80, Height = 80 },

              new ForceDirectedDetails { Id = "PO3_T1", Role = "Team-1", Manager = "PO3", Color = "Green", Width = 80, Height = 80 },
              new ForceDirectedDetails { Id = "PO3_T2", Role = "Team-2", Manager = "PO3", Color = "Green", Width = 80, Height = 80 },
              new ForceDirectedDetails { Id = "PO3_T3", Role = "Team-3", Manager = "PO3", Color = "Green", Width = 80, Height = 80 },
              new ForceDirectedDetails { Id = "PO3_T4", Role = "Team-4", Manager = "PO3", Color = "Green", Width = 80, Height = 80 },

              new ForceDirectedDetails { Id = "PO4_T1", Role = "Team-1", Manager = "PO4", Color = "Green", Width = 80, Height = 80 },
              new ForceDirectedDetails { Id = "PO4_T2", Role = "Team-2", Manager = "PO4", Color = "Green", Width = 80, Height = 80 },
              new ForceDirectedDetails { Id = "PO4_T3", Role = "Team-3", Manager = "PO4", Color = "Green", Width = 80, Height = 80 },
              new ForceDirectedDetails { Id = "PO4_T4", Role = "Team-4", Manager = "PO4", Color = "Green", Width = 80, Height = 80 },

              new ForceDirectedDetails { Id = "Sales", Role = "Sales Team", Manager = "Dept", Color = "Green", Width = 80, Height = 80 },
              new ForceDirectedDetails { Id = "AGM1", Role = "AGM-1", Manager = "Sales", Color = "Red", Width = 60, Height = 60 },
              new ForceDirectedDetails { Id = "AGM2", Role = "AGM-2", Manager = "Sales", Color = "Red", Width = 60, Height = 60 }

              };

            return ForceDirectedSource;

        }
    }


    public class ForceDirectedDetails
    {
        public string Id { get; set; }
        public string Role { get; set; }
        public string Manager { get; set; }
        public string Color { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
    }

   

}
