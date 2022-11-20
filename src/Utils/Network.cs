using System.Net.NetworkInformation;

public static class Network
{
    private static Random _rand = new();
    public static int GetRandomPort()
    {
        int PortStartIndex = 1000;
        int PortEndIndex = 2000;
        var properties = IPGlobalProperties.GetIPGlobalProperties();
        var tcpEndPoints = properties.GetActiveTcpListeners();

        var usedPorts = tcpEndPoints.Select(p => p.Port).ToList();

        for (int count = PortStartIndex; count < PortEndIndex; count++)
        {
            var randomPort = _rand.Next(PortStartIndex, PortEndIndex);
            if (!usedPorts.Contains(randomPort))
                return randomPort;
        }

        throw new ArgumentOutOfRangeException("Should be free ports");
    }
    //public static FileInfo CopyDatabase(string dbName)
    //{
        //var dbFile = new FileInfo(dbName);
        //if (dbFile.Exists == false) throw new FileNotFoundException();
        //var copyPath = Path.Combine(TempFolder, $"{Guid.NewGuid()}{dbName}");
        //var copy = dbFile.CopyTo(copyPath);
        //return copy;
    //}
}