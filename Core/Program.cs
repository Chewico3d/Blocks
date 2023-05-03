using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core {
    internal class Program {
        public static void Main(string[] args) {
            BlocksGame BG = new BlocksGame((int)(1920 * .7f), (int)(1080 * .7f), "BlockGame");

            BG.Run();
        }
    }
}
