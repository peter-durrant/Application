using System;
using System.Globalization;
using System.Reflection;
using System.Windows;
using Hdd.Presentation.Core;

namespace Hdd.Application
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        public App()
        {
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("de");
            CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("de");

            var logger = new Logger.Logger();
            logger.Info(this, new string('-', 100));
            logger.Info(this, "Application starting up");
            logger.Info(this, $"Language: {ResourceDictionaryLoader.GetLanguage}");
        }

        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();
        }
    }
}