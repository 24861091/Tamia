using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 五子棋
{
    public abstract class ChessPlayer
    {
        protected 棋子 side;
        public void SetSide(棋子 side)
        {
            this.side = side;
        }
        
        public abstract void GameStart(棋子[][] positions);
        protected void MakeStep(int x, int y)
        {
            Messager.Instance.SendMessage(MessageKey.MakeStep, new object[] { side, x, y });
        }
        public abstract void OnYourTurn(棋子[][] positions, List<Position> blacks, List<Position> whites);
        protected void FinishTurn(int x, int y)
        {
            Messager.Instance.SendMessage(MessageKey.FinishTurn, new object[] { this.side, x, y });
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
