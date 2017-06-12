using System.Windows;
using System.Windows.Controls;

namespace Hdd.Presentation.Core
{
   /// <summary>
   ///    Interaction logic for NameValueControl.xaml
   /// </summary>
   public partial class NameValueControl : UserControl
   {
      // Using a DependencyProperty as the backing store for NameText.  This enables animation, styling, binding, etc...
      public static readonly DependencyProperty NameTextProperty =
         DependencyProperty.Register("NameText", typeof(string), typeof(NameValueControl),
            new PropertyMetadata("NameText"));

      // Using a DependencyProperty as the backing store for ValueText.  This enables animation, styling, binding, etc...
      public static readonly DependencyProperty ValueTextProperty =
         DependencyProperty.Register("ValueText", typeof(string), typeof(NameValueControl),
            new PropertyMetadata("ValueText"));

      public NameValueControl()
      {
         InitializeComponent();
         (Content as FrameworkElement).DataContext = this;
      }

      public string NameText
      {
         get { return (string) GetValue(NameTextProperty); }
         set { SetValue(NameTextProperty, value); }
      }

      public string ValueText
      {
         get { return (string) GetValue(ValueTextProperty); }
         set { SetValue(ValueTextProperty, value); }
      }
   }
}