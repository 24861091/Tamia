using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 五子棋
{
    public static class Utility
    {
        public static bool IsDebugOpen = true;
        public static int sizeX = 15;
        public static int sizeY = 15;
        public static int left = 550;
        public static int top = 50;

        public static int interval = 50;

        public static int CalculateLength(string code, int num)
        {
            int left = -1;
            int right = code.Length;
            for (int i = code.Length / 2 - 1; i >= 0; i--)
            {
                if (code[i] == 'b')
                {
                    left = i;
                    break;
                }
            }
            for (int i = code.Length / 2 + 1; i < code.Length; i++)
            {
                if (code[i] == 'b')
                {
                    right = i;
                    break;
                }
            }
            return num + right - left - 2;
        }
        public static 棋子 GetOppside(棋子 side)
        {
            switch(side)
            {
                case 棋子.白子:
                    return 棋子.黑子;
                case 棋子.黑子:
                    return 棋子.白子;

            }
            return 棋子.无;
        }
        public static bool IsInChess(int x, int y)
        {
            return x >= 0 && y >= 0 && x < sizeX && y < sizeY;
        }

        public static ChessPlayer CreatePlayer(string typeName)
        {
            string[] s = typeName.Split('_');
            if (s != null)
            {
                Type type = Type.GetType("五子棋.AI." + s[0]);
                if (type != null)
                {
                    ChessPlayer player = Activator.CreateInstance(type) as ChessPlayer;
                    if (s.Length == 2)
                    {
                        player.Name = s[1];
                    }
                    else
                    {
                        player.Name = s[0];
                    }

                    return player;
                }

            }
            return null;

        }

        public static AI.DNAPlayer CreateDNAPlayer(string fullPath)
        {
            AI.DNAPlayer player = new AI.DNAPlayer();
            player.SetPath(Path.GetDirectoryName(fullPath));
            player.Name = Path.GetFileName(fullPath);
            return player;
        }



    }
}
