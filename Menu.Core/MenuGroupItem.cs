using System;

namespace Menu.Core
{
    public class MenuGroupItem
    {
        public MenuGroupItem(string name)
        {
            Name = name;
            Group = string.Empty;
            Precedence = 0;
        }

        public MenuGroupItem(string name, int precedence)
        {
            Name = name;
            Precedence = precedence;
            Group = string.Empty;
            GroupPrecedence = 0;
        }

        public MenuGroupItem(string name, string group)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(group))
            {
                throw new InvalidOperationException($"Menu items must be defined with name ({name}) and group ({group})");
            }
            Name = name;
            Precedence = 0;
            Group = group;
            GroupPrecedence = 0;
        }

        public MenuGroupItem(string name, int precedence, string group)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(group))
            {
                throw new InvalidOperationException($"Menu items must be defined with name ({name}) and group ({group})");
            }

            Name = name;
            Precedence = precedence;
            Group = group;
            GroupPrecedence = 0;
        }

        public MenuGroupItem(string name, int precedence, string group, int groupPrecedence)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(group))
            {
                throw new InvalidOperationException($"Menu items must be defined with name ({name}) and group ({group})");
            }

            Name = name;
            Precedence = precedence;
            Group = group;
            GroupPrecedence = groupPrecedence;
        }

        public string Name { get; }
        public int Precedence { get; }
        public string Group { get; }
        public int GroupPrecedence { get; }
    }
}