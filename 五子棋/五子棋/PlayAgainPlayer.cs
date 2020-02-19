using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 五子棋.AI
{
    public class PlayAgainPlayer : ChessPlayer
    {
        public override void GameStart(棋子[][] positions)
        {

        }

        public override void OnYourTurn(棋子[][] positions, List<Position> blacks, List<Position> whites)
        {
            //FinishTurn(1, 1);
            //int pos = ((int)selfSide + 1) % 2;
            //for (int i = 0; i < 15; i++)
            //{
            //    for (int j = 0; j < 15; j++)
            //    {
            //        int m = i * 15 + j;
            //        int n = m / 4 * 4;
            //        if(n % 2 == pos && positions[i][j] == 棋子.无)
            //        {
            //            MakeStep(i, j);
            //            FinishTurn(i, j);
            //            return;
            //        }
            //    }
            //}

            //for (int i = 0; i < 15; i++)
            //{
            //    for(int k = 0; k < 5; k ++)
            //    {
            //        pos = (pos + 1) % 2;
            //        for (int j = k * 3; j < 3 + k * 3; j++)
            //        {
            //            if (i % 2 == pos && positions[i][j] == 棋子.无)
            //            {
            //                MakeStep(i, j);
            //                FinishTurn(i, j);
            //                return;
            //            }
            //        }
            //    }
            //}
            //for(int i = 0; i < 15; i ++)
            //{
            //    for(int j = 0; j < 15; j ++)
            //    {
            //        int m = i / 3 * 3;
            //        int n = j / 3 * 3;
            //        if((m + n) % 2 == pos && positions[i][j] == 棋子.无)
            //        {
            //            MakeStep(i, j);
            //            FinishTurn(i, j);
            //            return;
            //        }
            //    }
            //}
        }
    }
}
