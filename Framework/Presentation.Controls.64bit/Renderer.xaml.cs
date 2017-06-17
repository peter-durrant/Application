using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Hdd.Presentation.Controls._64bit
{
    /// <summary>
    ///     Interaction logic for Renderer.xaml
    /// </summary>
    public partial class Renderer : UserControl, IRenderer
    {
        public Renderer()
        {
            InitializeComponent();
        }

        public Brush Color
        {
            get
            {
                var rectangle = (Rectangle) Content;
                return rectangle.Fill;
            }
            set
            {
                var rectangle = (Rectangle) Content;
                rectangle.Fill = value;
            }
        }
    }
}