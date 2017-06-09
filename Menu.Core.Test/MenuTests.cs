using System;
using NSubstitute;
using NUnit.Framework;

namespace Menu.Core.Test
{
    [TestFixture]
    public class MenuTests
    {
        private readonly IMenuCommand _menuCommand = Substitute.For<IMenuCommand>();

        [Test]
        public void GivenANewMenu_WhenWrittenOut_ThenWritesCorrectMenu()
        {
            var menu = new Menu();

            Assert.AreEqual("", menu.ToString());
            Console.WriteLine(menu);
        }

        [Test]
        public void GivenMenuWithTopLevelItems_WhenWrittenOut_ThenWritesCorrectMenu()
        {
            var menu = new Menu();

            menu.AddOrGetMenuItem(new[] {"File"}, _menuCommand);
            menu.AddOrGetMenuItem(new[] {"Edit"}, _menuCommand);
            menu.AddOrGetMenuItem(new[] {"View"}, _menuCommand);
            menu.AddOrGetMenuItem(new[] {"Help"}, _menuCommand);

            Assert.AreEqual("File\nEdit\nView\nHelp\n", menu.ToString());
            Console.WriteLine(menu);
        }

        [Test]
        public void GivenMenuWithTopLevelItems_WhenAdded_ThenReturnsCorrectMenuItem()
        {
            var menu = new Menu();

            var fileItem = menu.AddOrGetMenuItem(new[] {"File"}, _menuCommand);
            var editItem = menu.AddOrGetMenuItem(new[] {"Edit"}, _menuCommand);
            var viewItem = menu.AddOrGetMenuItem(new[] {"View"}, _menuCommand);
            var helpItem = menu.AddOrGetMenuItem(new[] {"Help"}, _menuCommand);

            Assert.AreEqual("File", fileItem.Name);
            Assert.AreEqual("Edit", editItem.Name);
            Assert.AreEqual("View", viewItem.Name);
            Assert.AreEqual("Help", helpItem.Name);
        }

        [Test]
        public void GivenMenu_WhenAddingItemsByPath_ThenGeneratesCorrectMenu()
        {
            var menu = new Menu();

            var openItem = menu.AddOrGetMenuItem(new[] {"File", "Open"}, _menuCommand);
            var copyItem = menu.AddOrGetMenuItem(new[] {"Edit", "Copy"}, _menuCommand);
            var viewItem = menu.AddOrGetMenuItem(new[] {"View"}, _menuCommand);
            var aboutItem = menu.AddOrGetMenuItem(new[] {"Help", "About"}, _menuCommand);

            Console.WriteLine(menu);

            Assert.AreEqual("Open", openItem.Name);
            Assert.AreEqual("Copy", copyItem.Name);
            Assert.AreEqual("View", viewItem.Name);
            Assert.AreEqual("About", aboutItem.Name);

            Assert.AreEqual("File\n\tOpen\nEdit\n\tCopy\nView\nHelp\n\tAbout\n", menu.ToString());
        }

        [Test]
        public void GivenMenu_WhenAddingItemsInGroup_ThenGeneratesCorrectMenu()
        {
            var menu = new Menu();

            var openItem = menu.AddOrGetMenuItem(new[] {new MenuGroupItem("File"), new MenuGroupItem("Open", "FileNew")}, _menuCommand);
            var gotoItem = menu.AddOrGetMenuItem(new[] {new MenuGroupItem("Edit"), new MenuGroupItem("Goto", "EditGoto")}, _menuCommand);
            var cutItem = menu.AddOrGetMenuItem(new[] {new MenuGroupItem("Edit"), new MenuGroupItem("Cut", "EditCopyAndPaste")}, _menuCommand);
            var copyItem = menu.AddOrGetMenuItem(new[] {new MenuGroupItem("Edit"), new MenuGroupItem("Copy", "EditCopyAndPaste")}, _menuCommand);
            var pasteItem = menu.AddOrGetMenuItem(new[] {new MenuGroupItem("Edit"), new MenuGroupItem("Paste", "EditCopyAndPaste")}, _menuCommand);
            var viewItem = menu.AddOrGetMenuItem(new[] {"View"}, _menuCommand);
            var aboutItem = menu.AddOrGetMenuItem(new[] {"Help", "About"}, _menuCommand);
            var feedbackItem = menu.AddOrGetMenuItem(new[] {new MenuGroupItem("Help"), new MenuGroupItem("Send Feedback", "HelpFeedback")}, _menuCommand);
            var samplesItem = menu.AddOrGetMenuItem(new[] {new MenuGroupItem("Help"), new MenuGroupItem("Samples", "HelpSamples")}, _menuCommand);
            var reportAProblemItem = Menu.AddOrGetMenuItem(feedbackItem, new MenuGroupItem("Report a Problem...", "HelpFeedbackReportAProblem"), _menuCommand);

            Console.WriteLine(menu);

            Assert.AreEqual("Open", openItem.Name);
            Assert.AreEqual("Goto", gotoItem.Name);
            Assert.AreEqual("Cut", cutItem.Name);
            Assert.AreEqual("Copy", copyItem.Name);
            Assert.AreEqual("Paste", pasteItem.Name);
            Assert.AreEqual("View", viewItem.Name);
            Assert.AreEqual("About", aboutItem.Name);
            Assert.AreEqual("Send Feedback", feedbackItem.Name);
            Assert.AreEqual("Samples", samplesItem.Name);
            Assert.AreEqual("Report a Problem...", reportAProblemItem.Name);

            Assert.AreEqual("File\n\tOpen\nEdit\n\tGoto\n\t----------\n\tCut\n\tCopy\n\tPaste\nView\nHelp\n\tAbout\n\t----------\n\tSend Feedback\n\t\tReport a Problem...\n\t----------\n\tSamples\n",
                menu.ToString());
        }

