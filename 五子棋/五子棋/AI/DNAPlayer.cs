using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 五子棋.AI
{
    public class DNAPlayer : AIPlayer
    {
        private DNA _dna = null;
        public float[][] selfTest = null;
        public float[][] oppTest = null;

        public override void GameStart(棋子[][] positions)
        {
            _dna = new DNA("DNAPlayer/" + Name, Name);
            if(Utility.IsDebugOpen)
            {
                initTest(ref selfTest, positions);
                initTest(ref oppTest, positions);

            }
        }

        private void initTest(ref float[][] test, 棋子[][] positions)
        {
            if (test == null)
            {
                test = new float[positions.Length][];
                for (int i = 0; i < positions.Length; i++)
                {
                    test[i] = new float[positions[i].Length];
                    for (int j = 0; j < positions.Length; j++)
                    {
                        test[i][j] = 0;
                    }
                }
            }

        }
        private void ClearTest(ref float[][] test)
        {
            if (test != null)
            {
                for (int i = 0; i < test.Length; i++)
                {
                    for (int j = 0; j < test[0].Length; j++)
                    {
                        test[i][j] = 0;
                    }
                }
            }

        }

        public override AIResult MakeTurn(棋子[][] positions, List<Position> blacks, List<Position> whites)
        {
            AIResult result = new AIResult();
            List<KeyValuePair<int, int>> list = new List<KeyValuePair<int, int>>();


            ///test
            //positions[0][2] = 棋子.黑子;
            //float val = ScanLine(0, 0,  棋子.黑子, positions, 0, 1, list);
            //for(int i= 0; i < positions.Length; i ++)
            //{
            //    if(positions[1][i] == 棋子.无)
            //    {
            //        result.X = 1;
            //        result.Y = i;
            //        break;
            //    }
            //}
            //positions[0][2] = 棋子.无;
            //return result;
            ///test








            float selfVal = 0f;
            float oppVal = 0f;
            selfVal = CalculateValue(positions, selfSide, list);
            oppVal = CalculateValue(positions, Utility.GetOppside(selfSide), list);

            KeyValuePair<int, int> best = new KeyValuePair<int, int>();
            float bestVal = 0f;
            if(list.Count <= 0)
            {
                for(int c = 1; c <= positions.Length / 2; c ++)
                {
                    for (int i = 0; i < c * 2 + 1; i++)
                    {
                        for(int j = 0; j < c * 2 + 1; j ++)
                        {
                            int coordX = positions.Length / 2 - c + i;
                            int coordY = positions.Length / 2 - c + j;
                            if (positions[i][j] == 棋子.无)
                            {
                                result.X = coordX;
                                result.Y = coordY;
                                if (Utility.IsDebugOpen)
                                {
                                    Messager.Instance.SendMessage(MessageKey.RefreshDebug, new object[] { selfTest, oppTest });
                                }
                                return result;

                            }
                        }
                    }
                }
            }
            else
            {
                if(Utility.IsDebugOpen)
                {
                    ClearTest(ref selfTest);
                    ClearTest(ref oppTest);
                }
                for (int i = 0; i < list.Count; i++)
                {
                    KeyValuePair<int, int> pair = list[i];
                    if (IsInChess(pair.Key, pair.Value) && positions[pair.Key][pair.Value] == 棋子.无)
                    {
                        positions[pair.Key][pair.Value] = selfSide;
                        selfVal = CalculateValue(positions, selfSide, null);
                        positions[pair.Key][pair.Value] = Utility.GetOppside(selfSide);
                        oppVal = CalculateValue(positions, Utility.GetOppside(selfSide), null);

                        if(Utility.IsDebugOpen)
                        {
                              selfTest[pair.Key][pair.Value] = selfVal;
                              oppTest[pair.Key][pair.Value] = oppVal;
                        }

                        float factor = _dna.Factor;

                        if (i == 0)
                        {
                            bestVal = selfVal * factor + oppVal;
                            best = pair;
                        }
                        else
                        {
                            if (selfVal * factor + oppVal > bestVal)
                            {
                                bestVal = selfVal * factor + oppVal;
                                best = pair;
                            }
                        }

                        positions[pair.Key][pair.Value] = 棋子.无;
                    }
                }
                result.X = best.Key;
                result.Y = best.Value;
            }



            if(Utility.IsDebugOpen)
            {
                Messager.Instance.SendMessage(MessageKey.RefreshDebug, new object[] { selfTest, oppTest });
            }
            return result;
        }

        private float CalculateValue(棋子[][] positions, 棋子 side, List<KeyValuePair<int,int>> list)
        {
            float val = 0f;
            if(positions == null || positions[0] == null)
            {
                return 0f;
            }
            //纵向扫描
            for (int i = 0; i < positions.Length; i ++)
            {
                val += ScanLine(i, 0, side, positions, 0, 1, list);
            }
            //横向扫描
            for (int i = 0; i < positions.Length; i++)
            {
                val += ScanLine(0, i, side, positions, 1, 0, list);
            }
            //左上右下扫描
            for (int i = 0; i < positions.Length; i++)
            {
                val += ScanLine(i, 0, side, positions, 1, 1, list);
            }
            for (int i = 1; i < positions.Length; i++)
            {
                val += ScanLine(0, i, side, positions, 1, 1, list);
            }
            //右上左下扫描
            for (int i = 0; i < positions.Length; i++)
            {
                val += ScanLine(0, i, side, positions, 1, -1, list);
            }
            for (int i = 1; i < positions.Length; i++)
            {
                val += ScanLine(i, positions.Length - 1, side, positions, 1, -1, list);
            }

            return val;
        }
        public float ScanLine(int x, int y, 棋子 side, 棋子[][] positions, int deltaX, int deltaY, List<KeyValuePair<int, int>> list)
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
                bool isIn = IsInChess(x, y);
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
                        FindCode(num, space, -1, startX, startY, xLength, yLength, deltaX, deltaY, side, positions, builder, list);
                        builder.Append(num.ToString());
                        FindCode(num, space, 1, endX, endY, xLength, yLength, deltaX, deltaY, side, positions, builder, list);
                        Fix(ref builder);
                        string code = builder.ToString();
                        int length = CalculateLength(code, num);
                        if (length >= 5)
                        {
                            if (code == "5")
                            {
                                code = "f5";
                            }
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
        private void Fix(ref StringBuilder builder)
        {
            bool isBlock = false;
            for(int i = builder.Length / 2 - 1; i >= 0; i --)
            {
                if(isBlock)
                {
                    builder[i] = 'b';
                    continue;
                }
                isBlock = builder[i] == 'b';
            }
            isBlock = false;
            for (int i = builder.Length / 2 + 1; i < builder.Length; i++)
            {
                if (isBlock)
                {
                    builder[i] = 'b';
                    continue;
                }
                isBlock = builder[i] == 'b';
            }

        }
        private int CalculateLength(string code, int num)
        {
            return Utility.CalculateLength(code, num);
        }
        private void FindCode(int num,int space, int sign, int startX, int startY, int xLength, int yLength, int deltaX, int deltaY, 棋子 side, 棋子[][]positions, StringBuilder builder, List<KeyValuePair<int, int>> list)
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
                if (!IsInChess(tempX, tempY))
                {
                    builder.Append("b");
                }
                else
                {
                    switch (positions[tempX][tempY])
                    {
                        case 棋子.无:
                            builder.Append("e");
                            KeyValuePair<int, int> pair = new KeyValuePair<int, int>(tempX, tempY);
                            if (list != null && !list.Contains(pair))
                            {
                                list.Add(pair);
                            }
                            
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
        private bool IsInChess(int x , int y)
        {
            return Utility.IsInChess(x, y);
        }
    }
}
