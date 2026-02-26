namespace PlexTools.API;

public class JsonFileManager
{
    public bool SaveToPath(string json, string path)
    {
        File.WriteAllText(path, json);

        // Return true if no exceptions.
        // This is mostly here incase for future catches.
        return true;
    }
}