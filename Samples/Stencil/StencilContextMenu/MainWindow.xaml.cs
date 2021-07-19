using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Controls;
using Syncfusion.UI.Xaml.Diagram.Stencil;
using Syncfusion.Windows.Shared;
using Syncfusion.Windows.Tools.Controls;
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

namespace SimCentral
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private ICommand editcommand;

        private ICommand renamecommand;

        public ICommand EditCommand
        {
            get { return editcommand; }
            set { editcommand = value; }
        }

        public ICommand RenameCommand
        {
            get { return renamecommand; }
            set { renamecommand = value; }
        }

        public MainWindow()
        {
            InitializeComponent();

            CreateTabs(tabcontrol);

            EditCommand = new DelegateCommand(OnEdit);

            RenameCommand = new DelegateCommand(OnRename);

        }

        private void OnRename(object obj)
        {
            
        }

        private void OnEdit(object obj)
        {
            
        }

        private void CreateTabs(TabControl tabcontrol)
        {
            TabItemExt BasicShapesTab = new TabItemExt()
            {
                Header = "Basic Shapes",
                Background = new SolidColorBrush(Colors.Blue),                
            };

            Stencil BasicStencil = new Stencil() { ShowDisplayModeToggleButton = false, ShowSearchTextBox = false, ExpandMode = ExpandMode.All, Constraints = StencilConstraints.Default | StencilConstraints.AllowDragDrop };

            BasicStencil.SymbolGroups = new SymbolGroups()
            {
                new SymbolGroupProvider()
                {
                    MappingName = "Key",
                },
            };

            BasicStencil.Menu = new DiagramMenu()
            {
                MenuItems = new ObservableCollection<DiagramMenuItem>()
                {
                    new DiagramMenuItem()
                    {
                        Content = "Cut",
                        Command = StencilCommands.Cut,
                        Icon = @"pack://application:,,,/Icons/Cut.png",
                    },

                    new DiagramMenuItem()
                    {
                        Content = "Copy",
                        Command = StencilCommands.Copy,
                        Icon = @"pack://application:,,,/Icons/Copy.png"
                    },

                    new DiagramMenuItem()
                    {
                        Content = "Paste",
                        Command = StencilCommands.Paste,
                        CommandParameter = this,
                        Icon = @"pack://application:,,,/Icons/Paste.png"
                    },

                    
                    new DiagramMenuItem()
                            {
                                Command = RenameCommand,
                                Content = "Rename",
                                 Icon = @"pack://application:,,,/Icons/Rename.png",
                            },
                },
            };

            BasicStencil.SymbolSource = new SymbolCollection()
            {
                new NodeViewModel()
                {
                    UnitWidth = 100,
                    UnitHeight = 100,
                    Shape = this.Resources["Ellipse"],
                    Key = "Basic Shapes",
                    Menu = new DiagramMenu()
                    {
                        MenuItems = new ObservableCollection<DiagramMenuItem>()
                        {
                            new DiagramMenuItem()
                            {
                                Content = "Delete",
                                Command = StencilCommands.Delete,
                                CommandParameter = this,
                                Icon = @"pack://application:,,,/Icons/Delete.png",
                            },
                        },
                    },
                },
                      
                new NodeViewModel()
                {
                    UnitWidth = 100,
                    UnitHeight = 100,
                    Shape = this.Resources["Rectangle"],
                    Key = "Basic Shapes",
                },

                new NodeViewModel()
                {
                    UnitWidth = 100,
                    UnitHeight = 100,
                    Shape = this.Resources["Plus"],
                    Key = "Basic Shapes",
                },


                new NodeViewModel()
                {
                    UnitWidth = 100,
                    UnitHeight = 100,
                    Shape = this.Resources["Triangle"],
                    Key = "Basic Shapes",
                },


                new NodeViewModel()
                {
                    UnitWidth = 100,
                    UnitHeight = 100,
                    Shape = this.Resources["Pentagon"],
                    Key = "Basic Shapes",
                },


                new NodeViewModel()
                {
                    UnitWidth = 100,
                    UnitHeight = 100,
                    Shape = this.Resources["Hexagon"],
                    Key = "Basic Shapes",
                },


                new NodeViewModel()
                {
                    UnitWidth = 100,
                    UnitHeight = 100,
                    Shape = this.Resources["Frame"],
                    Key = "Basic Shapes",
                },


                new NodeViewModel()
                {
                    UnitWidth = 100,
                    UnitHeight = 100,
                    Shape = this.Resources["Donut"],
                    Key = "Basic Shapes",
                },


                new NodeViewModel()
                {
                    UnitWidth = 100,
                    UnitHeight = 100,
                    Shape = this.Resources["Cube"],
                    Key = "Basic Shapes",
                },
            };

            BasicShapesTab.Content = BasicStencil;
            tabcontrol.Items.Add(BasicShapesTab);
        }

        private bool Filter(SymbolFilterProvider sender, object symbol)
        {
            if (symbol is NodeViewModel)
            {
                if (sender.Content.ToString() == (symbol as NodeViewModel).Key.ToString())
                    return true;
            }

            if (sender.Content.ToString() == "All")
            {
                return true;
            }
            return false;
        }
    }
}
