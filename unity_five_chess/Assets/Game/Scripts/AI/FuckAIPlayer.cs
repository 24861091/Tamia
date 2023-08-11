using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 五子棋.AI
{
    class 程序测试用AI : AIPlayer
    {
        public static float[][] sTest = null;
        private int[][] datas = null;

        public override void GameStart(棋子[][] positions)
        {
            datas = new int[positions.Length][];
            sTest = new float[positions.Length][];

            for (int i = 0; i < positions.Length; i++)
            {
                datas[i] = new int[positions[i].Length];
                sTest[i] = new float[positions[i].Length];
            }

            
        }

        public override AIResult MakeTurn(棋子[][] positions, List<Position> blacks, List<Position> whites)
        {
            int x = 0;
            int y = 0;
            List<Position> enemy = null;

            if (selfSide == 棋子.白子)
            {
                enemy = blacks;
            }
            else if (selfSide == 棋子.黑子)
            {
                enemy = whites;
            }
            else
            {
                return new AIResult();
            }
            if (enemy.Count > 0)
            {
                Position p = enemy[enemy.Count - 1];
                CalculateAroudPosition(p, GetOpposite(selfSide), positions);
                int max = -1;

                for (int i = 0; i < datas.Length; i++)
                {
                    for (int j = 0; j < datas[i].Length; j++)
                    {
                        if (positions[i][j] == 棋子.无)
                        {
                            if (datas[i][j] > max)
                            {
                                x = i;
                                y = j;
                                max = datas[i][j];
                            }
                        }
                    }
                }
            }
            else
            {
                for(int i = 0; i < positions.Length; i ++)
                {
                    for (int j = 0;j < positions[0].Length; j ++)
                    {
                        if(positions[i][j] == 棋子.无)
                        {
                            x = i;
                            y = j;
                        }
                    }
                }
            }

            positions[x][y] = selfSide;
            if (selfSide == 棋子.白子)
            {
                whites.Add(new Position(x, y));
            }
            else if (selfSide == 棋子.黑子)
            {
                blacks.Add(new Position(x, y));
            }

            CalculateAroudPosition(new Position(x, y), GetOpposite(selfSide), positions);
            Messager.Instance.SendMessageLater(MessageKey.RefreshDebug, new object[] { sTest, null });
            return new AIResult(selfSide, x, y);
        }

        private void CalculateAroudPosition(Position p, 棋子 side, 棋子[][] positions)
        {
            int minX = Math.Max(0, p.X - 4);
            int maxX = Math.Min(positions[0].Length - 1, p.X + 4);
            int minY = Math.Max(0, p.Y - 4);
            int maxY = Math.Min(positions.Length - 1, p.Y + 4);

            for (int i = minX; i <= maxX; i++)
            {
                for (int j = minY; j <= maxY; j++)
                {
                    int deltaX = Math.Abs(p.X - i);
                    int deltaY = Math.Abs(p.Y - j);
                    if(datas[i][j] >= 0)
                    {
                        if (deltaX == 0 && deltaY == 0)
                        {
                            datas[i][j] = -1;
                        }
                        else if (deltaX == 0)
                        {
                            datas[i][j] = CalculateValue(i, j, positions, p, deltaY);
                            //datas[i][j] += (5 - deltaY) * 50;
                        }
                        else if (deltaY == 0)
                        {
                            datas[i][j] = CalculateValue(i, j, positions, p, deltaX);
                        }
                        else if (deltaX == deltaY)
                        {
                            datas[i][j] = CalculateValue(i, j, positions, p, deltaX);
                        }

                    }
                }
            }

        }

        private int CalculateValue(int x, int y, 棋子[][] positions,Position p,int delta)
        {
            if(positions[x][y] != 棋子.无)
            {
                return -1;
            }
            int value = 0;
            value += CalculateLine(positions, 0, 1, x, y, selfSide, p, delta);
            value += CalculateLine(positions, 1, 0, x, y, selfSide, p, delta);
            value += CalculateLine(positions, 1, 1, x, y, selfSide, p, delta);
            value += CalculateLine(positions, -1, 1, x, y, selfSide, p, delta);

            棋子 opp = GetOpposite(selfSide);

            value += CalculateLine(positions, 0, 1, x, y, opp, p, delta);
            value += CalculateLine(positions, 1, 0, x, y, opp, p, delta);
            value += CalculateLine(positions, 1, 1, x, y, opp, p, delta);
            value += CalculateLine(positions, -1, 1, x, y, opp, p, delta);

            return value;
        }
        private 棋子 GetOpposite(棋子 q)
        {
            if (q == 棋子.白子)
                return 棋子.黑子;
            if (q == 棋子.黑子)
                return 棋子.白子;
            return 棋子.无;
        }
        public 棋子 GetSide(int x, int y,int sizeX, int sizeY, 棋子[][] positions)
        {
            if (x < 0 || x >= sizeX) return 棋子.无;
            if (y < 0 || y >= sizeY) return 棋子.无;
            return positions[x][y];
        }

        private int CalculateLine(棋子[][] positions,int deltaX, int deltaY, int x, int y, 棋子 side, Position p, int delta)
        {
            if (side == 棋子.无) return 0;

            int leftNum = 0;
            int rightNum = 0;
            bool left = true;
            bool right = true;
            for (int i = 1; i <= 4; i++)
            {
                if (left)
                {
                    棋子 s = GetSide(x - deltaX * i, y - deltaY * i, positions.Length, positions[x].Length, positions);
                    if (s == side)
                        leftNum++;
                    else if (s != 棋子.无)
                        left = false;
                    else if (IsBeyondBorder(x - deltaX * i, positions.Length) || IsBeyondBorder(y - deltaY * i, positions.Length))
                        left = false;
                }
                if (right)
                {
                    棋子 s = GetSide(x + deltaX * i, y + deltaY * i, positions.Length, positions[x].Length, positions);
                    if (s == side)
                        rightNum++;
                    else if (s != 棋子.无)
                        right = false;
                    else if (IsBeyondBorder(x - deltaX * i, positions.Length) || IsBeyondBorder(y - deltaY * i, positions.Length))
                        right = false;
                }
                if (!left && !right)
                {
                    break;
                }
            }
            return CalculateValue(leftNum, rightNum, left, right, p, delta);
            
        }
        private int CalculateValue(int left,int right,bool isLeftAlive, bool isRightAlive, Position p, int delta)
        {

            if (left + right == 4)
            {
                return 10000;
            }
            if(!isLeftAlive || !isRightAlive)
            {
                return 1;
            }
            int num = left + right;
            int v = 1;
            for(int i = 1; i <= num; i ++)
            {
                v *= (i) * 4;
            }
            v += 5 - delta;

            return v;
        }
        private bool IsBeyondBorder(int x, int num)
        {
            return x < 0 || x >= num;
        }

    }
}
