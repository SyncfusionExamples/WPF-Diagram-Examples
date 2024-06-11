using StencilCategory.ViewModel;
using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Controls;
using Syncfusion.UI.Xaml.Diagram.Stencil;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.SymbolStore;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Xml.Linq;

namespace StencilCategory
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
      
        private string temp;

        public MainWindow()
        {
            InitializeComponent();
            stencil11.Expanded += Stencil11_Expanded1;
            stencil11.MenuItemClicked += Stencil_MenuItemClicked;
        }

        private void Stencil11_Expanded1(object sender, SymbolGroupExpandCollapseEventArgs args)
        {
            temp = args.GroupName.ToString();   
        }

       

        private void Stencil_MenuItemClicked(object sender, MenuItemClickedEventArgs args)
        {

            if (args.Item.Content.ToString() == "Delete")
            {
                for (int i = 0; i < stencil11.SymbolGroups.Count; i++)
                {
                    var v = stencil11.SymbolGroups[i];
                    if ((v as SymbolGroupViewModel).Name == temp)
                    {
                        stencil11.SymbolGroups.RemoveAt(i);
                        break;
                    }
                }
            }
            else
            {
                for (int i = 0; i < stencil11.SymbolGroups.Count; i++)
                {
                    var v = stencil11.SymbolGroups[i];
                    if ((v as SymbolGroupViewModel).Name == temp)
                    {
                        if (args.Item.Content.ToString() == "Move Up")
                        {
                            stencil11.SymbolGroups.Move(i, i - 1);
                            break;
                        }
                        else
                        {
                            stencil11.SymbolGroups.Move(i, i + 1);
                            break;
                        }
                    }
                }
            }
        }



        private void MainWindow_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            
        }    
    }

    public class DiagramMenuItems : ObservableCollection<DiagramMenuItem>
    {
        public DiagramMenuItems() { }
    }

   
}
