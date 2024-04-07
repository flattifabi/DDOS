namespace DepDos.Core.Interfaces;

public interface IDosConfiguration
{
    List<string> Urls { get; set; }
    string Name { get; set; }
    bool StopIfDown { get; set; }
    int RequestsPerMinute { get; set; }
    int ParallelismLimit { get; set; }
}

public class DosConfiguration : IDosConfiguration
{
    public List<string> Urls { get; set; }
    public string Name { get; set; }
    public bool StopIfDown { get; set; }
    public int RequestsPerMinute { get; set; }
    public int ParallelismLimit { get; set; }
}
