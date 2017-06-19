using System.Windows.Input;
using System.Windows.Media;
using Hdd.Presentation.Core;

namespace Hdd.Application
{
    public class RendererViewModel : ViewModelBase
    {
        private Brush _color;

        public RendererViewModel()
        {
            ChangeColorCommand = new RelayCommand(x => { Color = Brushes.Aquamarine; });
        }

        public Brush Color
        {
            get { return _color; }
            set
            {
                _color = value;
                OnPropertyChanged();
            }
        }

        public ICommand ChangeColorCommand { get; }
    }
}