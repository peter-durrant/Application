using System;
using System.Collections.Generic;
using System.Linq;

namespace Menu.Core
{
    /// <summary>
    ///     a menu consists of top level menu items
    ///     e.g. File, Edit, View, Window, Help
    ///     each top level menu item can contain child menu items
    ///     e.g. File -> Open, File -> Close, Help -> About
    ///     each sub-menu may group similar functionality together, with a separator line delimiting the group
    ///     e.g. File -> New, File -> Open | File -> Close, File -> Close Solution | File -> Exit
    ///     menus must be extensible, so can add any item at any time to any place, so need to identify where
    /// </summary>
    public class Menu
    {
        public Menu()
        {
            RootMenu = new MenuItem();
        }

        public MenuItem RootMenu { get; }

        public MenuItem AddOrGetMenuItem(IReadOnlyList<string> path, IMenuCommand menuCommand)
        {
            if (path.Count == 0)
            {
                throw new InvalidOperationException("Menu item path undefined");
            }

            var menuItem = RootMenu;
            foreach (var element in path)
            {
                menuItem = menuItem.AddOrGetMenuItem(element);
            }
            // last item added is a leaf, so add command and other properties
            UpdateMenuItemProperties(menuCommand, menuItem);
            return menuItem;
        }

        public MenuItem AddOrGetMenuItem(IReadOnlyList<MenuGroupItem> path, IMenuCommand menuCommand)
        {
            if (path.Count == 0)
            {
                throw new InvalidOperationException("Menu item path undefined");
            }

            var menuItem = RootMenu;
            foreach (var element in path)
            {
                menuItem = menuItem.AddOrGetMenuItem(element);
            }
            // last item added is a leaf, so add command
            UpdateMenuItemProperties(menuCommand, menuItem);
            return menuItem;
        }

        public static MenuItem AddOrGetMenuItem(MenuItem parent, MenuGroupItem item, IMenuCommand menuCommand)
        {
            var menuItem = parent.AddOrGetMenuItem(item);
            UpdateMenuItemProperties(menuCommand, menuItem);
            return menuItem;
        }

        public override string ToString()
        {
            return WriteMenu(RootMenu);
        }

        private static string WriteMenu(MenuItem root, int indent = 0)
        {
            var menu = "";
            var groupedItems = new Dictionary<GroupPrecedence, List<MenuItem>>();
            foreach (var item in root.Items)
            {
                var groupPrecedence = new GroupPrecedence(item.Group, item.GroupPrecedence);
                if (!groupedItems.ContainsKey(groupPrecedence))
                {
                    groupedItems.Add(groupPrecedence, new List<MenuItem> {item});
                }
                else
                {
                    groupedItems[groupPrecedence].Add(item);
                }
            }

            var groupIndex = 0;
            var groupMax = groupedItems.Count;
            var sortedGroupedItems = groupedItems.OrderBy(x => x.Key.Precedence);

            foreach (var group in sortedGroupedItems)
            {
                var orderedInGroup = group.Value.OrderBy(item => item.Precedence);
                foreach (var item in orderedInGroup)
                {
                    menu += new string('\t', indent);
                    menu += $"{item.Name}\n";
                    menu += WriteMenu(item, indent + 1);
                }
                if (groupIndex + 1 < groupMax)
                {
                    groupIndex++;
                    menu += new string('\t', indent);
                    menu += new string('-', 10);
                    menu += '\n';
                }
            }

            return menu;
        }

        private static void UpdateMenuItemProperties(IMenuCommand menuCommand, MenuItem menuItem)
        {
            menuItem.Command = menuCommand.Command;
            menuItem.Active = menuCommand.Active;
        }

        public void AddSeparators()
        {
            AddSeparatorsToChildren(RootMenu);
        }

        private static void AddSeparatorsToChildren(MenuItem parent)
        {
            var first = true;
            var group = string.Empty;
            foreach (var item in parent.Items)
            {
                AddSeparatorsToChildren(item);
                if (first)
                {
                    first = false;
                    group = item.Group;
                }
                else if (group != item.Group)
                {
                    parent.AddSeparator();
                }
            }
        }


        private struct GroupPrecedence
        {
            public GroupPrecedence(string name, int precedence)
            {
                Name = name;
                Precedence = precedence;
            }

            private string Name { get; }
            public int Precedence { get; }
        }
    }
}