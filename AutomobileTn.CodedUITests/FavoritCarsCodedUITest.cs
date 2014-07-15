using System;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Microsoft.VisualStudio.TestTools.UITest.Input;
using Microsoft.VisualStudio.TestTools.UITest.Extension;
using Microsoft.VisualStudio.TestTools.UITesting.WindowsRuntimeControls;
using System.Threading.Tasks;
using System.Linq;
using System.Diagnostics;

namespace AutomobileTn.CodedUITests
{
    /// <summary>
    /// Testing adding and removing cars to and from the favorit list.
    /// </summary>
    [CodedUITest]
    public class FavoritCarsCodedUITest
    {

        private readonly string tileAutomationId;


        public FavoritCarsCodedUITest()
        {
            tileAutomationId = "{C9CD2F01-6067-4EC0-B4AF-A86A9BFA82E2}:App:55039HoussemDellai.AutomobileTn_z6d44j4t15nqw!App";
        }

        [TestMethod]
        [Priority(1)]
        public void AddCarToFavoritTest()
        {
            // Arrange
            XamlListItem alfaRomeoGiuliettaCarListItem = UIMap.UIAutomobileTnWindow.UIMainHubHub.UITousHubSection.UIGroupedCarsListViewList.UIAutomobileTnModelsGrListItem.UIListViewList.AlfaRomeoGiuliettaCaListItem;
            XamlListItem alfaRomeoMitoCarListItem = UIMap.UIAutomobileTnWindow.UIMainHubHub.UITousHubSection.UIGroupedCarsListViewList.UIAutomobileTnModelsGrListItem.UIListViewList.AlfaRomeoMitoCaListItem;
            XamlListItem citroenC4PicassoCarListItem = UIMap.UIAutomobileTnWindow.UIMainHubHub.UITousHubSection.UIGroupedCarsListViewList.UIAutomobileTnModelsGrListItem2.UIListViewList.CitroenC4PicassoCarListItem;

            XamlList uIFavoritItemList = UIMap.UIAutomobileTnWindow.UIMainHubHub.UIFavorisHubSection.UIFavorisText.UIFavoritItemList;
            XamlButton uIConfirmAddButton = UIMap.UIAutomobileTnWindow.UIConfirmAddButton;

            Logger.LogMessage("Test started...");
            
            // Act
            Logger.LogMessage("Will tap on Alpha Romeo");
            Gesture.Tap(alfaRomeoGiuliettaCarListItem);
            Logger.LogMessage("Tapped on Alpha Romeo");
            
            Gesture.Tap(uIConfirmAddButton);
            Logger.LogMessage("Tapped on Add button");

            
            Gesture.Tap(alfaRomeoMitoCarListItem);
            Logger.LogMessage("Tapped on Add Alpha Romeo second car");
            
            Gesture.Tap(uIConfirmAddButton);
            Logger.LogMessage("Tapped on Add button");

            //Gesture.Tap(bMW5SeriesCarListItem);
            //Gesture.Tap(uIConfirmAddButton);

            Gesture.Tap(citroenC4PicassoCarListItem);
            Gesture.Tap(uIConfirmAddButton);

            // Assert
            Assert.IsTrue(uIFavoritItemList.Items.Count >= 2, "2 cars added to the favorit list");
        }

        [TestMethod]
        [Priority(2)]
        public void RemoveAllCarsFromFavoritTest()
        {
            //XamlWindow xamlWindow = XamlWindow.Launch(tileAutomationId);

            // Arrange
            XamlList uIFavoritItemList = UIMap.UIAutomobileTnWindow.UIMainHubHub.UIFavorisHubSection.UIFavorisText.UIFavoritItemList;

            Gesture.Swipe(UIMap.UIAutomobileTnWindow.UIMainHubHub, UITestGestureDirection.Left, 
                Convert.ToUInt32(UIMap.UIAutomobileTnWindow.UIMainHubHub.UITousHubSection.Width + 24));

            // Act
            var uITestControlCollection = uIFavoritItemList.Items.ToList();
            uITestControlCollection.Reverse();
            foreach (var item in uITestControlCollection)
            {
                Gesture.Tap(item);
                Gesture.Tap(UIMap.UIAutomobileTnWindow.UIConfirmRemoveButton);
            }

            // Assert
            Assert.IsTrue(uIFavoritItemList.Items.Count == 0, "There still some remaining cars in the favorit list");
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
