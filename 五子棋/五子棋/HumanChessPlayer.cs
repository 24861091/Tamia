using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 五子棋
{
    class HumanChessPlayer : ChessPlayer
    {
        private 棋子[][] positions;


        public override void OnYourTurn(棋子[][] positions, List<Position> blacks, List<Position> whites)
        {
            
        }



        public override void OnMouseClick(int x, int y)
        {
            if(positions[x][y] == 棋子.无)
            {
                Messager.Instance.SendMessage(MessageKey.MakeStep, new object[] { side, x, y });
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
