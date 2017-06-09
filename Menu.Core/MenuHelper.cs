using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Menu.Core
{
    public static class MenuHelper
    {
        #region Parse

        public static void Parse(this Menu menu, IEnumerable<string> items, IMenuCommand menuItem)
        {
            menu.AddOrGetMenuItem(items.Select(Parse).ToList(), menuItem);
        }

        private static MenuGroupItem Parse(string item)
        {
            const string pattern = @"(?<name>[A-Za-z]+)((\|)(?<precedence>\d+))?((\|)(?<group>[A-Za-z]+))?((\|)(?<groupPrecedence>\d+))?";
            var regex = new Regex(pattern);

            var match = regex.Match(item);
            if (match.Success)
            {
                var name = match.Groups["name"].Value;
                var precedence = match.Groups["precedence"].Value;
                var group = match.Groups["group"].Value;
                var groupPrecedence = match.Groups["groupPrecedence"].Value;

                if (string.IsNullOrEmpty(name))
                {
                    throw new InvalidOperationException($"menu name is undefined: {item}");
                }

                if (string.IsNullOrEmpty(precedence) && string.IsNullOrEmpty(group) && string.IsNullOrEmpty(groupPrecedence))
                {
                    return new MenuGroupItem(name);
                }

                if (!string.IsNullOrEmpty(precedence) && string.IsNullOrEmpty(group) && string.IsNullOrEmpty(groupPrecedence))
                {
                    return new MenuGroupItem(name, int.Parse(precedence));
                }

                if (string.IsNullOrEmpty(precedence) && !string.IsNullOrEmpty(group) && string.IsNullOrEmpty(groupPrecedence))
                {
                    return new MenuGroupItem(name, group);
                }

                if (!string.IsNullOrEmpty(precedence) && !string.IsNullOrEmpty(group) && string.IsNullOrEmpty(groupPrecedence))
                {
                    return new MenuGroupItem(name, int.Parse(precedence), group);
                }

                if (!string.IsNullOrEmpty(precedence) && !string.IsNullOrEmpty(group) && !string.IsNullOrEmpty(groupPrecedence))
                {
                    return new MenuGroupItem(name, int.Parse(precedence), group, int.Parse(groupPrecedence));
                }

                throw new InvalidOperationException($"invalid combination of menu parameters");
            }
            throw new InvalidOperationException($"menu parameters syntax error: {item}");
        }
    }

    #endregion Parse
}