        [Test]
        public void GivenMenu_WhenAddingItemsWithPrecedenceOnItems_ThenGeneratesCorrectMenu()
        {
            var menu = new Menu();

            var openItem = menu.AddOrGetMenuItem(new[] {new MenuGroupItem("File"), new MenuGroupItem("Open", 1, "FileNew")}, _menuCommand);
            var gotoItem = menu.AddOrGetMenuItem(new[] {new MenuGroupItem("Edit"), new MenuGroupItem("Goto", 5, "EditGoto")}, _menuCommand);
            var copyItem = menu.AddOrGetMenuItem(new[] {new MenuGroupItem("Edit"), new MenuGroupItem("Copy", 2, "EditCopyAndPaste")}, _menuCommand);
            var cutItem = menu.AddOrGetMenuItem(new[] {new MenuGroupItem("Edit"), new MenuGroupItem("Cut", 1, "EditCopyAndPaste")}, _menuCommand);
            var pasteItem = menu.AddOrGetMenuItem(new[] {new MenuGroupItem("Edit"), new MenuGroupItem("Paste", 3, "EditCopyAndPaste")}, _menuCommand);
            var viewItem = menu.AddOrGetMenuItem(new[] {"View"}, _menuCommand);
            var aboutItem = menu.AddOrGetMenuItem(new[] {new MenuGroupItem("Help", 20), new MenuGroupItem("About")}, _menuCommand);
            var feedbackItem = menu.AddOrGetMenuItem(new[] {new MenuGroupItem("Help", 20), new MenuGroupItem("Send Feedback", "HelpFeedback")}, _menuCommand);
            var samplesItem = menu.AddOrGetMenuItem(new[] {new MenuGroupItem("Help", 20), new MenuGroupItem("Samples", "HelpSamples")}, _menuCommand);
            var reportAProblemItem = Menu.AddOrGetMenuItem(feedbackItem, new MenuGroupItem("Report a Problem...", "HelpFeedbackReportAProblem"), _menuCommand);

            Console.WriteLine(menu);

            Assert.AreEqual("Open", openItem.Name);
            Assert.AreEqual("Goto", gotoItem.Name);
            Assert.AreEqual("Cut", cutItem.Name);
            Assert.AreEqual("Copy", copyItem.Name);
            Assert.AreEqual("Paste", pasteItem.Name);
            Assert.AreEqual("View", viewItem.Name);
            Assert.AreEqual("About", aboutItem.Name);
            Assert.AreEqual("Send Feedback", feedbackItem.Name);
            Assert.AreEqual("Samples", samplesItem.Name);
            Assert.AreEqual("Report a Problem...", reportAProblemItem.Name);

            Assert.AreEqual("File\n\tOpen\nEdit\n\tGoto\n\t----------\n\tCut\n\tCopy\n\tPaste\nView\nHelp\n\tAbout\n\t----------\n\tSend Feedback\n\t\tReport a Problem...\n\t----------\n\tSamples\n",
                menu.ToString());
        }

