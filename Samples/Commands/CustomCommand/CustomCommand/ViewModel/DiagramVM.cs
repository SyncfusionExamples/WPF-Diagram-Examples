using Microsoft.Win32;
using Syncfusion.UI.Xaml.Diagram;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CustomCommand.ViewModel
{
    public class DiagramVM : DiagramViewModel
    {
        // Custom Command for save the diagram
        public ICommand Save { get; set; }
        public DiagramVM()
        {
            CommandManager = new Syncfusion.UI.Xaml.Diagram.CommandManager();

            (CommandManager as Syncfusion.UI.Xaml.Diagram.CommandManager).Commands = new CommandCollection();

            // Initialize the Custom command property.
            Save = new Command(OnSaveCommand);

            //To define the mouse and keyboard gesture for the commands

            GestureCommand saveGesture = new GestureCommand()
            {
                // Define the command with custom command
                Command = Save,
                // Define gesture for custom Command
                Gesture = new Gesture
                {
                    KeyModifiers = ModifierKeys.Control,
                    KeyState = KeyStates.Down,
                    Key = System.Windows.Input.Key.S
                },
                // Parameter for command - file name for save command
                Parameter = "diagram"
            };

            // Add the custom command to the existsing command collection.
            CommandManager.Commands.Add(saveGesture);
        }

        // Action for save the Command
        private void OnSaveCommand(object obj)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            Object parameter = (obj as IGestureParameter).Parameter;
            dialog.FileName = parameter.ToString();
            dialog.Filter = "XAML File (*.xaml)|*.xaml";
            if (dialog.ShowDialog() == true)
            {
                using (Stream s = File.Open(dialog.FileName, FileMode.CreateNew))
                {
                    (Info as IGraphInfo).Save(s);
                }
            }
        }
    }
}
