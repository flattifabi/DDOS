namespace DepDos.Core.Constants;

public class FilePaths
{
    public static readonly string BaseFolder = "C:\\DepDos\\";
    public static readonly string ConfigurationFile = "settings.xml";
    public static readonly string FavoriteFile = "favorites.xml";
    
    public static void CreateBaseFilePath()
    {
        if (!Directory.Exists(BaseFolder))
            Directory.CreateDirectory(BaseFolder);
        if (!File.Exists(BaseFolder + ConfigurationFile))
            File.Create(BaseFolder + ConfigurationFile);
        if (!File.Exists(BaseFolder + FavoriteFile))
            File.Create(BaseFolder + FavoriteFile);
    }
}