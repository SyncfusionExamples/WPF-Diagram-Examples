using Microsoft.Win32;
using SwimLane.Utility;
using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Controls;
using Syncfusion.Windows.Shared;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
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
            ItemAddedCommand = new DelegateCommand(OnItemAdded);
            HorizontalRuler = new Ruler() { Orientation = Orientation.Horizontal };
            VerticalRuler = new Ruler() { Orientation = Orientation.Vertical };
            InitializeDiagram();
            NodeChangedCommand = new DelegateCommand(OnNodeChangedCommand);
        }

        private void OnItemAdded(object obj)
        {
            var args = obj as ItemAddedEventArgs;
            if(args.OriginalSource != null && args.OriginalSource is LaneViewModel)
            {
                if(args.Item != null && args.Item is CustomNodeVM)
                {
                    (args.Item as CustomNodeVM).ParentLane = args.OriginalSource as LaneViewModel;
                }
            }
        }

        private void OnNodeChangedCommand(object obj)
        {
            var args = obj as ChangeEventArgs<object, NodeChangedEventArgs>;
            CustomNodeVM node = args.Item as CustomNodeVM;
            if(node != null && node.ParentLane != null && args.NewValue.InteractionState == NodeChangedInteractionState.Dragging)
            {
                LaneViewModel parentlane = (args.Item as CustomNodeVM).ParentLane as LaneViewModel;
                args.BoundaryConstraints = (parentlane.Info as ILaneInfo).Bounds;
            }
        }


        private void InitializeDiagram()
        {
            //Initialize SwimlaneCollection to SfDiagram
            this.Swimlanes = new SwimlaneCollection();

            //Creating the SwimlaneViewModel
            SwimlaneViewModel swimlane = new SwimlaneViewModel()
            {
                UnitWidth = 450,
                UnitHeight = 420,
                OffsetX = 400,
                OffsetY = 450,
                Orientation = Orientation.Horizontal,
            };
            LaneViewModel lane1 = new LaneViewModel()
            {
                UnitHeight = 100,
            };
            swimlane.Lanes = new LaneCollection()
            {
               lane1
            };

            CustomNodeVM node = new CustomNodeVM() { UnitHeight = 50, UnitWidth = 50, LaneOffsetX = 100, LaneOffsetY = 30 };

            CustomNodeVM node1 = new CustomNodeVM() { UnitHeight = 50, UnitWidth = 50, LaneOffsetX = 250, LaneOffsetY = 30 };

            (this.Nodes as NodeCollection).Add(node);
            (this.Nodes as NodeCollection).Add(node1);

            (lane1.Children as LaneChildren).Add(node);
            (lane1.Children as LaneChildren).Add(node1);

            //Add Swimlane to Swimlanes property of the Diagram
            (this.Swimlanes as SwimlaneCollection).Add(swimlane);
        }
    }

    /// <summary>
    /// Represents a class to add custom proerties to add parent lane property to the node.
    /// </summary>
    public class CustomNodeVM : NodeViewModel
    {
        private LaneViewModel parentLane = null;
        public LaneViewModel ParentLane
        {
            get
            {
                return parentLane;
            }
            set
            {
                if (parentLane != value)
                {
                    parentLane = value;
                    OnPropertyChanged("ParentLane");
                }
            }
        }
    }
}
