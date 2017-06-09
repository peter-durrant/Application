using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace Menu.Core
{
    public class MenuItem : IMenuCommand
    {
        private List<MenuItem> _subMenuItems;

        internal MenuItem() : this("root")
        {
        }

        private MenuItem(string name)
        {
            Name = name;
            Group = string.Empty;
            _subMenuItems = new List<MenuItem>();
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

            _subMenuItems = new List<MenuItem>();
        }

        public int Precedence { get; }
        public string Group { get; }
        public int GroupPrecedence { get; }


        public IEnumerable<MenuItem> Items => _subMenuItems;
        public bool Separator { get; set; }

        public string Id { get; set; }
        public bool Active { get; set; }
        public string Name { get; set; }
        public ICommand Command { get; set; }

        private static MenuItem CreateSeparator()
        {
            return new MenuItem {Separator = true};
        }

        internal MenuItem AddOrGetMenuItem(string name)
        {
            if (!_subMenuItems.Exists(x => x.Name == name))
            {
                _subMenuItems.Add(new MenuItem(name));
            }
            return _subMenuItems.Single(x => x.Name == name);
        }

        internal MenuItem AddOrGetMenuItem(MenuGroupItem item)
        {
            if (_subMenuItems.Exists(x => x.Name == item.Name && x.Group != item.Group))
            {
                throw new InvalidOperationException($"A menu item can only appear in one group: {item.Name} cannot be added to group {item.Group}");
            }

            var matchedItems = _subMenuItems.Where(x => x.Name == item.Name && x.Group == item.Group);
            var menuItems = matchedItems as IList<MenuItem> ?? matchedItems.ToList();
            if (!menuItems.Any())
            {
                _subMenuItems.Add(new MenuItem(item));
            }
            else if (menuItems.First().Precedence != item.Precedence || menuItems.First().GroupPrecedence != item.GroupPrecedence)
            {
                throw new InvalidOperationException(
                    $"Cannot add items with different precedence {item.Precedence}/{item.GroupPrecedence} to group precedence {menuItems.First().Precedence}/{menuItems.First().GroupPrecedence}");
            }
            return _subMenuItems.Single(x => x.Name == item.Name);
        }

        public void AddSeparator()
        {
            var subMenuItems = new List<MenuItem>();
            var first = true;
            string group = null;
            foreach (var item in _subMenuItems)
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
            _subMenuItems = subMenuItems;
        }
    }
}