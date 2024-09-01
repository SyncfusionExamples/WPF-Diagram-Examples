using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SyncFusionDiagramTest
{
    /// <summary>
    /// App.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            //Register Syncfusion license
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mjc3NzE4NkAzMjMzMmUzMDJlMzBDcFpzenU0OUJpTCt2cjAvdCt1cGYrc1dPd0NhdmNsSHVhcUZ4aWJrSTNVPQ==");
        }
    }
}
