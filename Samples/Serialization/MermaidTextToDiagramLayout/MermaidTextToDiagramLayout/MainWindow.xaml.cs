using Syncfusion.UI.Xaml.Diagram.Layout;
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
using Syncfusion.UI.Xaml.Diagram.Theming;

namespace MermaidTextToDiagramLayoutSupport
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Diagram.LayoutManager = new LayoutManager()
            {
                Layout = new FlowchartLayout()
                {
                    Orientation = FlowchartOrientation.TopToBottom,
                    YesBranchValues = new List<string> { "Yes", "True", "Y", "s" },
                    YesBranchDirection = BranchDirection.LeftInFlow,
                    NoBranchValues = new List<string> { "No", "N", "False", "no" },
                    NoBranchDirection = BranchDirection.RightInFlow,
                    HorizontalSpacing = 60,
                    VerticalSpacing = 40,
                },
            };
        }
        private bool isFlowChartLayout = true;

        private string flowChartMermaid1 = @"graph TD
                               A[Start] --> B{Is the issue resolved?}
                               B -- Yes --> C[Proceed with the next task]
                               B -- No --> D[Investigate the issue further]
                               D --> E[Fix the issue]
                               E --> C
                               C --> F[End]";

        private string flowChartMermaid2 = @"graph TD
                  A([Start]) --> B[Choose Destination]
                  B --> C{Already Registered?}
                  C -->|No| D[Sign Up]
                  D --> E[Enter Details]
                  E --> F[Search Buses]
                  C --> |Yes| F
                  F --> G{Buses Available?}
                  G -->|Yes| H[Select Bus]
                  H --> I[Enter Passenger Details]
                  I --> J[Make Payment]
                  J --> K[Booking Confirmed]
                  G -->|No| L[Set Reminder]
                  K --> M([End])
                  L --> M";

        private string flowChartMermaid3 = @"graph TD
                  A([Start]) --> B[Choose Destination]
                  B --> C{Already Registered?}
                  C -->|No| D[Sign Up]
                  D --> E[Enter Details]
                  E --> F[Search Buses]
                  C --> |Yes| F
                  F --> G{Buses Available?}
                  G -->|Yes| H[Select Bus]
                  H --> I[Enter Passenger Details]
                  I --> J[Make Payment]
                  J --> K[Booking Confirmed]
                  G -->|No| L[Set Reminder]
                  K --> M([End])
                  L --> M
                  style A fill:#90EE90,stroke:#333,stroke-width:2px;
                  style B fill:#4682B4,stroke:#333,stroke-width:2px;
                  style C fill:#32CD32,stroke:#333,stroke-width:2px;
                  style D fill:#FFD700,stroke:#333,stroke-width:2px;
                  style E fill:#4682B4,stroke:#333,stroke-width:2px;
                  style F fill:#4682B4,stroke:#333,stroke-width:2px;
                  style G fill:#32CD32,stroke:#333,stroke-width:2px;
                  style H fill:#4682B4,stroke:#333,stroke-width:2px;
                  style I fill:#4682B4,stroke:#333,stroke-width:2px;
                  style J fill:#4682B4,stroke:#333,stroke-width:2px;
                  style K fill:#FF6347,stroke:#333,stroke-width:2px;
                  style L fill:#FFD700,stroke:#333,stroke-width:2px;
                  style M fill:#FF6347,stroke:#333,stroke-width:2px;";

        private string mindmapMermaid1 = @"
