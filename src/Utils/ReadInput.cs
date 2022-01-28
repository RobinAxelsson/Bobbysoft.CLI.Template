using System;

namespace BotCli
{
    public partial class Utils
    {
        public static string ReadInput(int maxlength, bool hideText = true)
        {
            var inputString = string.Empty;
            var keyInfo = Console.ReadKey(true);

            while (keyInfo.Key != ConsoleKey.Enter)
            {
                //exit keys
                if(keyInfo.Key == ConsoleKey.Enter){
                    break;
                }
                //Erasing text (backspace)
                if (keyInfo.Key == ConsoleKey.Backspace && inputString.Length > 0)
                {
                    inputString = inputString.Remove(inputString.Length - 1);
                    Console.CursorLeft--;
                    Console.Write(" ");
                    Console.CursorLeft--;
                    keyInfo = Console.ReadKey(true);
                    continue;
                }
                //Disabled keys
                if (keyInfo.Key == ConsoleKey.Tab || keyInfo.Key == ConsoleKey.Spacebar){
                    keyInfo = Console.ReadKey(true);
                    continue;
                }
                //Writes to console if not maxlength
                if (inputString.Length < maxlength)
                {
                    inputString += keyInfo.KeyChar;
                    Console.Write(hideText ? "*" : keyInfo.KeyChar);
                }
                //All other cases wait for new key
                keyInfo = Console.ReadKey(true);
            }
            return inputString;
        }
    }
}

