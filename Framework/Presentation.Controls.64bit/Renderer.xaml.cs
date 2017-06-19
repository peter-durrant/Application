using System.Windows;
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
        public static readonly DependencyProperty ColorProperty = DependencyProperty.Register("Color", typeof(Brush), typeof(Renderer), new PropertyMetadata(PropertyChangedCallback));

        public Renderer()
        {
            InitializeComponent();
        }

        public Brush Color
        {
            get { return (Brush) GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        public Brush FillColor
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

        private static void PropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            if (dependencyObject is Renderer renderer)
            {
                if (dependencyPropertyChangedEventArgs.NewValue is Brush color)
                {
                    renderer.FillColor = color;
                }
            }
        }
    }
}