using Microsoft.Win32;
using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.Windows.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace Stencil_And_Diagram_Node_Color_Update
{
    public class ViewModel : DiagramViewModel
    {

        private ICommand _saveCommand;
        private ICommand _LoadCommand;
        private ICommand _newCommand;

        public ICommand SaveCommand
        {
            get { return _saveCommand; }
            set { _saveCommand = value; }
        }

        public ICommand LoadCommand
        {
            get { return _LoadCommand; }
            set { _LoadCommand = value; }
        }

        public ICommand NewCommand
        {
            get { return _newCommand; }
            set { _newCommand = value; }
        }

        public ViewModel() 
        {
            SymbolSource = new SymbolCollection();
            SaveCommand = new DelegateCommand(OnSaved);
            LoadCommand = new DelegateCommand(OnLoad);
            NewCommand = new DelegateCommand(OnNew);
            ItemAddedCommand = new DelegateCommand(OnItemAdded);


            NodeViewModel node = new NodeViewModel()
            {
                OffsetX = 100,
                OffsetY = 100,
                UnitHeight = 100,
                UnitWidth = 100,
                Content = new SolidColorBrush(Colors.Red),
            };

            (SymbolSource as SymbolCollection).Add(node);
        }

        private void OnItemAdded(object obj)
        {
            ItemAddedEventArgs args = (ItemAddedEventArgs)obj;
            if(args.Item is NodeViewModel)
            {
                (args.Item as NodeViewModel).Content = Brush;
            }
        }

        private void OnNew(object obj)
        {
            Brush = new SolidColorBrush(Colors.Red);
            SfDiagram.Nodes = new NodeCollection();
        }

        private void OnLoad(object obj)
        {
            Brush = new SolidColorBrush(Colors.Blue);
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == true)
            {
                using (Stream myStream = dialog.OpenFile())
                {
                    SfDiagram.Load(myStream);
                }
            }
        }

        private void OnSaved(object obj)
        {
            //To Save as stream in file
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Title = "Save XAML";
            dialog.Filter = "XAML File (*.xaml)|*.xaml";
            if (dialog.ShowDialog() == true)
            {
                using (Stream str = File.Open(dialog.FileName, FileMode.CreateNew))
                {
                    SfDiagram.Save(str);
                }
            }
        }

        private SolidColorBrush _brush = new SolidColorBrush(Colors.Red);
        private SymbolCollection _symbols = new SymbolCollection();
        private SfDiagram _sfDiagram;

        public SfDiagram SfDiagram
        {
            get { return _sfDiagram; }
            set { _sfDiagram = value; }
        }

        public SymbolCollection SymbolSource
        {
            get { return _symbols; }
            set { _symbols = value; }
        }

        public SolidColorBrush Brush
        {
            get 
            { 
                return _brush; 
            }
            set 
            { 
                _brush = value; 
                OnBrushChanged(value); 
                OnPropertyChanged(nameof(Brush));
            }
        }

        private void OnBrushChanged(SolidColorBrush value)
        {
            foreach(var symbol in SymbolSource) 
            {
                if(symbol != null && symbol is NodeViewModel)
                {
                    (symbol as NodeViewModel).Content = value;
                }
            }
        }
    }
}
