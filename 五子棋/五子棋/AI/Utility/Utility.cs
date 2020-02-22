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
        public static int evoluteBase = 10000;

        private static Random random = new Random();

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

        public static T RandomInt<T>(T[] datas, int[] rates)
        {
            if (datas == null || rates == null || datas.Length != rates.Length)
            {
                return default(T);
            }
            int total = 0;
            for (int i = 0; i < rates.Length; i++)
            {
                total += rates[i];
            }
            int r = random.Next(0, total);
            for (int i = 0; i < rates.Length; i++)
            {
                r -= rates[i];
                if (r < 0)
                {
                    return datas[i];
                }
            }
            return default(T);
        }
        public static bool RandomRate(int rate, int total)
        {
            return random.Next(0, total) < rate;
        }
        public static double RandomValue(float source, int rate, float min, float max)
        {
            bool change = RandomRate(rate, evoluteBase);
            if(min >= 1f || max <= 1f)
            {
                return 0f;
            }
            if(change)
            {
                bool isMin = RandomRate(50, 100);
                double d = random.NextDouble();
                if (isMin)
                {
                    return d * (source - min * source) + min * source;
                }
                else
                {
                    return d * (max * source - source) + source;
                }
            }
            else
            {
                return source;
            }
        }
        public static void ClearDirectory(string directory)
        {
            if (!string.IsNullOrEmpty(directory) && Directory.Exists(directory))
            {
                string[] files = Directory.GetFiles(directory);
                if (files != null)
                {
                    for (int i = 0; i < files.Length; i++)
                    {
                        File.Delete(files[i]);
                    }
                }
            }
        }
        public static string CreateSourcePath(int generation)
        {
            return @"league\generation" + generation.ToString() + @"\parent";
        }
        public static string CreateTargetPath(int generation)
        {
            return @"league\generation" + generation.ToString() + @"\children";
        }
        public static void MakeSure(string path)
        {
            if(!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

    }
}