        [Test]
        public void GivenMenu_WhenAddingItemsWithPrecedenceOnItemsAndGroups_ThenGeneratesCorrectMenu()
        {
            var menu = new Menu();

            var openItem = menu.AddOrGetMenuItem(new[] {new MenuGroupItem("File", 3), new MenuGroupItem("Open", 1, "FileNew")}, _menuCommand);
            var gotoItem = menu.AddOrGetMenuItem(new[] {new MenuGroupItem("Edit"), new MenuGroupItem("Goto", 5, "EditGoto", 5)}, _menuCommand);
            var copyItem = menu.AddOrGetMenuItem(new[] {new MenuGroupItem("Edit"), new MenuGroupItem("Copy", 2, "EditCopyAndPaste", 1)}, _menuCommand);
            var cutItem = menu.AddOrGetMenuItem(new[] {new MenuGroupItem("Edit"), new MenuGroupItem("Cut", 1, "EditCopyAndPaste", 1)}, _menuCommand);
            var pasteItem = menu.AddOrGetMenuItem(new[] {new MenuGroupItem("Edit"), new MenuGroupItem("Paste", 3, "EditCopyAndPaste", 1)}, _menuCommand);
            var viewItem = menu.AddOrGetMenuItem(new[] {"View"}, _menuCommand);
            var aboutItem = menu.AddOrGetMenuItem(new[] {new MenuGroupItem("Help", 20), new MenuGroupItem("About")}, _menuCommand);
            var feedbackItem = menu.AddOrGetMenuItem(new[] {new MenuGroupItem("Help", 20), new MenuGroupItem("Send Feedback", "HelpFeedback")}, _menuCommand);
            var samplesItem = menu.AddOrGetMenuItem(new[] {new MenuGroupItem("Help", 20), new MenuGroupItem("Samples", "HelpSamples")}, _menuCommand);
            var reportAProblemItem = Menu.AddOrGetMenuItem(feedbackItem, new MenuGroupItem("Report a Problem...", "HelpFeedbackReportAProblem"), _menuCommand);

            Console.WriteLine(menu);

            Assert.AreEqual("Open", openItem.Name);
            Assert.AreEqual("Goto", gotoItem.Name);
            Assert.AreEqual("Cut", cutItem.Name);
            Assert.AreEqual("Copy", copyItem.Name);
            Assert.AreEqual("Paste", pasteItem.Name);
            Assert.AreEqual("View", viewItem.Name);
            Assert.AreEqual("About", aboutItem.Name);
            Assert.AreEqual("Send Feedback", feedbackItem.Name);
            Assert.AreEqual("Samples", samplesItem.Name);
            Assert.AreEqual("Report a Problem...", reportAProblemItem.Name);

            Assert.AreEqual("Edit\n\tCut\n\tCopy\n\tPaste\n\t----------\n\tGoto\nView\nFile\n\tOpen\nHelp\n\tAbout\n\t----------\n\tSend Feedback\n\t\tReport a Problem...\n\t----------\n\tSamples\n",
                menu.ToString());
        }

        [Test]
        public void GivenMenu_WhenAddingDuplicateItems_ThenMenuOnlyHasSingleItem()
        {
            var menu = new Menu();

            menu.AddOrGetMenuItem(new[] {new MenuGroupItem("File"), new MenuGroupItem("Open")}, _menuCommand);
            menu.AddOrGetMenuItem(new[] {new MenuGroupItem("File"), new MenuGroupItem("Open")}, _menuCommand);

            Console.WriteLine(menu);

            Assert.AreEqual("File\n\tOpen\n", menu.ToString());
        }

        [Test]
        public void GivenMenu_WhenAddingItemsWithDifferentPrecedence_ThenOrdersItemsByPrecedence()
        {
            var menu = new Menu();

            menu.AddOrGetMenuItem(new[] {new MenuGroupItem("File"), new MenuGroupItem("Open", 2)}, _menuCommand);
            menu.AddOrGetMenuItem(new[] {new MenuGroupItem("File"), new MenuGroupItem("Close", 1)}, _menuCommand);

            Console.WriteLine(menu);

            Assert.AreEqual("File\n\tClose\n\tOpen\n", menu.ToString());
        }

        [Test]
        public void GivenMenu_WhenAddingItemsWithEmptyStringPath_ThenThrowsException()
        {
            var menu = new Menu();

            Assert.Throws(typeof(InvalidOperationException), () => menu.AddOrGetMenuItem(new string[] {}, _menuCommand));
        }

        [Test]
        public void GivenMenu_WhenAddingItemsWithEmptyMenuItemPath_ThenThrowsException()
        {
            var menu = new Menu();

            Assert.Throws(typeof(InvalidOperationException), () => menu.AddOrGetMenuItem(new MenuGroupItem[] {}, _menuCommand));
        }

        [Test]
        public void GivenMenu_WhenAddingItemsWithDifferentGroupPrecedence_ThenThrowsException()
        {
            var menu = new Menu();
            menu.AddOrGetMenuItem(new[] {new MenuGroupItem("File", 3), new MenuGroupItem("Open")}, _menuCommand);

            Assert.Throws(typeof(InvalidOperationException), () => menu.AddOrGetMenuItem(new[] {new MenuGroupItem("File"), new MenuGroupItem("Close")}, _menuCommand));
        }

        [Test]
        public void GivenMenu_WhenAddingItemsWithDifferentGroup_ThenThrowsException()
        {
            var menu = new Menu();
            menu.AddOrGetMenuItem(new[] {new MenuGroupItem("File"), new MenuGroupItem("Open", "OpenGroup")}, _menuCommand);

            Assert.Throws(typeof(InvalidOperationException), () => menu.AddOrGetMenuItem(new[] {new MenuGroupItem("File"), new MenuGroupItem("Open", "AnotherGroup")}, _menuCommand));
        }

        [Test]
        public void GivenMenu_WhenAddingItemsWithDifferentParentGroupPrecedence_ThenThrowsException()
        {
            var menu = new Menu();
            menu.AddOrGetMenuItem(new[] {new MenuGroupItem("File", 1), new MenuGroupItem("Open")}, _menuCommand);

            Assert.Throws(typeof(InvalidOperationException), () => menu.AddOrGetMenuItem(new[] {new MenuGroupItem("File", 2), new MenuGroupItem("Close")}, _menuCommand));
        }
    }
}