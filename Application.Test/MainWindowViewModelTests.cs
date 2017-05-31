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
            Assert.AreEqual(1, _uut.MainMenuViewModel.MenuGroups.Groups.Count);
            var menuGroups = _uut.MainMenuViewModel.MenuGroups.Groups.First();
            Assert.AreEqual("File", menuGroups.Value.Name);
            Assert.AreEqual(int.MinValue, menuGroups.Key.Item1);
            Assert.AreEqual("File", menuGroups.Key.Item2);
            Assert.AreEqual("File", menuGroups.Key.Item2);
            Assert.AreEqual(4, menuGroups.Value.ChildItems.Count);
            var menuItems = menuGroups.Value.ChildItems.ToList();
            Assert.AreEqual("Open", menuItems[0].Name);
            Assert.AreEqual("Close", menuItems[1].Name);
            Assert.AreEqual("Print", menuItems[2].Name);
            Assert.AreEqual("Exit", menuItems[3].Name);
        }
    }
}