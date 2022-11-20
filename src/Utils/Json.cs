namespace SwDb.CLI.Utils
{
    public static class Json
    {
        public static string ToJsonString(this object obj) => JsonSerializer.Serialize(obj, new JsonSerializerOptions { WriteIndented = true });
        public static void Dump(this object obj, string message) => Console.WriteLine(JsonSerializer.Serialize(new { Message = message, Value = obj }, new JsonSerializerOptions() { WriteIndented = true }));

    }
}
