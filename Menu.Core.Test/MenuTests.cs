using System;
using NUnit.Framework;

namespace Menu.Core.Test
{
    [TestFixture]
    public class MenuTests
    {
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

            menu.AddOrGetMenuItem(new[] {"File"});
            menu.AddOrGetMenuItem(new[] {"Edit"});
            menu.AddOrGetMenuItem(new[] {"View"});
            menu.AddOrGetMenuItem(new[] {"Help"});

            Assert.AreEqual("File\nEdit\nView\nHelp\n", menu.ToString());
            Console.WriteLine(menu);
        }

        [Test]
        public void GivenMenuWithTopLevelItems_WhenAdded_ThenReturnsCorrectMenuItem()
        {
            var menu = new Menu();

            var fileItem = menu.AddOrGetMenuItem(new[] {"File"});
            var editItem = menu.AddOrGetMenuItem(new[] {"Edit"});
            var viewItem = menu.AddOrGetMenuItem(new[] {"View"});
            var helpItem = menu.AddOrGetMenuItem(new[] {"Help"});

            Assert.AreEqual("File", fileItem.Name);
            Assert.AreEqual("Edit", editItem.Name);
            Assert.AreEqual("View", viewItem.Name);
            Assert.AreEqual("Help", helpItem.Name);
        }

        [Test]
        public void GivenMenu_WhenAddingItemsByPath_ThenGeneratesCorrectMenu()
        {
            var menu = new Menu();

            var openItem = menu.AddOrGetMenuItem(new[] {"File", "Open"});
            var copyItem = menu.AddOrGetMenuItem(new[] {"Edit", "Copy"});
            var viewItem = menu.AddOrGetMenuItem(new[] {"View"});
            var aboutItem = menu.AddOrGetMenuItem(new[] {"Help", "About"});

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

            var openItem = menu.AddOrGetMenuItem(new[] {new MenuGroupItem("File"), new MenuGroupItem("Open", "FileNew")});
            var gotoItem = menu.AddOrGetMenuItem(new[] {new MenuGroupItem("Edit"), new MenuGroupItem("Goto", "EditGoto")});
            var cutItem = menu.AddOrGetMenuItem(new[] {new MenuGroupItem("Edit"), new MenuGroupItem("Cut", "EditCopyAndPaste")});
            var copyItem = menu.AddOrGetMenuItem(new[] {new MenuGroupItem("Edit"), new MenuGroupItem("Copy", "EditCopyAndPaste")});
            var pasteItem = menu.AddOrGetMenuItem(new[] {new MenuGroupItem("Edit"), new MenuGroupItem("Paste", "EditCopyAndPaste")});
            var viewItem = menu.AddOrGetMenuItem(new[] {"View"});
            var aboutItem = menu.AddOrGetMenuItem(new[] {"Help", "About"});
            var feedbackItem = menu.AddOrGetMenuItem(new[] {new MenuGroupItem("Help"), new MenuGroupItem("Send Feedback", "HelpFeedback")});
            var samplesItem = menu.AddOrGetMenuItem(new[] {new MenuGroupItem("Help"), new MenuGroupItem("Samples", "HelpSamples")});
            var reportAProblemItem = Menu.AddOrGetMenuItem(feedbackItem, new MenuGroupItem("Report a Problem...", "HelpFeedbackReportAProblem"));

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

            var openItem = menu.AddOrGetMenuItem(new[] {new MenuGroupItem("File"), new MenuGroupItem("Open", 1, "FileNew")});
            var gotoItem = menu.AddOrGetMenuItem(new[] {new MenuGroupItem("Edit"), new MenuGroupItem("Goto", 5, "EditGoto")});
            var copyItem = menu.AddOrGetMenuItem(new[] {new MenuGroupItem("Edit"), new MenuGroupItem("Copy", 2, "EditCopyAndPaste")});
            var cutItem = menu.AddOrGetMenuItem(new[] {new MenuGroupItem("Edit"), new MenuGroupItem("Cut", 1, "EditCopyAndPaste")});
            var pasteItem = menu.AddOrGetMenuItem(new[] {new MenuGroupItem("Edit"), new MenuGroupItem("Paste", 3, "EditCopyAndPaste")});
            var viewItem = menu.AddOrGetMenuItem(new[] {"View"});
            var aboutItem = menu.AddOrGetMenuItem(new[] {new MenuGroupItem("Help", 20), new MenuGroupItem("About")});
            var feedbackItem = menu.AddOrGetMenuItem(new[] {new MenuGroupItem("Help", 20), new MenuGroupItem("Send Feedback", "HelpFeedback")});
            var samplesItem = menu.AddOrGetMenuItem(new[] {new MenuGroupItem("Help", 20), new MenuGroupItem("Samples", "HelpSamples")});
            var reportAProblemItem = Menu.AddOrGetMenuItem(feedbackItem, new MenuGroupItem("Report a Problem...", "HelpFeedbackReportAProblem"));

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

            var openItem = menu.AddOrGetMenuItem(new[] {new MenuGroupItem("File", 3), new MenuGroupItem("Open", 1, "FileNew")});
            var gotoItem = menu.AddOrGetMenuItem(new[] {new MenuGroupItem("Edit"), new MenuGroupItem("Goto", 5, "EditGoto", 5)});
            var copyItem = menu.AddOrGetMenuItem(new[] {new MenuGroupItem("Edit"), new MenuGroupItem("Copy", 2, "EditCopyAndPaste", 1)});
            var cutItem = menu.AddOrGetMenuItem(new[] {new MenuGroupItem("Edit"), new MenuGroupItem("Cut", 1, "EditCopyAndPaste", 1)});
            var pasteItem = menu.AddOrGetMenuItem(new[] {new MenuGroupItem("Edit"), new MenuGroupItem("Paste", 3, "EditCopyAndPaste", 1)});
            var viewItem = menu.AddOrGetMenuItem(new[] {"View"});
            var aboutItem = menu.AddOrGetMenuItem(new[] {new MenuGroupItem("Help", 20), new MenuGroupItem("About")});
            var feedbackItem = menu.AddOrGetMenuItem(new[] {new MenuGroupItem("Help", 20), new MenuGroupItem("Send Feedback", "HelpFeedback")});
            var samplesItem = menu.AddOrGetMenuItem(new[] {new MenuGroupItem("Help", 20), new MenuGroupItem("Samples", "HelpSamples")});
            var reportAProblemItem = Menu.AddOrGetMenuItem(feedbackItem, new MenuGroupItem("Report a Problem...", "HelpFeedbackReportAProblem"));

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

            menu.AddOrGetMenuItem(new[] {new MenuGroupItem("File"), new MenuGroupItem("Open")});
            menu.AddOrGetMenuItem(new[] {new MenuGroupItem("File"), new MenuGroupItem("Open")});

            Console.WriteLine(menu);

            Assert.AreEqual("File\n\tOpen\n", menu.ToString());
        }

