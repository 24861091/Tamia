using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 五子棋.AI
{
    public class DNAPlayer : AIPlayer
    {
        private 棋子[][] _positions = null;
        private DNA _dna = null;

        public override void GameStart(棋子[][] positions)
        {
            _positions = positions;
            _dna = new DNA("DNAPlayer/" + Name, Name);
        }

        public override AIResult MakeTurn(棋子[][] positions, List<Position> blacks, List<Position> whites)
        {
            AIResult result = new AIResult();
            float val = CalculateValue(positions, 棋子.黑子);
            val = CalculateValue(positions, 棋子.白子);
            for (int i = 0; i < positions.Length; i ++)
            {
                if(positions[i][i] == 棋子.无)
                {
                    result.X = i;
                    result.Y = i;
                    break;
                }
            }
            return result;
        }

        private float CalculateValue(棋子[][] positions, 棋子 side)
        {
            float val = 0f;
            if(positions == null || positions[0] == null)
            {
                return 0f;
            }
            //纵向扫描
            for (int i = 0; i < positions.Length; i ++)
            {
                val += ScanLine(i, 0, side, positions, 0, 1);
            }
            //横向扫描
            for (int i = 0; i < positions.Length; i++)
            {
                val += ScanLine(0, i, side, positions, 1, 0);
            }
            //左上右下扫描
            for (int i = 0; i < positions.Length; i++)
            {
                val += ScanLine(i, 0, side, positions, 1, 1);
            }
            for (int i = 1; i < positions.Length; i++)
            {
                val += ScanLine(0, i, side, positions, 1, 1);
            }
            //右上左下扫描
            for (int i = 0; i < positions.Length; i++)
            {
                val += ScanLine(0, i, side, positions, 1, -1);
            }
            for (int i = 1; i < positions.Length; i++)
            {
                val += ScanLine(i, positions.Length - 1, side, positions, 1, -1);
            }

            return val;
        }
        private float ScanLine(int x, int y, 棋子 side, 棋子[][] positions, int deltaX, int deltaY)
        {
            float val = 0f;
            int xLength = positions[0].Length;
            int yLength = positions.Length;
            int startX = -1;
            int startY = -1;

            int endX = -1;
            int endY = -1;
            StringBuilder builder = new StringBuilder();
            while (true)
            {
                //if(!IsInChess(x,y, xLength,yLength))
                //{
                //    break;
                //}
                bool isIn = IsInChess(x, y, xLength, yLength);
                if (isIn && side == positions[x][y])
                {
                    if(startX < 0)
                    {
                        startX = x;
                        startY = y;
                    }
                    endX = x;
                    endY = y;
                }
                else
                {
                    if(startX >= 0)
                    {
                        int num = Math.Max(Math.Abs(endX - startX + 1), Math.Abs(endY - startY + 1));
                        int space = 5 - num;
                        builder.Clear();
                        FindCode(num, space, -1, startX, startY, xLength, yLength, deltaX, deltaY, side, positions, builder);
                        builder.Append(num.ToString());
                        FindCode(num, space, 1, endX, endY, xLength, yLength, deltaX, deltaY, side, positions, builder);
                        string code = builder.ToString();
                        int length = CalculateLength(code, num);
                        if (length >= 5)
                        {
                            val += _dna.GetValue(code);
                        }
                        

                        startX = -1;
                        startY = -1;
                        endX = -1;
                        endY = -1;
                    }
                    if(!isIn)
                    {
                        break;
                    }
                }

                x += deltaX;
                y += deltaY;
            }
            return val;
        }
        private int CalculateLength(string code, int num)
        {
            return Utility.CalculateLength(code, num);
        }
        private void FindCode(int num,int space, int sign, int startX, int startY, int xLength, int yLength, int deltaX, int deltaY, 棋子 side, 棋子[][]positions, StringBuilder builder)
        {
            int tempX = -1;
            int tempY = -1;
            int start = space;
            int end = 1;
            if(sign > 0)
            {
                start = 1;
                end = space;
            }
            for (int m = start; m >= 1 && m <= space; m += sign)
            {
                tempX = startX + sign * m * deltaX;
                tempY = startY + sign * m * deltaY;

                if (!IsInChess(tempX, tempY, xLength, yLength))
                {
                    builder.Append("b");
                }
                else
                {
                    switch (positions[tempX][tempY])
                    {
                        case 棋子.无:
                            builder.Append("e");
                            break;
                        case 棋子.白子:
                        case 棋子.黑子:
                            if (positions[tempX][tempY] == side)
                            {
                                builder.Append("s");
                            }
                            else
                            {
                                builder.Append("b");
                            }
                            break;
                    }
                }
            }
        }
        private bool IsInChess(int x , int y, int xLength, int yLength)
        {
            return x >= 0 && y >= 0 && x < xLength && y < yLength;
        }
    }
}
