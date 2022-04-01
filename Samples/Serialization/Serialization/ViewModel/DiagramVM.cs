using Microsoft.Win32;
using Serialization_WPF.Utility;
using Syncfusion.UI.Xaml.Diagram;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Serialization_WPF.ViewModel
{
    public class DiagramVM : DiagramViewModel
    {
        private ICommand _SaveCommand;
        private ICommand _LoadCommand;

        public ICommand SaveCommand
        {
            get { return _SaveCommand; }
            set { _SaveCommand = value; }
        }

        public ICommand LoadCommand
        {
            get { return _LoadCommand; }
            set { _LoadCommand = value; }
        }

        public DiagramVM()
        {
            // Initialize ItemAdded command to add the content for loaded node
            ItemAddedCommand = new Command(OnItemAdded);

            LoadCommand = new Command(OnLoad);
            SaveCommand = new Command(OnSave);

            // Need to add new class which are not derived from our ViewModel classess
            // Need to add new types

            KnownTypes = () => new List<Type>()
            {
                typeof(NodeContent)
            };

            CustomNode node1 = new CustomNode()
            {
                OffsetX = 150,
                OffsetY = 150,
                UnitHeight = 100,
                UnitWidth = 100,
                CustomContent = new NodeContent() { Content = "Node1" },
                Content = new NodeContent() { Content = "Node1" },
                CustomContentTemplate = "NodeTemplate",
                ContentTemplate = App.Current.Resources["NodeTemplate"] as DataTemplate,
            };

            (Nodes as NodeCollection).Add(node1);


            CustomNode node2 = new CustomNode()
            {
                OffsetX = 300,
                OffsetY = 300,
                UnitHeight = 100,
                UnitWidth = 100,
                CustomContent = new NodeContent() { Content = "Node2" },
                Content = new NodeContent() { Content = "Node2" },
                CustomContentTemplate = "NodeTemplate",
                ContentTemplate = App.Current.Resources["NodeTemplate"] as DataTemplate,
            };

            (Nodes as NodeCollection).Add(node2);

            ConnectorViewModel Con = new ConnectorViewModel()
            {
                SourceNode = node1,
                TargetNode = node2,
            };

            (Connectors as ConnectorCollection).Add(Con);

        }

        private void OnSave(object obj)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Title = "Save XAML";
            dialog.Filter = "XAML File (*.xaml)|*.xaml";
            if (dialog.ShowDialog() == true)
            {
                using (Stream s = File.Open(dialog.FileName, FileMode.OpenOrCreate))
                {
                    (Info as IGraphInfo).Save(s);
                }
            }
        }

        private void OnLoad(object obj)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == true)
            {
                using (Stream myStream = dialog.OpenFile())
                {
                    (Info as IGraphInfo).Load(myStream);
                }
            }
        }

        private void OnItemAdded(object obj)
        {
            var args = obj as ItemAddedEventArgs;
            if (args.Item is CustomNode && args.ItemSource == ItemSource.Load)
            {
                CustomNode node = args.Item as CustomNode;
                node.Content = node.CustomContent;
                node.ContentTemplate = App.Current.Resources[node.CustomContentTemplate] as DataTemplate;
            }
        }
    }
}
