namespace Hdd.Presentation.Core
{
    public interface IModuleLocationAttribute
    {
        string GroupName { get; }
        int GroupOrder { get; }
        int SubGroup { get; }
        int SubGroupOrder { get; }
    }
}