using Microsoft.Win32;
using SwimLane.Utility;
using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Swimlane.ViewModel
{
    public class DiagramVM : DiagramViewModel
    {
        public DiagramVM()
        {
            SnapSettings = new SnapSettings()
            {
                SnapConstraints = SnapConstraints.None,
            };

            SelectedItems = new SelectorViewModel();

            HorizontalRuler = new Ruler() { Orientation = Orientation.Horizontal };
            VerticalRuler = new Ruler() { Orientation = Orientation.Vertical };
            InitializeDiagram();

        }

        private void InitializeDiagram()
        {
            //Initialize SwimlaneCollection to SfDiagram
            this.Swimlanes = new SwimlaneCollection();

            //Creating the SwimlaneViewModel
            SwimlaneViewModel swimlane = new SwimlaneViewModel()
            {
                UnitWidth = 450,
                UnitHeight = 120,
                OffsetX = 300,
                OffsetY = 150,
                Orientation = Orientation.Horizontal,
            };
            //Creating Header for SwimlaneViewModel
            swimlane.Header = new SwimlaneHeader()
            {
                UnitHeight = 32,
                Annotation = new AnnotationEditorViewModel()
                {
                    Content = "SALES PROCESS FLOW CHART"
                },
            };
            LaneViewModel lane1 = new LaneViewModel()
            {
                UnitHeight = 100,
                Header = new SwimlaneHeader()
                {
                    UnitHeight = 30,
                    Annotation = new AnnotationEditorViewModel() { Content = "Consumer" }
                }
            };
            swimlane.Lanes = new LaneCollection()
            {
               lane1
            };
            NodeViewModel node = new NodeViewModel() { UnitHeight = 50, UnitWidth = 50, LaneOffsetX = 100, LaneOffsetY = 30 };
            NodeViewModel node1 = new NodeViewModel() { UnitHeight = 50, UnitWidth = 50, LaneOffsetX = 250, LaneOffsetY = 30 };
            (this.Nodes as NodeCollection).Add(node);
            (this.Nodes as NodeCollection).Add(node1);
            (lane1.Children as LaneChildren).Add(node);
            (lane1.Children as LaneChildren).Add(node1);

            //Add Swimlane to Swimlanes property of the Diagram
            (this.Swimlanes as SwimlaneCollection).Add(swimlane);
        }
    }
}
