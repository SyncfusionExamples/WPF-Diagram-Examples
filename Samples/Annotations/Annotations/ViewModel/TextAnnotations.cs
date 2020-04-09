using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace Annotations
{
    public class TextAnnotations : DiagramViewModel
    {
        #region fields

        string ShapeName = "Center";
        public Button prevbutton = null;

        public ICommand SelectFontStyleCommand { get; set; }
        public ICommand SelectShapeCommand { get; set; }
        public ICommand ViewModeCommand { get; set; }
        public ICommand LabelInteractionCommand { get; set; }

        private Brush _fillcolor = new SolidColorBrush(Colors.Black);
        /// <summary>
        /// Gets or sets the Fillcolor for Ports
        /// </summary>
        public Brush FontColor
        {
            get
            {
                return _fillcolor;
            }
            set
            {
                if (_fillcolor != value)
                {
                    _fillcolor = value;
                    OnPropertyChanged("Fillcolor");
                    OnFillColorChanged(_fillcolor);
                }
            }
        }

        private FontFamily fontFamily = new FontFamily("Calibri");
        public FontFamily FontFamilyValue
        {
            get
            {
                return fontFamily;
            }
            set
            {
                if (fontFamily != value)
                {
                    fontFamily = value;
                    OnPropertyChanged("FontFamilyValue");
                    OnFontFamilyChanged(fontFamily);
                }
            }
        }

        private double textSize = 13;
        /// <summary>
        /// Gets or sets the Strokethickness for Ports
        /// </summary>
        public double TextSize
        {
            get
            {
                return textSize;
            }
            set
            {
                if (textSize != value)
                {
                    textSize = value;
                    OnPropertyChanged("TextSize");
                    OnFontSizeChanged(textSize);
                }
            }
        }

        private TextWrapping textWrapValue = TextWrapping.NoWrap;
        /// <summary>
        /// Gets or sets the Strokethickness for Ports
        /// </summary>
        public TextWrapping TextWrapValue
        {
            get
            {
                return textWrapValue;
            }
            set
            {
                if (textWrapValue != value)
                {
                    textWrapValue = value;
                    OnPropertyChanged("TextWrapValue");
                    OnTextWrappingChanged(textWrapValue);
                }
            }
        }

        private TextTrimming textTrimmingValue = TextTrimming.None;
        /// <summary>
        /// Gets or sets the Strokethickness for Ports
        /// </summary>
        public TextTrimming TextTrimmingValue
        {
            get
            {
                return textTrimmingValue;
            }
            set
            {
                if (textTrimmingValue != value)
                {
                    textTrimmingValue = value;
                    OnPropertyChanged("TextTrimmingValue");
                    OnTextTrimmingChanged(textTrimmingValue);
                }
            }
        }

        #endregion

        #region Constructor
        public TextAnnotations()
        {
            this.Nodes = new ObservableCollection<NodeViewModel>();
            this.Connectors = new ObservableCollection<ConnectorViewModel>();
            this.HorizontalRuler = new Ruler()
            {
                Orientation = Orientation.Horizontal,
            };
            this.VerticalRuler = new Ruler()
            {
                Orientation = Orientation.Vertical,
            };
            this.SnapSettings = new SnapSettings()
            {
                SnapConstraints = SnapConstraints.ShowLines,
                SnapToObject = SnapToObject.All,
            };

            //Create Node
            NodeViewModel newProject = CreateNode(130, 40, 450, 80, "Terminator", "New Project");
            NodeViewModel design = CreateNode(130, 40, 450, 160, "Rectangle", "Project planning and designing");
            NodeViewModel coding = CreateNode(130, 40, 450, 250, "Rectangle", "Coding");
            NodeViewModel testing = CreateNode(130, 40, 450, 360, "Rectangle", "Testing");
            NodeViewModel errors = CreateNode(200, 50, 450, 470, "Decision", "Is Errors?");
            NodeViewModel completed = CreateNode(130, 40, 450, 570, "Terminator", "Project Completed");

            CreateNodePort(design, "designPort", 0, 0.5);
            CreateNodePort(coding, "codingPort", 1, 0.5);
            CreateNodePort(errors, "errorsPort2", 1, 0.5);
            CreateNodePort(errors, "errorsPort1", 0, 0.5);



            //Add nodes into collection
            (this.Nodes as ObservableCollection<NodeViewModel>).Add(newProject);
            (this.Nodes as ObservableCollection<NodeViewModel>).Add(design);
            (this.Nodes as ObservableCollection<NodeViewModel>).Add(coding);
            (this.Nodes as ObservableCollection<NodeViewModel>).Add(testing);
            (this.Nodes as ObservableCollection<NodeViewModel>).Add(errors);
            (this.Nodes as ObservableCollection<NodeViewModel>).Add(completed);

            ConnectorViewModel connector1 = CreateConnector(newProject, design, "", null, null);
            ConnectorViewModel connector2 = CreateConnector(design, coding, "", null, null);
            ConnectorViewModel connector3 = CreateConnector(coding, testing, "Coding completed", null, null);
            ConnectorViewModel connector4 = CreateConnector(testing, errors, "Testing completed", null, null);
            ConnectorViewModel connector5 = CreateConnector(errors, completed, "No Errors", null, null);
            ConnectorViewModel connector6 = CreateConnector(errors, coding, "Coding errors", "errorsPort2", "codingPort");
            ConnectorViewModel connector7 = CreateConnector(errors, design, "Design errors", "errorsPort1", "designPort");



            (this.Connectors as ObservableCollection<ConnectorViewModel>).Add(connector1);
            (this.Connectors as ObservableCollection<ConnectorViewModel>).Add(connector2);
            (this.Connectors as ObservableCollection<ConnectorViewModel>).Add(connector3);
            (this.Connectors as ObservableCollection<ConnectorViewModel>).Add(connector4);
            (this.Connectors as ObservableCollection<ConnectorViewModel>).Add(connector5);
            (this.Connectors as ObservableCollection<ConnectorViewModel>).Add(connector6);
            (this.Connectors as ObservableCollection<ConnectorViewModel>).Add(connector7);



            this.SelectedItems = new SelectorViewModel();
            (this.SelectedItems as SelectorViewModel).SelectorConstraints &= ~(SelectorConstraints.QuickCommands);
            SelectShapeCommand = new Command(OnSelectShapeCommandExecute);
            ViewModeCommand = new Command(OnViewModeCommandExecute);
            LabelInteractionCommand = new Command(OnLabelInteractionCommandExecute);
            SelectFontStyleCommand = new Command(OnSelectFontStyleCommandExecute);
            this.ItemSelectedCommand = new Command(OnItemSelectedCommandExecute);
        }
        #endregion

        #region helper methods

        /// <summary>
        /// Method to change the stroke color of the nodes
        /// </summary>
        /// <param name="fillcolor"></param>
        private void OnFontSizeChanged(double value)
        {
            double val = value;
            foreach (NodeViewModel node in (this.SelectedItems as SelectorViewModel).Nodes as IEnumerable<object>)
            {
                foreach (IAnnotation annotation in node.Annotations as ObservableCollection<IAnnotation>)
                {
                    (annotation as TextAnnotationViewModel).FontSize = val;
                }
            }
            foreach (ConnectorViewModel conn in (this.SelectedItems as SelectorViewModel).Connectors as IEnumerable<object>)
            {
                foreach (IAnnotation annotation in conn.Annotations as ObservableCollection<IAnnotation>)
                {
                    (annotation as TextAnnotationViewModel).FontSize = val;
                }
            }
        }

        private void CreateNodePort(NodeViewModel node1, string id, double nodeoffsetx, double nodeoffsety)
        {
            NodePortViewModel nodePort = new NodePortViewModel()
            {
                ID = id,
                NodeOffsetX = nodeoffsetx,
                NodeOffsetY = nodeoffsety,
                PortVisibility = PortVisibility.Visible,
            };

            (node1.Ports as PortCollection).Add(nodePort);
        }

        /// <summary>
        /// Method to change the stroke color of the nodes
        /// </summary>
        /// <param name="fillcolor"></param>
        private void OnTextTrimmingChanged(TextTrimming value)
        {
            foreach (NodeViewModel node in (this.SelectedItems as SelectorViewModel).Nodes as IEnumerable<object>)
            {
                foreach (IAnnotation annotation in node.Annotations as ObservableCollection<IAnnotation>)
                {
                    (annotation as TextAnnotationViewModel).TextTrimming = value;
                }
            }
            foreach (ConnectorViewModel conn in (this.SelectedItems as SelectorViewModel).Connectors as IEnumerable<object>)
            {
                foreach (IAnnotation annotation in conn.Annotations as ObservableCollection<IAnnotation>)
                {
                    (annotation as TextAnnotationViewModel).TextTrimming = value;
                }
            }
        }

        /// <summary>
        /// Method to change the stroke color of the nodes
        /// </summary>
        /// <param name="fillcolor"></param>
        private void OnTextWrappingChanged(TextWrapping value)
        {
            foreach (NodeViewModel node in (this.SelectedItems as SelectorViewModel).Nodes as IEnumerable<object>)
            {
                foreach (IAnnotation annotation in node.Annotations as ObservableCollection<IAnnotation>)
                {
                    (annotation as TextAnnotationViewModel).TextWrapping = value;
                }
            }
            foreach (ConnectorViewModel conn in (this.SelectedItems as SelectorViewModel).Connectors as IEnumerable<object>)
            {
                foreach (IAnnotation annotation in conn.Annotations as ObservableCollection<IAnnotation>)
                {
                    (annotation as TextAnnotationViewModel).TextWrapping = value;
                }
            }
        }

        private void OnFontFamilyChanged(FontFamily val)
        {
            string value = val.ToString();
            foreach (NodeViewModel node in (this.SelectedItems as SelectorViewModel).Nodes as IEnumerable<object>)
            {
                foreach (IAnnotation annotation in node.Annotations as ObservableCollection<IAnnotation>)
                {
                    (annotation as TextAnnotationViewModel).FontFamily = new FontFamily(value);
                }
            }
            foreach (ConnectorViewModel conn in (this.SelectedItems as SelectorViewModel).Connectors as IEnumerable<object>)
            {
                foreach (IAnnotation annotation in conn.Annotations as ObservableCollection<IAnnotation>)
                {
                    (annotation as TextAnnotationViewModel).FontFamily = new FontFamily(value);
                }
            }
        }


        public TextDecorationCollection Decoration;
        public bool strikeFlag = false;
        public bool underLineFlag = false;

        private void OnSelectFontStyleCommandExecute(object parameter)
        {
            Button button = parameter as Button;

            string fontStyleName = button.Name.ToString();

            if (fontStyleName.Equals("bold"))
            {
                foreach (NodeViewModel node in (this.SelectedItems as SelectorViewModel).Nodes as IEnumerable<object>)
                {
                    foreach (IAnnotation annotation in node.Annotations as ObservableCollection<IAnnotation>)
                    {
                       if((annotation as TextAnnotationViewModel).FontWeight == FontWeights.Bold)
                        {
                            (annotation as TextAnnotationViewModel).FontWeight = FontWeights.Normal;
                        }
                        else
                        {
                            (annotation as TextAnnotationViewModel).FontWeight = FontWeights.Bold;
                        }
                    }
                }
                foreach (ConnectorViewModel conn in (this.SelectedItems as SelectorViewModel).Connectors as IEnumerable<object>)
                {
                    foreach (IAnnotation annotation in conn.Annotations as ObservableCollection<IAnnotation>)
                    {
                        if ((annotation as TextAnnotationViewModel).FontWeight == FontWeights.Bold)
                        {
                            (annotation as TextAnnotationViewModel).FontWeight = FontWeights.Normal;
                        }
                        else
                        {
                            (annotation as TextAnnotationViewModel).FontWeight = FontWeights.Bold;
                        }
                    }
                }
            }
            else if (fontStyleName.Equals("italic"))
            {
                foreach (NodeViewModel node in (this.SelectedItems as SelectorViewModel).Nodes as IEnumerable<object>)
                {
                    foreach (IAnnotation annotation in node.Annotations as ObservableCollection<IAnnotation>)
                    {
                        if ((annotation as TextAnnotationViewModel).FontStyle == FontStyles.Italic)
                        {
                            (annotation as TextAnnotationViewModel).FontStyle = FontStyles.Normal;
                        }
                        else
                        {
                            (annotation as TextAnnotationViewModel).FontStyle = FontStyles.Italic;
                        }
                    }
                }
                foreach (ConnectorViewModel conn in (this.SelectedItems as SelectorViewModel).Connectors as IEnumerable<object>)
                {
                    foreach (IAnnotation annotation in conn.Annotations as ObservableCollection<IAnnotation>)
                    {
                        if ((annotation as TextAnnotationViewModel).FontStyle == FontStyles.Italic)
                        {
                            (annotation as TextAnnotationViewModel).FontStyle = FontStyles.Normal;
                        }
                        else
                        {
                            (annotation as TextAnnotationViewModel).FontStyle = FontStyles.Italic;
                        }
                    }
                }
            }
            else if (fontStyleName.Equals("underline"))
            {
                if (underLineFlag)
                {
                    underLineFlag = false;
                }
                else
                {
                    underLineFlag = true;
                }

                this.OnUnderLineChanged(underLineFlag);
            }
            else if (fontStyleName.Equals("Strike"))
            {
                if (strikeFlag)
                {
                    strikeFlag = false;
                }
                else
                {
                    strikeFlag = true;
                }

                this.OnStrikeChanged(strikeFlag);
            }
        }

        private void OnUnderLineChanged(bool UnderLine)
        {
            if (UnderLine)
            {
                if (Decoration == null)
                {
                    Decoration = new TextDecorationCollection();
                    Decoration.Add(TextDecorations.Underline);
                }
                else
                {
                    Decoration = new TextDecorationCollection();
                    Decoration.Add(TextDecorations.Strikethrough);
                    Decoration.Add(TextDecorations.Underline);
                }
            }
            else
            {
                if (Decoration != null && Decoration.Contains(TextDecorations.Strikethrough[0]))
                {
                    Decoration = new TextDecorationCollection();
                    Decoration.Add(TextDecorations.Strikethrough);
                }
                else
                    Decoration = null;
            }

            this.SetTextDecorations();
        }

        private void OnStrikeChanged(bool Strike)
        {
            if (Strike)
            {
                if (Decoration == null)
                {
                    Decoration = new TextDecorationCollection();
                    Decoration.Add(TextDecorations.Strikethrough);
                }
                else
                {
                    Decoration = new TextDecorationCollection();
                    Decoration.Add(TextDecorations.Strikethrough);
                    Decoration.Add(TextDecorations.Underline);
                }
            }
            else
            {
                if (Decoration != null && Decoration.Contains(TextDecorations.Underline[0]))
                {
                    Decoration = new TextDecorationCollection();
                    Decoration.Add(TextDecorations.Underline);
                }
                else
                    Decoration = null;
            }
            this.SetTextDecorations();
        }

        private void SetTextDecorations()
        {
            foreach (NodeViewModel node in (this.SelectedItems as SelectorViewModel).Nodes as IEnumerable<object>)
            {
                foreach (IAnnotation annotation in node.Annotations as ObservableCollection<IAnnotation>)
                {
                    (annotation as TextAnnotationViewModel).TextDecorations = Decoration;
                }
            }
            foreach (ConnectorViewModel conn in (this.SelectedItems as SelectorViewModel).Connectors as IEnumerable<object>)
            {
                foreach (IAnnotation annotation in conn.Annotations as ObservableCollection<IAnnotation>)
                {
                    (annotation as TextAnnotationViewModel).TextDecorations = Decoration;
                }
            }
        }

        private void OnLabelInteractionCommandExecute(object parameter)
        {
            if (this != null)
            {
                if ((bool)parameter)
                {
                    foreach (NodeViewModel node in (this.SelectedItems as SelectorViewModel).Nodes as IEnumerable<object>)
                    {
                        foreach (IAnnotation annotation in node.Annotations as ObservableCollection<IAnnotation>)
                        {
                            annotation.Constraints &= ~(AnnotationConstraints.InheritResizable | AnnotationConstraints.InheritRotatable| AnnotationConstraints.InheritDraggable | AnnotationConstraints.InheritSelectable);
                            annotation.Constraints |= AnnotationConstraints.Resizable| AnnotationConstraints.Rotatable |AnnotationConstraints.Draggable | AnnotationConstraints.Selectable;
                        }
                    }
                    foreach (ConnectorViewModel conn in (this.SelectedItems as SelectorViewModel).Connectors as IEnumerable<object>)
                    {
                        foreach (IAnnotation annotation in conn.Annotations as ObservableCollection<IAnnotation>)
                        {
                            annotation.Constraints &= ~(AnnotationConstraints.InheritResizable | AnnotationConstraints.InheritRotatable | AnnotationConstraints.InheritDraggable | AnnotationConstraints.InheritSelectable);
                            annotation.Constraints |= AnnotationConstraints.Resizable | AnnotationConstraints.Rotatable | AnnotationConstraints.Draggable | AnnotationConstraints.Selectable;
                        }
                    }
                }
                else
                {
                    foreach (NodeViewModel node in (this.SelectedItems as SelectorViewModel).Nodes as IEnumerable<object>)
                    {
                        foreach (IAnnotation annotation in node.Annotations as ObservableCollection<IAnnotation>)
                        {
                            annotation.Constraints = AnnotationConstraints.Default;
                        }
                    }
                    foreach (ConnectorViewModel conn in (this.SelectedItems as SelectorViewModel).Connectors as IEnumerable<object>)
                    {
                        foreach (IAnnotation annotation in conn.Annotations as ObservableCollection<IAnnotation>)
                        {
                            annotation.Constraints = AnnotationConstraints.Default;
                        }
                    }
                }
            }
        }
        private void OnViewModeCommandExecute(object parameter)
        {
            if (this != null)
            {
                if ((bool)parameter)
                {
                    foreach (NodeViewModel node in (this.SelectedItems as SelectorViewModel).Nodes as IEnumerable<object>)
                    {
                        foreach (IAnnotation annotation in node.Annotations as ObservableCollection<IAnnotation>)
                        {
                            annotation.ReadOnly = true;
                        }
                    }
                    foreach (ConnectorViewModel conn in (this.SelectedItems as SelectorViewModel).Connectors as IEnumerable<object>)
                    {
                        foreach (IAnnotation annotation in conn.Annotations as ObservableCollection<IAnnotation>)
                        {
                            annotation.ReadOnly = true;
                        }
                    }
                }
                else
                {
                    foreach (NodeViewModel node in (this.SelectedItems as SelectorViewModel).Nodes as IEnumerable<object>)
                    {
                        foreach (IAnnotation annotation in node.Annotations as ObservableCollection<IAnnotation>)
                        {
                            annotation.ReadOnly = false;
                        }
                    }
                    foreach (ConnectorViewModel conn in (this.SelectedItems as SelectorViewModel).Connectors as IEnumerable<object>)
                    {
                        foreach (IAnnotation annotation in conn.Annotations as ObservableCollection<IAnnotation>)
                        {
                            annotation.ReadOnly = false;
                        }
                    }
                }
            }
        }

        private void OnSelectShapeCommandExecute(object parameter)
        {
            Button button = parameter as Button;

            this.ShapeName = button.Name.ToString();

            if (prevbutton != null)
            {
                prevbutton.Style = Application.Current.Resources["ButtonStyle"] as Style;
            }
            button.Style = Application.Current.Resources["SelectedButtonStyle"] as Style;

            if (ShapeName.Equals("Center"))
            {
                foreach(NodeViewModel node in (this.SelectedItems as SelectorViewModel).Nodes as IEnumerable<object>)
                {
                    foreach(IAnnotation annotation in node.Annotations as ObservableCollection<IAnnotation>)
                    {
                        annotation.Offset = new Point(0.5, 0.5);
                        annotation.Margin = new Thickness(0);
                    }
                }
            }
            else if (ShapeName.Equals("TopLeft"))
            {
                foreach (NodeViewModel node in (this.SelectedItems as SelectorViewModel).Nodes as IEnumerable<object>)
                {
                    foreach (IAnnotation annotation in node.Annotations as ObservableCollection<IAnnotation>)
                    {
                        annotation.Offset = new Point(0, 0);
                        annotation.Margin = new Thickness(10, 10, 0, 0);
                    }
                }
            }
            else if (ShapeName.Equals("TopRight"))
            {
                foreach (NodeViewModel node in (this.SelectedItems as SelectorViewModel).Nodes as IEnumerable<object>)
                {
                    foreach (IAnnotation annotation in node.Annotations as ObservableCollection<IAnnotation>)
                    {
                        annotation.Offset = new Point(1, 0);
                        annotation.Margin = new Thickness(-10, 10, 0, 0);
                    }
                }
            }
            else if (ShapeName.Equals("BottomLeft"))
            {
                foreach (NodeViewModel node in (this.SelectedItems as SelectorViewModel).Nodes as IEnumerable<object>)
                {
                    foreach (IAnnotation annotation in node.Annotations as ObservableCollection<IAnnotation>)
                    {
                        annotation.Offset = new Point(0, 1);
                        annotation.Margin = new Thickness(10, -10, 0, 0);
                    }
                }
            }
            else if (ShapeName.Equals("BottomRight"))
            {
                foreach (NodeViewModel node in (this.SelectedItems as SelectorViewModel).Nodes as IEnumerable<object>)
                {
                    foreach (IAnnotation annotation in node.Annotations as ObservableCollection<IAnnotation>)
                    {
                        annotation.Offset = new Point(1, 1);
                        annotation.Margin = new Thickness(-10, -10, 0, 0);
                    }
                }
            }
            else if (ShapeName.Equals("MarginText"))
            {
                foreach (NodeViewModel node in (this.SelectedItems as SelectorViewModel).Nodes as IEnumerable<object>)
                {
                    foreach (IAnnotation annotation in node.Annotations as ObservableCollection<IAnnotation>)
                    {
                        annotation.Offset = new Point(0.5, 1);
                        annotation.Margin = new Thickness(0, 10, 0, 0);
                    }
                }
            }
            else if (ShapeName.Equals("SourceText"))
            {
                foreach (ConnectorViewModel node in (this.SelectedItems as SelectorViewModel).Connectors as IEnumerable<object>)
                {
                    foreach (IAnnotation annotation in node.Annotations as ObservableCollection<IAnnotation>)
                    {
                        annotation.Length = 0;
                        annotation.Margin = new Thickness(0, 0, 0, 0);
                    }
                }
            }
            else if (ShapeName.Equals("TargetText"))
            {
                foreach (ConnectorViewModel node in (this.SelectedItems as SelectorViewModel).Connectors as IEnumerable<object>)
                {
                    foreach (IAnnotation annotation in node.Annotations as ObservableCollection<IAnnotation>)
                    {
                        annotation.Length = 1;
                        annotation.Margin = new Thickness(0, 0, 0, 0);
                    }
                }
            }
            else if (ShapeName.Equals("AboveCenter"))
            {
                foreach (ConnectorViewModel node in (this.SelectedItems as SelectorViewModel).Connectors as IEnumerable<object>)
                {
                    foreach (IAnnotation annotation in node.Annotations as ObservableCollection<IAnnotation>)
                    {
                        annotation.Length = 0.5;
                        annotation.Margin = new Thickness(0, -10, 0, 0);
                    }
                }
            }
            else if (ShapeName.Equals("BelowCenter"))
            {
                foreach (ConnectorViewModel node in (this.SelectedItems as SelectorViewModel).Connectors as IEnumerable<object>)
                {
                    foreach (IAnnotation annotation in node.Annotations as ObservableCollection<IAnnotation>)
                    {
                        annotation.Length = 0.5;
                        annotation.Margin = new Thickness(0, 10, 0, 0);
                    }
                }
            }
            else if (ShapeName.Equals("CenterText"))
            {
                foreach (ConnectorViewModel node in (this.SelectedItems as SelectorViewModel).Connectors as IEnumerable<object>)
                {
                    foreach (IAnnotation annotation in node.Annotations as ObservableCollection<IAnnotation>)
                    {
                        annotation.Length = 0.5;
                        annotation.Margin = new Thickness(0, 0, 0, 0);
                    }
                }
            }

            prevbutton = parameter as Button;
        }

        /// <summary>
        /// Method to change the fill color of Port
        /// </summary>
        /// <param name="fillcolor"></param>
        private void OnFillColorChanged(Brush fillcolor)
        {
            Brush value = fillcolor;
            foreach (NodeViewModel node in (this.SelectedItems as SelectorViewModel).Nodes as IEnumerable<object>)
            {
                foreach (IAnnotation annotation in node.Annotations as ObservableCollection<IAnnotation>)
                {
                    (annotation as TextAnnotationViewModel).Foreground = value;
                }
            }
            foreach (ConnectorViewModel conn in (this.SelectedItems as SelectorViewModel).Connectors as IEnumerable<object>)
            {
                foreach (IAnnotation annotation in conn.Annotations as ObservableCollection<IAnnotation>)
                {
                    (annotation as TextAnnotationViewModel).Foreground = value;
                }
            }
        }

        private NodeViewModel CreateNode(double unitWidth, double unitHeight, double offsetx, double offsety, string shape, string text)
        {
            NodeViewModel node = new NodeViewModel()
            {
                UnitWidth = unitWidth,
                UnitHeight = unitHeight,
                OffsetX = offsetx,
                OffsetY = offsety,
                Shape = App.Current.Resources[shape],
                Annotations = new ObservableCollection<IAnnotation>()
                {
                   new TextAnnotationViewModel()
                   {
                       Text = text,
                       FontSize = 13,
                       Foreground = new SolidColorBrush(Colors.Black),
                       TextWrapping = TextWrapping.Wrap,
                       FontWeight = FontWeights.Bold,
                   },
                }
            };

            return node;
        }

        private ConnectorViewModel CreateConnector(NodeViewModel sourceNode, NodeViewModel targetNode, string text, 
            string sourcePort, string targetPort)
        {
            ConnectorViewModel connector = new ConnectorViewModel()
            {
                SourceNode = sourceNode,
                TargetNode = targetNode,
                SourcePortID = sourcePort,
                TargetPortID = targetPort,
                Annotations = new ObservableCollection<IAnnotation>()
                {
                   new TextAnnotationViewModel()
                   {
                       Text = text,
                       FontSize = 13,
                       Foreground = new SolidColorBrush(Colors.Black),
                       Length = 0.5,
                       RotationReference = RotationReference.Page,
                       TextWrapping = TextWrapping.Wrap,
                       FontWeight = FontWeights.Bold,
                   },
                }
            };

            return connector;

        }

        private void AddAnnotations(NodeViewModel node, string text, VerticalAlignment verticalAlignment, HorizontalAlignment horizontalAlignment)
        {
            node.Annotations = new ObservableCollection<IAnnotation>()
            {
               new TextAnnotationViewModel()
               {
                   Text = text,
                   FontSize = 13,
                   Foreground = new SolidColorBrush(Colors.Black),
                   VerticalAlignment = verticalAlignment,
                   HorizontalAlignment = horizontalAlignment,
               }
            };
        }

        private void OnItemSelectedCommandExecute(object parameter)
        {
            if ((parameter as ItemSelectedEventArgs).Item is INode)
            {
                NodeViewModel node = (parameter as ItemSelectedEventArgs).Item as NodeViewModel;
                foreach (IAnnotation annotation in node.Annotations as ObservableCollection<IAnnotation>)
                {
                    this.FontFamilyValue = (annotation as TextAnnotationViewModel).FontFamily;
                    this.FontColor = (annotation as TextAnnotationViewModel).Foreground;
                    this.TextSize = (annotation as TextAnnotationViewModel).FontSize;
                    this.TextWrapValue = (annotation as TextAnnotationViewModel).TextWrapping;
                    this.TextTrimmingValue = (annotation as TextAnnotationViewModel).TextTrimming;
                   
                }
            }
            else if ((parameter as ItemSelectedEventArgs).Item is IConnector)
            {
                ConnectorViewModel connector = (parameter as ItemSelectedEventArgs).Item as ConnectorViewModel;
                foreach (IAnnotation annotation in connector.Annotations as ObservableCollection<IAnnotation>)
                {
                    this.FontFamilyValue = (annotation as TextAnnotationViewModel).FontFamily;
                    this.FontColor = (annotation as TextAnnotationViewModel).Foreground;
                    this.TextSize = (annotation as TextAnnotationViewModel).FontSize;
                    this.TextWrapValue = (annotation as TextAnnotationViewModel).TextWrapping;
                    this.TextTrimmingValue = (annotation as TextAnnotationViewModel).TextTrimming;
                   
                }
            }
        }

        #endregion
    }

    public class ColorToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                var converter = new System.Windows.Media.BrushConverter();

                Brush brush = (Brush)converter.ConvertFromString(value.ToString());

                return (brush as SolidColorBrush).Color;
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                var converter = new System.Windows.Media.BrushConverter();
                var brush = (Brush)converter.ConvertFromString(value.ToString());
                return brush;
            }
            return value;
        }
    }
}
