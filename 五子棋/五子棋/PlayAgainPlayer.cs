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
        }
    }
}
