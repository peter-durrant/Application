using System.Linq;
using Hdd.Application;
using NUnit.Framework;

namespace Application.Test
{
    [TestFixture]
    public class MainWindowViewModelTests
    {
        [SetUp]
        public void Init()
        {
            _uut = new MainWindowViewModel();
        }

        private MainWindowViewModel _uut;

        [Test]
        public void GivenTwoModulesAvailable_WhenModulesAreLoaded_ThenTwoModulesAreAvailable()
        {
            Assert.AreEqual(2, _uut.Modules.Count());
        }

        [Test]
        public void GivenTwoModulesAvailable_WhenModulesAreLoaded_ThenMenusAreAvailable()
        {
            var rootMenu = _uut.MainMenuViewModel.Menu.RootMenu;
            var rootMenuItems = rootMenu.Items.ToList();
            Assert.AreEqual(2, rootMenuItems.Count);
            Assert.AreEqual("File", rootMenuItems[0].Name);
            Assert.AreEqual("Help", rootMenuItems[1].Name);
            var fileMenu = rootMenuItems[0].Items.ToList();
            var helpMenu = rootMenuItems[1].Items.ToList();
            Assert.AreEqual("Open", fileMenu[0].Name);
            Assert.AreEqual("Close", fileMenu[1].Name);
            Assert.AreEqual("Print", fileMenu[2].Name);
            Assert.AreEqual("Exit", fileMenu[3].Name);
            Assert.AreEqual("About", helpMenu[0].Name);
            Assert.AreEqual("SendFeedback", helpMenu[1].Name);
            Assert.AreEqual("Feedback", helpMenu[1].Group);
        }
    }
}