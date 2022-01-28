using System;
namespace BotCli
{
    public partial class Utils
    {
        //NOTE: if you check the pipe this way we cant get back the Console.ReadLine for inputs
        public static string CheckPipe()
        {
            //Get piped values from last command
            string pipedText = null;
            try
            {
                bool isKeyAvailable = System.Console.KeyAvailable;
            }
            catch (InvalidOperationException expected)
            {
                pipedText = System.Console.In.ReadToEnd();
            }
            return pipedText;
        }
    }

    // Use these for clipboard!
    //
    // ClipboardService.GetText();
    // ClipboardService.SetText(string text);
}