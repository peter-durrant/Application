using System;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Media;

namespace Hdd.Presentation.Controls
{
    /// <summary>
    ///     Interaction logic for RendererContainer.xaml
    ///     Proxy to 32-bit and 64-bit implementations of Renderer
    ///     The 32-bit renderer is loaded by default (see .xaml) which has the advantage of appearing in the WPF/XAML designer.
    /// </summary>
    public partial class RendererContainer : UserControl, IRenderer
    {
        private readonly IRenderer _renderer;

        public RendererContainer()
        {
            InitializeComponent();

            if (Environment.Is64BitProcess)
            {
                const string controls64BitAssembly = "Hdd.Presentation.Controls.64bit.dll";
                var assembly = Assembly.LoadFrom(controls64BitAssembly);
                var type = assembly.GetType("Hdd.Presentation.Controls._64bit.Renderer");
                _renderer = (IRenderer) Activator.CreateInstance(type);
            }
            else
            {
                _renderer = new Renderer();
            }
            RendererContainerControl.Content = _renderer;
        }

        public Brush Color
        {
            get { return _renderer.Color; }
            set { _renderer.Color = value; }
        }
    }
}