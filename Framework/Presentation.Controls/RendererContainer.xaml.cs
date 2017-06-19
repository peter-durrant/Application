using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Hdd.Presentation.Controls
{
    /// <summary>
    ///     Interaction logic for RendererContainer.xaml
    ///     Proxy to 32-bit and 64-bit implementations of Renderer
    ///     The 32-bit renderer is loaded by default (see .xaml) which has the advantage of appearing in the WPF/XAML designer.
    /// </summary>
    public partial class RendererContainer : UserControl
    {
        public static readonly DependencyProperty ColorProperty = DependencyProperty.Register("Color", typeof(Brush), typeof(RendererContainer), new PropertyMetadata(PropertyChangedCallback));

        public RendererContainer()
        {
            InitializeComponent();

            if (Environment.Is64BitProcess)
            {
                const string controls64BitAssembly = "Hdd.Presentation.Controls.64bit.dll";
                var assembly = Assembly.LoadFrom(controls64BitAssembly);
                var type = assembly.GetType("Hdd.Presentation.Controls._64bit.Renderer");
                Renderer = (IRenderer) Activator.CreateInstance(type);
            }
            else
            {
                Renderer = new Renderer();
            }
            RendererContainerControl.Content = Renderer;
        }

        private IRenderer Renderer { get; }

        public Brush Color
        {
            get { return (Brush) GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        private static void PropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            if (dependencyObject is RendererContainer container)
            {
                if (dependencyPropertyChangedEventArgs.NewValue is Brush color)
                {
                    container.Renderer.FillColor = color;
                }
            }
        }
    }
}