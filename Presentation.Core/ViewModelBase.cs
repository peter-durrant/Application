using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Hdd.Presentation.Core
{
   // From Liero, http://stackoverflow.com/a/36151255
   public abstract class ViewModelBase : INotifyPropertyChanged
   {
      public event PropertyChangedEventHandler PropertyChanged;

      protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
      {
         PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
      }
   }
}