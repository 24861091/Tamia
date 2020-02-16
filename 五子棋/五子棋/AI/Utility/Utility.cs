using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 五子棋
{
    public static class Utility
    {
        public static bool IsDebugOpen = true;
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

    }
}
