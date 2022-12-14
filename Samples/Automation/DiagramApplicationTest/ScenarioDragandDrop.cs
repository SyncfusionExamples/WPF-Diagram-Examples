using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Interactions;
using System;
using System.Globalization;
using System.Threading;

namespace DiagramApplicationTest
{
    [TestClass]
    public class ScenarioDragandDrop: DiagramSession
    {
        private static WindowsElement diagram;
        
        // ClassInitialize - called only once and any initialization operations it performs will apply to the entire TestMethod
        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            Setup(context);

            diagram = DiagramAppSession.FindElementByAccessibilityId("diagram");
            Assert.IsNotNull(diagram);
        }

        // ClassCleanup - called once all the TestMethod from the class have been executed
        [ClassCleanup]
        public static void ClassCleanup()
        {
            diagram = null;

            TearDown();
        }

        [TestMethod]
        public void DropNodeFromStencilToDiagram()
        {
            Thread.Sleep(TimeSpan.FromSeconds(.5));

            var actions1 = new Actions(DiagramAppSession);

            var stencil = DiagramAppSession.FindElementByAccessibilityId("stencil");
            Assert.IsNotNull(stencil);

            var rectangleNode =  stencil.FindElement(new ByAccessibilityId("Rectangle"));
            Assert.IsNotNull(rectangleNode);

            // Select a node in stencil
            actions1.MoveToElement(rectangleNode).Click().Perform();
            Thread.Sleep(TimeSpan.FromSeconds(.5));

            // Click and hold the desired node for draging
            actions1.ClickAndHold().MoveByOffset(1, 0).MoveByOffset(0, 1).Perform();
            Thread.Sleep(TimeSpan.FromSeconds(.5));

            actions1.MoveByOffset(diagram.Location.X + 200, diagram.Location.Y + 200).Release().Perform();
            Thread.Sleep(TimeSpan.FromSeconds(2));

            var actions2 = new Actions(DiagramAppSession);

            var ellipse = stencil.FindElement(new ByAccessibilityId("Triangle"));
            Assert.IsNotNull(ellipse);

            // Select a node in stencil
            actions2.MoveToElement(ellipse).Click().Perform();
            Thread.Sleep(TimeSpan.FromSeconds(.5));

            // Click and hold the desired node for draging
            actions2.ClickAndHold().MoveByOffset(1, 0).MoveByOffset(0, 1).Perform();
            Thread.Sleep(TimeSpan.FromSeconds(.5));

            actions2.MoveByOffset(diagram.Location.X + 400, diagram.Location.Y + 400).Release().Perform();
            Thread.Sleep(TimeSpan.FromSeconds(2));
        }
    }
}
