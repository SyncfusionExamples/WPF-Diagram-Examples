using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System;
using System.Diagnostics;
using System.Threading;

namespace DiagramApplicationTest
{
    public class DiagramSession
    {
        private static Process winAppDriverProcess;
        private const string winAppDriverPath = @"C:\Program Files (x86)\Windows Application Driver\WinAppDriver.exe";
        private const string winAppDriverUrl = "http://127.0.0.1:4723";
        private const string diagramAppId = @"C:\Users\DeepaThiruppathy\Downloads\WinAppDriverTesting\WinAppDriverTesting\DiagramApplication\bin\Debug\DiagramApplication.exe";
        private const string deviceName = "WindowsPC";
        private const int waitForAppLaunch = 2;

        protected static WindowsDriver<WindowsElement> DiagramAppSession;
        protected static WindowsDriver<WindowsElement> DesktopSession;

        public static void Setup(TestContext context)
        {
            StartWinAppDriver();

            if (DiagramAppSession == null)
            {
                // Create a new session to launch Diagram application
                var appiumOptions = new AppiumOptions();
                appiumOptions.AddAdditionalCapability("app", diagramAppId);
                appiumOptions.AddAdditionalCapability("deviceName", deviceName);
                appiumOptions.AddAdditionalCapability("ms:waitForAppLaunch", waitForAppLaunch);

                DiagramAppSession = new WindowsDriver<WindowsElement>(new Uri(winAppDriverUrl), appiumOptions);
                Assert.IsNotNull(DiagramAppSession);
                Assert.IsNotNull(DiagramAppSession.SessionId);

                // Set implicit timeout to 1.5 seconds to make element search to retry every 500 ms for at most three times
                DiagramAppSession.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1.5);
            }

            if (DesktopSession == null)
            {
                // Create a desktop session to run test-case on additional window since they are not part of the same visual tree.
                var appiumOptions = new AppiumOptions();
                appiumOptions.AddAdditionalCapability("app", "Root");
                appiumOptions.AddAdditionalCapability("deviceName", deviceName);

                DesktopSession = new WindowsDriver<WindowsElement>(new Uri(winAppDriverUrl), appiumOptions);
            }

            CloseTrailDialog();
        }

        public static void TearDown()
        {
            // Close the application and delete the session
            if (DiagramAppSession != null)
            {
                DiagramAppSession.Close();
                DiagramAppSession.Quit();
                DiagramAppSession = null;
            }

            if (DesktopSession != null)
            {
                DesktopSession.Close();
                DesktopSession.Quit();
                DesktopSession = null;
            }

            StopWinAppDriver();
        }

        private static void StartWinAppDriver()
        {
            if (winAppDriverProcess == null)
            {
                var info = new ProcessStartInfo(winAppDriverPath)
                {
                    UseShellExecute = true,
                    Verb = "runas",
                    WindowStyle = ProcessWindowStyle.Hidden
                };
                winAppDriverProcess = Process.Start(info);
            }
        }

        private static void StopWinAppDriver()
        {
            if (winAppDriverProcess != null)
            {
                foreach (var process in Process.GetProcessesByName("WinAppDriver"))
                {
                    process.Kill();
                }
            }
        }

        private static void CloseTrailDialog()
        {
            try
            {
                // This is just to close syncfusion license window in case of trail version
                var licenseWindow = DesktopSession.FindElementByAccessibilityId("LicenseMessageBox");
                if (licenseWindow != null)
                {
                    DesktopSession.FindElementByAccessibilityId("btn_Close").Click();
                    Thread.Sleep(TimeSpan.FromSeconds(1));
                }
            }
            catch { }
        }
    }
}
