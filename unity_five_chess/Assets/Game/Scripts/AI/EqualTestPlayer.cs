using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 五子棋.AI
{
    public class EqualTestPlayer : ChessPlayer
    {
        public override void GameStart(棋子[][] positions)
        {

        }

        public override void OnYourTurn(棋子[][] positions, List<Position> blacks, List<Position> whites)
        {
            int pos = (int)selfSide % 2;
            for(int k = 0; k < 5; k ++)
            {
                pos = (pos + 1) % 2;

                for (int i = 0; i < 15; i++)
                {
                    for (int j = 0 + 3 * k; j < 3 + 3 * k; j++)
                    {
                        if (positions[i][j] == 棋子.无 && i % 2 == pos)
                        {
                            FinishTurn(i, j);
                            return;
                        }
                    }
                }

            }

            for(int i =0; i < 15; i ++)
            {
                for (int j = 0; j < 15; j++)
                {
                    if(positions[i][j] == 棋子.无)
                    {
                        FinishTurn(i, j);
                        return;
                    }
                }
            }
        }
    }
}
