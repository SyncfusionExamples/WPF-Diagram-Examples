﻿using Syncfusion.UI.Xaml.Diagram;
using System;
using System.Collections.Generic;
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

namespace NodeCreation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //Create instance of SfDiagram
            SelectorViewModel svm = (diagram.SelectedItems as SelectorViewModel);
            //Removing draw command from QuickCommandCollection using index. Here Draw command index is 1.
            (svm.Commands as QuickCommandCollection).RemoveAt(1);
        }
    }
}