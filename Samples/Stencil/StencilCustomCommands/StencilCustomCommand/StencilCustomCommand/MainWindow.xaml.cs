using StencilCustomCommand.Utility;
using Syncfusion.UI.Xaml.Diagram;
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

namespace StencilCustomCommand
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            AddCommand();
            RemoveCommand();
           
        }

        ICommand CustomCommand;

        private void AddCommand()
        {
            // Adding a custom command to the stencil CommandManager
            CustomCommand = new Command(OnCustomCommand);
            GestureCommand customCommmandGesture = new GestureCommand()
            {
                Command = CustomCommand,
                Gesture = new Gesture
                {
                    Key = Key.K,
                    KeyModifiers = ModifierKeys.Control,
                    KeyState = KeyStates.Down
                },
                Name = "Custom",
            };
            Stencil.CommandManager.Commands.Add(customCommmandGesture);
        }
        private void OnCustomCommand(object obj)
        {
            // Perform Operations
            MessageBox.Show("Custom Command Executed Successfully!");
        }

        private void RemoveCommand()
        {
            // Removing a particular stencil command from the CommandManager
            string commandName = "SelectAll";
            IGestureCommand commandToBeRemoved = Stencil.CommandManager.Commands.FirstOrDefault(command => command.Name.Equals(commandName));
            Stencil.CommandManager.Commands.Remove(commandToBeRemoved);
        }

    }
}
