using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 五子棋
{
    public abstract class ChessPlayer
    {
        protected 棋子 selfSide;
        private bool canSend = false;
        private ChessMove lastMove = new ChessMove();
        public ChessMove LastMove
        {
            get
            {
                return lastMove;
            }
            set
            {
                lastMove = value;
            }
        }
        public bool CanSend
        {
            set
            {
                canSend = value;
            }
        }
        public void SetSide(棋子 side)
        {
            this.selfSide = side;
        }
        public abstract void GameStart(棋子[][] positions);
        public abstract void TurnTo(棋子[][] positions, List<Position> blacks, List<Position> whites);
        protected void FinishTurn(int x, int y)
        {
            lastMove = new ChessMove(selfSide, x, y);
            if(canSend)
            {
                Messager.Instance.SendMessageLater(MessageKey.FinishTurn, new object[] { this.selfSide, x, y });
            }
        }
        public string Name { get; set; }
        public virtual void Put(int x, int y)
        {

        }
        public virtual bool IsHuman()
        {
            return false;
        }
    }
}
