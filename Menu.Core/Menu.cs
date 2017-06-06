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
        private readonly Dictionary<string, int> _groupPrecedence;
        private readonly MenuItem _menuRoot;

        public Menu()
        {
            _menuRoot = new MenuItem();
            _groupPrecedence = new Dictionary<string, int>();
        }

        public MenuItem AddOrGetMenuItem(IReadOnlyList<string> path)
        {
            if (path.Count == 0)
            {
                throw new InvalidOperationException("Menu item path undefined");
            }

            var menuItem = _menuRoot;
            foreach (var element in path)
            {
                menuItem = menuItem.AddOrGetMenuItem(element);
            }
            return menuItem;
        }

        public MenuItem AddOrGetMenuItem(IReadOnlyList<MenuGroupItem> path)
        {
            if (path.Count == 0)
            {
                throw new InvalidOperationException("Menu item path undefined");
            }

            var menuItem = _menuRoot;
            foreach (var element in path)
            {
                menuItem = menuItem.AddOrGetMenuItem(element);
            }
            return menuItem;
        }

        public static MenuItem AddOrGetMenuItem(MenuItem parent, MenuGroupItem item)
        {
            return parent.AddOrGetMenuItem(item);
        }

        public override string ToString()
        {
            return WriteMenu(_menuRoot);
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

        private struct GroupPrecedence
        {
            public GroupPrecedence(string name, int precedence)
            {
                Name = name;
                Precedence = precedence;
            }

            public string Name { get; }
            public int Precedence { get; }
        }
    }
}