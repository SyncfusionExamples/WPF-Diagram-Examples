using Microsoft.Win32;
using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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

namespace CustomCommand
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {        
        public MainWindow()
        {
            InitializeComponent();
            diagram.Loaded += Diagram_Loaded;
           
        }

        private void Diagram_Loaded(object sender, RoutedEventArgs e)
        {
            if (diagram != null)
            {
                
                string MoveRightCommand = "MoveRight";
                var RightcommandToBeRemoved = diagram.CommandManager.Commands.FirstOrDefault(command => command.Name != null && command.Name.Equals(MoveRightCommand));
                if (RightcommandToBeRemoved != null)
                {
                    diagram.CommandManager.Commands.Remove(RightcommandToBeRemoved);
                }


                string MoveLeftCommand = "MoveLeft";
                var LeftcommandToBeRemoved = diagram.CommandManager.Commands.FirstOrDefault(command => command.Name != null && command.Name.Equals(MoveLeftCommand));
                if (LeftcommandToBeRemoved != null)
                {
                    diagram.CommandManager.Commands.Remove(LeftcommandToBeRemoved);
                }

                string MoveUpCommand = "MoveUp";
                var UpcommandToBeRemoved = diagram.CommandManager.Commands.FirstOrDefault(command => command.Name != null && command.Name.Equals(MoveUpCommand));
                if (UpcommandToBeRemoved != null)
                {
                    diagram.CommandManager.Commands.Remove(UpcommandToBeRemoved);
                }


                string MoveDownCommand = "MoveDown";
                var DowncommandToBeRemoved = diagram.CommandManager.Commands.FirstOrDefault(command => command.Name != null && command.Name.Equals(MoveDownCommand));
                if (DowncommandToBeRemoved != null)
                {
                    diagram.CommandManager.Commands.Remove(DowncommandToBeRemoved);
                }
            }
        }
    }
}
