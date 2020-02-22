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
        public void SetSide(棋子 side)
        {
            this.selfSide = side;
        }
        public abstract void GameStart(棋子[][] positions);
        public abstract void OnYourTurn(棋子[][] positions, List<Position> blacks, List<Position> whites);
        protected void FinishTurn(int x, int y)
        {
            Messager.Instance.SendMessageLater(MessageKey.FinishTurn, new object[] { this.selfSide, x, y });
        }
        public string Name { get; set; }
        public virtual void OnMouseClick(int x, int y)
        {

        }
        public virtual bool IsHuman()
        {
            return false;
        }
    }
}
