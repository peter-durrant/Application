namespace Hdd.Presentation.Core
{
    public static class MainMenuLocations
    {
        public static int Position(string name)
        {
            switch (name)
            {
                case "File":
                    return int.MinValue;

                case "Help":
                    return int.MaxValue;

                default:
                    return 0;
            }
        }
    }
}