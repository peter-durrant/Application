using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Menu.Core
{
    public sealed class MenuItem : IMenuCommand
    {
        internal MenuItem() : this("root")
        {
        }

        private MenuItem(string name)
        {
            Name = name;
            Group = string.Empty;
            Items = new ObservableCollection<MenuItem>();
        }

        private MenuItem(MenuGroupItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            Name = item.Name;
            Precedence = item.Precedence;
            Group = item.Group;
            GroupPrecedence = item.GroupPrecedence;

            Items = new ObservableCollection<MenuItem>();
        }

        public int Precedence { get; }
        public string Group { get; }
        public int GroupPrecedence { get; }

        public IMenuCommand MenuCommand { get; set; }

        public ObservableCollection<MenuItem> Items { get; private set; }

        public bool Separator { get; set; }

        public bool Active
        {
            get { return MenuCommand?.Active == null || MenuCommand.Active; }
            set { MenuCommand.Active = value; }
        }

        public ICommand Command => MenuCommand?.Command;

        public string Id { get; set; }
        public string Name { get; set; }

        private static MenuItem CreateSeparator()
        {
            return new MenuItem {Separator = true};
        }

        internal MenuItem AddOrGetMenuItem(string name)
        {
            if (Items.All(item => item.Name != name))
            {
                Items.Add(new MenuItem(name));
            }
            return Items.Single(x => x.Name == name);
        }

        internal MenuItem AddOrGetMenuItem(MenuGroupItem item)
        {
            if (Items.Any(x => x.Name == item.Name && x.Group != item.Group))
            {
                throw new InvalidOperationException($"A menu item can only appear in one group: {item.Name} cannot be added to group {item.Group}");
            }

            var matchedItems = Items.Where(x => x.Name == item.Name && x.Group == item.Group);
            var menuItems = matchedItems as IList<MenuItem> ?? matchedItems.ToList();
            if (!menuItems.Any())
            {
                Items.Add(new MenuItem(item));
            }
            else if (menuItems.First().Precedence != item.Precedence || menuItems.First().GroupPrecedence != item.GroupPrecedence)
            {
                throw new InvalidOperationException(
                    $"Cannot add items with different precedence {item.Precedence}/{item.GroupPrecedence} to group precedence {menuItems.First().Precedence}/{menuItems.First().GroupPrecedence}");
            }
            return Items.Single(x => x.Name == item.Name);
        }

        public void AddSeparator()
        {
            var subMenuItems = new ObservableCollection<MenuItem>();
            var first = true;
            string group = null;
            foreach (var item in Items)
            {
                if (first)
                {
                    first = false;
                    group = item.Group;
                }
                else if (item.Group != group)
                {
                    subMenuItems.Add(CreateSeparator());
                    group = item.Group;
                }
                subMenuItems.Add(item);
            }
            Items = subMenuItems;
        }
    }
}