        [Test]
        public void GivenMenu_WhenAddingItemsWithDifferentPrecedence_ThenOrdersItemsByPrecedence()
        {
            var menu = new Menu();

            menu.AddOrGetMenuItem(new[] {new MenuGroupItem("File"), new MenuGroupItem("Open", 2)});
            menu.AddOrGetMenuItem(new[] {new MenuGroupItem("File"), new MenuGroupItem("Close", 1)});

            Console.WriteLine(menu);

            Assert.AreEqual("File\n\tClose\n\tOpen\n", menu.ToString());
        }

        [Test]
        public void GivenMenu_WhenAddingItemsWithEmptyStringPath_ThenThrowsException()
        {
            var menu = new Menu();

            Assert.Throws(typeof(InvalidOperationException), () => menu.AddOrGetMenuItem(new string[] {}));
        }

        [Test]
        public void GivenMenu_WhenAddingItemsWithEmptyMenuItemPath_ThenThrowsException()
        {
            var menu = new Menu();

            Assert.Throws(typeof(InvalidOperationException), () => menu.AddOrGetMenuItem(new MenuGroupItem[] {}));
        }

        [Test]
        public void GivenMenu_WhenAddingItemsWithDifferentGroupPrecedence_ThenThrowsException()
        {
            var menu = new Menu();
            menu.AddOrGetMenuItem(new[] {new MenuGroupItem("File", 3), new MenuGroupItem("Open")});

            Assert.Throws(typeof(InvalidOperationException), () => menu.AddOrGetMenuItem(new[] {new MenuGroupItem("File"), new MenuGroupItem("Close")}));
        }

        [Test]
        public void GivenMenu_WhenAddingItemsWithDifferentGroup_ThenThrowsException()
        {
            var menu = new Menu();
            menu.AddOrGetMenuItem(new[] {new MenuGroupItem("File"), new MenuGroupItem("Open", "OpenGroup")});

            Assert.Throws(typeof(InvalidOperationException), () => menu.AddOrGetMenuItem(new[] {new MenuGroupItem("File"), new MenuGroupItem("Open", "AnotherGroup")}));
        }

        [Test]
        public void GivenMenu_WhenAddingItemsWithDifferentParentGroupPrecedence_ThenThrowsException()
        {
            var menu = new Menu();
            menu.AddOrGetMenuItem(new[] {new MenuGroupItem("File", 1), new MenuGroupItem("Open")});

            Assert.Throws(typeof(InvalidOperationException), () => menu.AddOrGetMenuItem(new[] {new MenuGroupItem("File", 2), new MenuGroupItem("Close")}));
        }
    }
}