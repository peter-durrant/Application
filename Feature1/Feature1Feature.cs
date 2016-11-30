using System.ComponentModel.Composition;
using Hdd.Contract;

namespace Hdd.Feature1
{
   [Export(typeof(IFeature1Contract))]
   public class Feature1Feature : IFeature1Contract
   {
      public string Name => "Feature 1";
   }
}