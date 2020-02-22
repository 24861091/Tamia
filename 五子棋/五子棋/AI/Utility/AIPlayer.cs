using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 五子棋.AI
{

    public abstract class AIPlayer : ChessPlayer
    {
        public override void OnYourTurn(棋子[][] positions, List<Position> blacks, List<Position> whites)
        {
            AIResult result = MakeTurn(positions, blacks, whites);
            result.Side = selfSide;
            FinishTurn(result.X, result.Y);
        }
        
        public abstract AIResult MakeTurn(棋子[][] positions, List<Position> blacks, List<Position> whites);
    }

    public struct AIResult
    {
        public int X;
        public int Y;
        public 棋子 Side;
        public AIResult(棋子 side, int x, int y)
        {
            X = x;
            Y = y;
            Side = side;
        }
    }
}
