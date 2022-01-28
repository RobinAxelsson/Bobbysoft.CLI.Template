using System;
using System.Collections.Generic;
using System.Linq;

namespace BotCli
{
    public partial class Utils
    {
        public static string Menu(string[] options){

            Console.Clear();

            var oPts = options.Aggregate( new List<OptionPoint>(), (oPts, opt) => {
                Console.Write(opt);
                var cp = Console.GetCursorPosition();
                oPts.Add(new OptionPoint(cp.Left +1, cp.Top, opt, false));
                Console.Write(Environment.NewLine);
                return oPts;
            });

            void Draw(OptionPoint opt){
                if(opt.IsDrawn) return;
                Console.SetCursorPosition(opt.Left, opt.Top);
                Console.Write('$');
                opt.IsDrawn = true;
            };

            void Erase(OptionPoint opt){
                if(opt.IsDrawn == false) return;
                Console.SetCursorPosition(opt.Left, opt.Top);
                Console.Write(' ');
                opt.IsDrawn = false;
            }

            void Refresh(int selIndex){
                var selected = oPts[selIndex];
                oPts.Where(o => o != selected).ToList().ForEach(x => Erase(x));
                Draw(selected);
            }

            int selIndex = 0;
            Draw(oPts[selIndex]);
            var keyInfo = Console.ReadKey(true);

            while (keyInfo.Key != ConsoleKey.Enter)
            {
                //If up arrow index goes down if not 0
                if (keyInfo.Key == ConsoleKey.UpArrow && selIndex > 0) selIndex--;

                //if down arrow index goes up if note length of array
                if (keyInfo.Key == ConsoleKey.DownArrow && selIndex < options.Length - 1) selIndex++;

                Refresh(selIndex);

                keyInfo = Console.ReadKey(true);
            }
            Console.SetCursorPosition(0, oPts[^1].Top + 1);
            Console.WriteLine();
            return oPts[selIndex].Option;
        }
        private class OptionPoint
        {
            public OptionPoint(int Left, int Top, string Option, bool IsDrawn){
                this.Left = Left;
                this.Top = Top;
                this.Option = Option;
                this.IsDrawn = IsDrawn;
            }

            public int Left { get; }
            public int Top { get; }
            public string Option { get; }
            public bool IsDrawn { get; set; }
        }
    }
}