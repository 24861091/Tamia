using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 五子棋.AI
{
    class HumanChessPlayer : ChessPlayer
    {
        private 棋子[][] positions;


        public override void TurnTo(棋子[][] positions, List<Position> blacks, List<Position> whites)
        {
            
        }



        public override void Put(int x, int y)
        {
            if(x < 0 || x >= Utility.sizeX || y < 0 || y >= Utility.sizeY)
            {
                return;
            }
            if(positions[x][y] == 棋子.无)
            {
                FinishTurn(x, y);
            }
        }

        public override bool IsHuman()
        {
            return true;
        }


        public override void GameStart(棋子[][] positions)
        {
            this.positions = positions;
        }

    }
}
