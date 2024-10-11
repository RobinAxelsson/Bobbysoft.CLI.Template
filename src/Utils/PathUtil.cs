namespace $projectName$.Utils;

public static class PathUtil
{
    public static string OsUserProfileFolder => Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
    public static string OsTempFolder => Path.GetTempPath();
    public static string Desktop => Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
    public static string OsProgramFiles => Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
    public static string OsSystemFolder => Environment.GetFolderPath(Environment.SpecialFolder.System);
    public static bool HasValidPathChars(string path) => path.IndexOfAny(Path.GetInvalidPathChars()) == -1;
    public static string CheckDirPath(string path, bool createOnMissing = false)
    {
        if (path == null) throw new ArgumentNullException(path);
        var dirInfo = new DirectoryInfo(path);
        if (!dirInfo.Exists && !createOnMissing) throw new FileNotFoundException(path);
        if (!dirInfo.Exists) dirInfo.Create();
        return dirInfo.FullName;
    }
    public static string CheckFilePath(string path, bool createOnMissing = false)
    {
        if (path == null) throw new ArgumentNullException(path);
        var fileInfo = new FileInfo(path);
        if (!fileInfo.Exists && !createOnMissing) throw new FileNotFoundException(path);
        if (!fileInfo.Exists) fileInfo.Create();
        return fileInfo.FullName;
    }
}

