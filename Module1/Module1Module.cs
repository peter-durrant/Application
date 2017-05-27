using System.ComponentModel.Composition;
using System.Reflection;
using Hdd.Contract;
using Hdd.Logger;

namespace Hdd.Module1
{
    [Export(typeof(IModuleContract))]
    public class Module1Module : IModule1Contract
    {
        private readonly ILogger _logger;

        public Module1Module()
        {
            _logger = new Logger.Logger();
        }

        public IModuleContract Module { get; set; }

        public void Open()
        {
            _logger.Info(this, "Open");
        }

        public void Close()
        {
            _logger.Info(this, "Close");
        }

        public string Name => Assembly.GetAssembly(GetType()).GetName().Name;

        public string Version => Assembly.GetAssembly(typeof(Module1Module)).GetName().Version.ToString();

        public string SayHello => Resources.Module1.Hello;
    }
}