namespace $projectName$.Utils
{
    public static class Pipe
    {
        //NOTE: if you check the pipe this way we cant get back the Console.ReadLine for inputs
        public static string Check()
        {
            //Get piped values from last command
            string pipedText = null;
            try
            {
                var isKeyAvailable = Console.KeyAvailable;
            }
            catch (InvalidOperationException expected)
            {
                pipedText = Console.In.ReadToEnd();
            }
            return pipedText;
        }
    }

    // Use these for clipboard!
    //
    // ClipboardService.GetText();
    // ClipboardService.SetText(string text);
}