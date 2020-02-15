using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 五子棋.AI
{
    public static class Creater
    {
        private static Dictionary<string, ChessPlayer> dics = new Dictionary<string, ChessPlayer>();
        public static void Register(string key, ChessPlayer player)
        {
            dics[key] = player;
        }

        public static ChessPlayer GetPlayer(string key)
        {
            return dics[key];
        }
        public static Dictionary<string, ChessPlayer> Dics
        {
            get
            {
                return dics;
            }
        }
    }
}
