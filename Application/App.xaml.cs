using System.Windows;
using Hdd.Logger;

namespace Hdd.Application
{
   /// <summary>
   ///    Interaction logic for App.xaml
   /// </summary>
   public partial class App : System.Windows.Application
   {
      public App()
      {
         var logger = new Logger.Logger();
         logger.Info(this, "Application starting up");
      }

      private void App_OnStartup(object sender, StartupEventArgs e)
      {
         var mainWindow = new MainWindow();
         mainWindow.Show();
      }
   }
}