mindmap
root(Mobile Banking Registration)
    User(User)
    PersonalInfo(Personal Information)
        Name(Name)
        DOB(Date of Birth)
        Address(Address)
    ContactInfo))Contact Information((
        Email(Email)
        Phone(Phone Number)
    Account[Account]
        AccountType[Account Type]
            Savings[Savings]
            Checking[Checking]
        AccountDetails(Account Details)
            AccountNumber(Account Number)
            SortCode(Sort Code)
    Security{{Security}}
        Authentication(Authentication)
            Password(Password)
            Biometrics(Biometrics)
            Fingerprint(Fingerprint)
            FaceID(Face ID)
        Verification)Verification(
            OTP)OTP(
            SecurityQuestions)Security Questions(
    Terms(Terms & Conditions)
        AcceptTerms(Accept Terms)
        PrivacyPolicy(Privacy Policy)
                ";

        private string mindmapMermaid2 = @"mindmap
root(Mobile App Development)
    Planning(Planning)
        Requirements(Requirements)
        Feasibility(Feasibility)
    Design(Design)
        UI(UI Design)
        UX(UX Design)
    Development(Development)
        Frontend(Frontend)
        Backend(Backend)
        Database(Database)
    Testing(Testing)
        Unit(Unit Testing)
        Integration(Integration Testing)
        System(System Testing)
        UserAcceptance(User Acceptance Testing)
    Deployment(Deployment)
        AppStore(App Store)
        PlayStore(Play Store)
    Maintenance(Maintenance)
        BugFixes(Bug Fixes)
        Updates(Updates)
";

        private string mindmapMermaid3 = @"mindmap
root(Syncfusion Products)
    UIComponents(UI Components)
        Grids(Grids)
        Charts(Charts)
        Diagrams(Diagrams)
        Editors(Editors)
    DataVisualization(Data Visualization)
        Dashboards(Dashboards)
        Reporting(Reporting)
        BI(Business Intelligence)
    FileFormats(File Formats)
        PDF(PDF)
        Excel(Excel)
        Word(Word)
        PowerPoint(PowerPoint)
    DevelopmentTools(Development Tools)
        CodeEditors(Code Editors)
        Debuggers(Debuggers)
        Profilers(Profilers)
    Integration(Integration)
        Cloud(Cloud)
        OnPremises(On-Premises)
";
        private void SaveToMermaid_Click(object sender, RoutedEventArgs e)
        {
            string mermaidData = Diagram.SaveDiagramAsMermaid();
            if (!string.IsNullOrEmpty(mermaidData))
                DisplayMermaid.Text = mermaidData;

        }

        private void LoadFromMermaid_Click(object sender, RoutedEventArgs e)
        {
            Diagram.Theme = null;



            if (!string.IsNullOrEmpty(EnterMermaid.Text))
            {
                if (!isFlowChartLayout)
                {
                    Diagram.LayoutManager = new LayoutManager()
                    {
                        Layout = new MindMapTreeLayout()
                        {
                            HorizontalSpacing = 50,
                            VerticalSpacing = 30,
                            Orientation = Orientation.Horizontal,
                            SplitMode = MindMapTreeMode.Level
                        },
                        RefreshFrequency = RefreshFrequency.ArrangeParsing
                    };
                    Diagram.Theme = new OfficeTheme();
                }
                Diagram.LoadDiagramFromMermaid(EnterMermaid.Text);
            }
            else
            {
                //List<string> filteredData = mindmapMermaid
                //                            .Split(new[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries)
                //                            .Where(line => !string.IsNullOrWhiteSpace(line))
                //                            .ToList();
                //if (filteredData[0].Trim() != "mindmap")
                //    filteredData.Insert(0, "mindmap");
                //filteredData[1].TrimStart('-', '+');
                //mindmapMermaid = string.Join("\n", filteredData);
                if (isFlowChartLayout)
                {
                    Diagram.LoadDiagramFromMermaid(flowChartMermaid3);
                }
                else
                {
                    Diagram.LoadDiagramFromMermaid(mindmapMermaid2);
                    Diagram.LayoutManager = new LayoutManager()
                    {
                        Layout = new MindMapTreeLayout()
                        {
                            HorizontalSpacing = 50,
                            VerticalSpacing = 30,
                            Orientation = Orientation.Horizontal,
                            SplitMode = MindMapTreeMode.Level
                        },
                        RefreshFrequency = RefreshFrequency.ArrangeParsing
                    };
                    Diagram.Theme = new OfficeTheme();
                }
            }
        }

        private void DiagramLayout_Checked(object sender, RoutedEventArgs e)
        {
            Diagram.LayoutManager = new LayoutManager()
            {
                Layout = new MindMapTreeLayout()
                {
                    HorizontalSpacing = 50,
                    VerticalSpacing = 30,
                    Orientation = Orientation.Horizontal,
                    SplitMode = MindMapTreeMode.Level
                },
                RefreshFrequency = RefreshFrequency.ArrangeParsing
            };
            isFlowChartLayout = false;
        }

        private void DiagramLayout_Unchecked(object sender, RoutedEventArgs e)
        {
            Diagram.LayoutManager = new LayoutManager()
            {
                Layout = new FlowchartLayout()
                {
                    Orientation = FlowchartOrientation.TopToBottom,
                    YesBranchValues = new List<string> { "Yes", "True", "Y", "s" },
                    YesBranchDirection = BranchDirection.LeftInFlow,
                    NoBranchValues = new List<string> { "No", "N", "False", "no" },
                    NoBranchDirection = BranchDirection.RightInFlow,
                    HorizontalSpacing = 60,
                    VerticalSpacing = 40,
                },
            };
            isFlowChartLayout = true;
        }

        private void MermaidExamples_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Diagram.Theme = null;
            switch (MermaidExamples.SelectedIndex)
            {
                case 0:
                    {
                        if (isFlowChartLayout)
                            Diagram.LoadDiagramFromMermaid(flowChartMermaid1);
                        else
                        {
                            Diagram.LoadDiagramFromMermaid(mindmapMermaid1);
                            Diagram.LayoutManager = new LayoutManager()
                            {
                                Layout = new MindMapTreeLayout()
                                {
                                    HorizontalSpacing = 50,
                                    VerticalSpacing = 30,
                                    Orientation = Orientation.Horizontal,
                                    SplitMode = MindMapTreeMode.Level
                                },
                                RefreshFrequency = RefreshFrequency.ArrangeParsing
                            };
                            Diagram.Theme = new OfficeTheme();
                        } 
                        break;
                    }
                case 1:
                    {
                        if (isFlowChartLayout)
                            Diagram.LoadDiagramFromMermaid(flowChartMermaid2);
                        else
                        {
                            Diagram.LoadDiagramFromMermaid(mindmapMermaid2);
                            Diagram.LayoutManager = new LayoutManager()
                            {
                                Layout = new MindMapTreeLayout()
                                {
                                    HorizontalSpacing = 50,
                                    VerticalSpacing = 30,
                                    Orientation = Orientation.Horizontal,
                                    SplitMode = MindMapTreeMode.Level
                                },
                                RefreshFrequency = RefreshFrequency.ArrangeParsing
                            };
                            Diagram.Theme = new OfficeTheme();
                        } 
                        break;
                    }
                case 2:
                    {
                        if (isFlowChartLayout)
                            Diagram.LoadDiagramFromMermaid(flowChartMermaid3);
                        else
                        {
                            Diagram.LoadDiagramFromMermaid(mindmapMermaid3);
                            Diagram.LayoutManager = new LayoutManager()
                            {
                                Layout = new MindMapTreeLayout()
                                {
                                    HorizontalSpacing = 50,
                                    VerticalSpacing = 30,
                                    Orientation = Orientation.Horizontal,
                                    SplitMode = MindMapTreeMode.Level
                                },
                                RefreshFrequency = RefreshFrequency.ArrangeParsing
                            };
                            Diagram.Theme = new OfficeTheme();
                        } 
                        break;
                    }
            }
        }
    }
}
