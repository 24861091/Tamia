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
            MakeStep(result.X, result.Y);
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

    //class AIPlayerExample1 : AIPlayer
    //{
    //    private int turn = 0;
    //    public override AIResult MakeTurn(棋子[][] positions, List<Position> blacks, List<Position> whites)
    //    {
    //        MakeStep(turn, turn);
    //        turn++;
    //        return new AIResult(side, turn - 1, turn - 1);
    //    }

    //    public override void StartGame(棋子[][] positions)
    //    {
            
    //    }
    //}
    //class AIPlayerExample2 : AIPlayer
    //{
    //    private int turn = 14;
    //    public override AIResult MakeTurn(棋子[][] positions, List<Position> blacks, List<Position> whites)
    //    {
    //        MakeStep(turn, turn);
    //        turn--;
    //        return new AIResult(side,turn + 1, turn + 1);
    //    }

    //    public override void StartGame(棋子[][] positions)
    //    {
            
    //    }
    //}

}
