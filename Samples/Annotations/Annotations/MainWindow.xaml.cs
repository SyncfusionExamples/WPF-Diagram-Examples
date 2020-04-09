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
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Annotations
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
            (Diagram.Info as IGraphInfo).ItemSelectedEvent += MainWindow_ItemSelectedEvent;
            (Diagram.Info as IGraphInfo).ItemUnSelectedEvent += MainWindow_ItemUnSelectedEvent;
        }

        private void MainWindow_ItemUnSelectedEvent(object sender, DiagramEventArgs args)
        {
            (Diagram.DataContext as TextAnnotations).Decoration = null;
            viewMode.IsChecked = false;
            labelInteraction.IsChecked = false;
            if (((Diagram.SelectedItems as SelectorViewModel).Nodes as IEnumerable<object>).Count() == 0 &&
               ((Diagram.SelectedItems as SelectorViewModel).Nodes as IEnumerable<object>).Count() == 0)
            {
                (Diagram.DataContext as TextAnnotations).prevbutton.Style = Application.Current.Resources["ButtonStyle"] as Style;
            }
        }

        private void MainWindow_ItemSelectedEvent(object sender, DiagramEventArgs args)
        {
            propertyPanel.IsEnabled = true;
            if ((Diagram.DataContext as TextAnnotations).prevbutton != null)
            {
                (Diagram.DataContext as TextAnnotations).prevbutton.Style = Application.Current.Resources["ButtonStyle"] as Style;
            }

            if ((args as ItemSelectedEventArgs).Item is INode)
            {
                NodeViewModel node = (args as ItemSelectedEventArgs).Item as NodeViewModel;
                foreach (IAnnotation annotation in node.Annotations as ObservableCollection<IAnnotation>)
                {
                    #region UnderLine and Strike for Node
                    if ((annotation as TextAnnotationViewModel).TextDecorations != null && (annotation as TextAnnotationViewModel).TextDecorations.Contains(TextDecorations.Underline[0]))
                    {
                        (Diagram.DataContext as TextAnnotations).underLineFlag = true;
                        if ((Diagram.DataContext as TextAnnotations).Decoration == null)
                            (Diagram.DataContext as TextAnnotations).Decoration = new TextDecorationCollection();
                        (Diagram.DataContext as TextAnnotations).Decoration.Add(TextDecorations.Underline);
                    }
                    else
                    {
                        (Diagram.DataContext as TextAnnotations).underLineFlag = false;
                    }

                    if ((annotation as TextAnnotationViewModel).TextDecorations != null &&
                        (annotation as TextAnnotationViewModel).TextDecorations.Contains(TextDecorations.Strikethrough[0]))
                    {
                        (Diagram.DataContext as TextAnnotations).strikeFlag = true;
                        if ((Diagram.DataContext as TextAnnotations).Decoration == null)
                            (Diagram.DataContext as TextAnnotations).Decoration = new TextDecorationCollection();
                        (Diagram.DataContext as TextAnnotations).Decoration.Add(TextDecorations.Strikethrough);
                    }
                    else
                    {
                        (Diagram.DataContext as TextAnnotations).strikeFlag = false;
                    }
                    #endregion

                    #region Edit Mode

                    if (annotation.ReadOnly)
                    {
                        viewMode.IsChecked = true;
                    }
                    else
                    {
                        viewMode.IsChecked = false;
                    }
                    #endregion

                    #region Label interaction

                    if (annotation.Constraints.Contains(AnnotationConstraints.Selectable))
                    {
                        labelInteraction.IsChecked = true;
                    }
                    else
                    {
                        labelInteraction.IsChecked = false;
                    }
                    #endregion

                    #region Button selection for Node
                    if (annotation.Offset == new Point(0.5, 0.5))
                    {
                        this.Center.Style = Application.Current.Resources["SelectedButtonStyle"] as Style;
                        (Diagram.DataContext as TextAnnotations).prevbutton = Center as Button;
                        break;
                    }
                    else if (annotation.Offset == new Point(0,0))
                    {
                        this.TopLeft.Style = Application.Current.Resources["SelectedButtonStyle"] as Style;
                        (Diagram.DataContext as TextAnnotations).prevbutton = TopLeft as Button;
                        break;
                    }
                    else if (annotation.Offset == new Point(1, 0))
                    {
                        this.TopRight.Style = Application.Current.Resources["SelectedButtonStyle"] as Style;
                        (Diagram.DataContext as TextAnnotations).prevbutton = TopRight as Button;
                        break;
                    }
                    else if (annotation.Offset == new Point(0, 1))
                    {
                        this.BottomLeft.Style = Application.Current.Resources["SelectedButtonStyle"] as Style;
                        (Diagram.DataContext as TextAnnotations).prevbutton = BottomLeft as Button;
                        break;
                    }
                    else if (annotation.Offset == new Point(1, 1))
                    {
                        this.BottomRight.Style = Application.Current.Resources["SelectedButtonStyle"] as Style;
                        (Diagram.DataContext as TextAnnotations).prevbutton = BottomRight as Button;
                        break;
                    }
                    else if (annotation.Offset == new Point(0.5, 1))
                    {
                        this.MarginText.Style = Application.Current.Resources["SelectedButtonStyle"] as Style;
                        (Diagram.DataContext as TextAnnotations).prevbutton = MarginText as Button;
                        break;
                    }
                    #endregion
                }
            }
            else
            {
                ConnectorViewModel connector = (args as ItemSelectedEventArgs).Item as ConnectorViewModel;
                foreach (IAnnotation annotation in connector.Annotations as ObservableCollection<IAnnotation>)
                {
                    #region Strike and UnderLine for Connector
                    if ((annotation as TextAnnotationViewModel).TextDecorations != null && (annotation as TextAnnotationViewModel).TextDecorations.Contains(TextDecorations.Underline[0]))
                    {
                        (Diagram.DataContext as TextAnnotations).underLineFlag = true;
                        if ((Diagram.DataContext as TextAnnotations).Decoration == null)
                            (Diagram.DataContext as TextAnnotations).Decoration = new TextDecorationCollection();
                        (Diagram.DataContext as TextAnnotations).Decoration.Add(TextDecorations.Underline);
                    }
                    else
                    {
                        (Diagram.DataContext as TextAnnotations).underLineFlag = false;
                    }

                    if ((annotation as TextAnnotationViewModel).TextDecorations != null &&
                        (annotation as TextAnnotationViewModel).TextDecorations.Contains(TextDecorations.Strikethrough[0]))
                    {
                        (Diagram.DataContext as TextAnnotations).strikeFlag = true;
                        if ((Diagram.DataContext as TextAnnotations).Decoration == null)
                            (Diagram.DataContext as TextAnnotations).Decoration = new TextDecorationCollection();
                        (Diagram.DataContext as TextAnnotations).Decoration.Add(TextDecorations.Strikethrough);
                    }
                    else
                    {
                        (Diagram.DataContext as TextAnnotations).strikeFlag = false;
                    }
                    #endregion

                    #region Edit Mode

                    if (annotation.ReadOnly)
                    {
                        viewMode.IsChecked = true;
                    }
                    else
                    {
                        viewMode.IsChecked = false;
                    }
                    #endregion

                    #region Button Selection for connectors
                    if (annotation.Length == 0)
                    {
                        this.SourceText.Style = Application.Current.Resources["SelectedButtonStyle"] as Style;
                        (Diagram.DataContext as TextAnnotations).prevbutton = SourceText as Button;
                        break;
                    }
                    else if (annotation.Length == 1)
                    {
                        this.TargetText.Style = Application.Current.Resources["SelectedButtonStyle"] as Style;
                        (Diagram.DataContext as TextAnnotations).prevbutton = TargetText as Button;
                        break;
                    }
                    else if (annotation.Length == 0.5 && annotation.Margin == new Thickness(0, -10, 0, 0))
                    {
                        this.AboveCenter.Style = Application.Current.Resources["SelectedButtonStyle"] as Style;
                        (Diagram.DataContext as TextAnnotations).prevbutton = AboveCenter as Button;
                        break;
                    }
                    else if (annotation.Length == 0.5 && annotation.Margin == new Thickness(0, 10, 0, 0))
                    {
                        this.BelowCenter.Style = Application.Current.Resources["SelectedButtonStyle"] as Style;
                        (Diagram.DataContext as TextAnnotations).prevbutton = BelowCenter as Button;
                        break;
                    }
                    else if (annotation.Length == 0.5 && annotation.Margin == new Thickness(0, 0, 0, 0))
                    {
                        this.CenterText.Style = Application.Current.Resources["SelectedButtonStyle"] as Style;
                        (Diagram.DataContext as TextAnnotations).prevbutton = CenterText as Button;
                        break;
                    }
                    #endregion
                }
            }
        }

        private void PropertyPanel_MouseEnter(object sender, MouseEventArgs e)
        {
            if (((Diagram.SelectedItems as SelectorViewModel).Nodes as IEnumerable<object>).Count() == 0 &&
              ((Diagram.SelectedItems as SelectorViewModel).Connectors as IEnumerable<object>).Count() == 0)
            {
                propertyPanel.Cursor = Cursors.No;
            }
            else
            {
                propertyPanel.Cursor = Cursors.Arrow;
            }
        }
    }
}
