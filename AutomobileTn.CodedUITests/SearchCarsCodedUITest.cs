using System;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Microsoft.VisualStudio.TestTools.UITest.Input;
using Microsoft.VisualStudio.TestTools.UITest.Extension;
using Microsoft.VisualStudio.TestTools.UITesting.WindowsRuntimeControls;


namespace AutomobileTn.CodedUITests
{
    /// <summary>
    /// Summary description for CodedUITest1
    /// </summary>
    [CodedUITest]
    public class SearchCarsCodedUITest
    {
        public SearchCarsCodedUITest()
        {
        }

        [TestMethod]
        public void SearchCarsByPriceCodedUITest()
        {
            // Arrange
            Gesture.Tap(UIMap.UIItemWindow.SearchAppBarButton);
            Gesture.Tap(UIMap.UIAutomobileTnWindow.UIMainHubHub.UIRechercheHubSection.SearchByPriceHyperlink);

            // Act
            UIMap.UIAutomobileTnWindow.UIMainHubHub.UIRechercheHubSection.SearchPriceSlider.Position = 30000;

            // Assert
            Assert.IsTrue(UIMap.UIAutomobileTnWindow.UIMainHubHub.UIRechercheHubSection.SearchResultList.Items.Count > 0, "No car found with the matching price.");
        }

        [TestMethod]
        public void SearchCarsByNameCodedUITest()
        {
            // Arrange
            Gesture.Tap(UIMap.UIItemWindow.SearchAppBarButton);
            Gesture.Tap(UIMap.UIAutomobileTnWindow.UIMainHubHub.UIRechercheHubSection.SearchModelHyperlink);

            // Act
            UIMap.UIAutomobileTnWindow.UIMainHubHub.UIRechercheHubSection.UIAutoSuggestBoxGroup.ModelTextBoxEdit.Text = "Mercedes";

            // Assert
            Assert.IsTrue(UIMap.UIAutomobileTnWindow.UIMainHubHub.UIRechercheHubSection.SearchResultList.Items.Count > 0, "No car found with the matching price.");
        }

        public UIMap UIMap
        {
            get
            {
                if ((this.map == null))
                {
                    this.map = new UIMap();
                }

                return this.map;
            }
        }

        private UIMap map;
    }
}
