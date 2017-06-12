using System.ComponentModel.Composition;
using System.Reflection;
using Hdd.Contract;
using Hdd.Logger;

namespace Hdd.Module2
{
    [Export(typeof(IModuleContract))]
    public class Module2Module : IModule2Contract
    {
        private readonly ILogger _logger;

        public Module2Module()
        {
            _logger = new Logger.Logger();
        }

        public IModuleContract Module { get; set; }

        public void Print()
        {
            _logger.Info(this, "Print");
        }

        public void Exit()
        {
            _logger.Info(this, "Exit");
        }

        public string Name => Assembly.GetAssembly(GetType()).GetName().Name;

        public string Version => Assembly.GetAssembly(typeof(Module2Module)).GetName().Version.ToString();

        public string SayHello => Resources.Module2.Hello;
    }
}