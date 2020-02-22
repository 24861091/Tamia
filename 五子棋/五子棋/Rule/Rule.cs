using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 五子棋
{
    public class Rule : IListener
    {
        private ChessPannel panel = new ChessPannel();
        private ChessPlayer black = null;
        private ChessPlayer white = null;
        private 棋子 turn = 棋子.无;

        public Rule()
        {
        }
        public void Clear()
        {
            turn = 棋子.无;
            panel.Clear();
            black = null;
            white = null;
        }
        public ChessPannel GetPanel()
        {
            return panel;
        }
        public void Initialize(int sizeX, int sizeY)
        {
            panel.Initialize(sizeX, sizeY);
            Messager.Instance.Register(MessageKey.MouseDown, this);
            Messager.Instance.Register(MessageKey.FinishTurn, this);
        }
        public void SetChessPlayers(ChessPlayer black, ChessPlayer white)
        {
            this.black = black;
            this.black.SetSide(棋子.黑子);
            this.black.GameStart(panel.Positions);
            this.white = white;
            this.white.SetSide(棋子.白子);
            this.white.GameStart(panel.Positions);
            //bool canSend = black.IsHuman() || white.IsHuman();
            this.black.CanSend = true;
            this.white.CanSend = true;
        }
        public void Restart()
        {
            if(this.black.IsHuman() || this.white.IsHuman())
            {
                this.ChangeTurn();
                this.OnYourTurn();
            }
            else
            {
                棋子 side = 棋子.无;
                int x = 0;
                int y = 0;
                while (true)
                {
                    this.ChangeTurn();
                    this.OnYourTurn();

                    if(turn == 棋子.白子)
                    {
                        side = this.white.LastMove.Side;
                        x = this.white.LastMove.Position.X;
                        y = this.white.LastMove.Position.Y;
                    }
                    else if(turn == 棋子.黑子)
                    {
                        side = this.black.LastMove.Side;
                        x = this.black.LastMove.Position.X;
                        y = this.black.LastMove.Position.Y;
                    }
                    else
                    {
                        MessageBox.Show("Error！过程中出现错误！");
                        return;
                    }
                    _MakeStep(side, x, y);
                    if (Judge(x, y, side))
                    {
                        Finish(side);
                        return;
                    }
                    else if (JudgeEqual())
                    {
                        Finish(棋子.无);
                        return;
                    }
                }

            }
        }
        public void OnYourTurn()
        {
            if (turn == 棋子.黑子)
            {
                ChessPannel panel = this.GetPanel();
                black.OnYourTurn(panel.Positions, panel.BlackList, panel.WhiteList);
            }
            else if (turn == 棋子.白子)
            {
                ChessPannel panel = this.GetPanel();
                white.OnYourTurn(panel.Positions, panel.BlackList, panel.WhiteList);
            }
        }
        public void FinishTurn(棋子 side, int x, int y)
        {
            _MakeStep(side, x, y);

            if (Judge(x, y, side))
            {
                Finish(side);
            }
            else if (JudgeEqual())
            {
                Finish(棋子.无);
            }
            else
            {
                if (turn != 棋子.无)
                {
                    ChangeTurn();
                    OnYourTurn();
                }
            }
        }
        private bool JudgeEqual()
        {
            ChessPannel panel = this.GetPanel();
            for (int i = 0; i < panel.Positions.Length; i++)
            {
                for (int j = 0; j < panel.Positions.Length; j++)
                {
                    if (panel.Positions[i][j] == 棋子.无)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public void ChangeTurn()
        {
            if (turn == 棋子.无)
            {
                turn = 棋子.黑子;
            }
            else if (turn == 棋子.黑子)
            {
                turn = 棋子.白子;
            }
            else if (turn == 棋子.白子)
            {
                turn = 棋子.黑子;
            }
        }
        private void _MakeStep(棋子 side, int x, int y)
        {
            if (panel.Has(x, y))
            {
                return;
            }

            panel.MakeStep(x, y, side);
        }

        public void Finish(棋子 side)
        {
            turn = 棋子.无;
            if(side == 棋子.无)
            {
                Messager.Instance.SendMessageLater(MessageKey.Equal, null);
            }
            else
            {
                Messager.Instance.SendMessageLater(MessageKey.Finish, side);
            }
            
        }

        public bool Judge(int x, int y, 棋子 side)
        {
            if (JudgeLine(0, 1, x, y, side)) return true;
            if (JudgeLine(1, 0, x, y, side)) return true;
            if (JudgeLine(-1, 1, x, y, side)) return true;
            if (JudgeLine(1, 1, x, y, side)) return true;

            return false;
        }

        private bool JudgeLine(int deltaX, int deltaY, int x, int y, 棋子 side)
        {
            if (side == 棋子.无) return false;

            int x1 = x;
            int x2 = x;
            int y1 = y;
            int y2 = y;

            while (true)
            {
                x1 -= deltaX;
                y1 -= deltaY;
                if (!Utility.IsInChess(x1, y1))
                {
                    break;
                }
                if (panel.Positions[x1][y1] != side)
                {
                    break;
                }
            }
            while (true)
            {
                x2 += deltaX;
                y2 += deltaY;
                if (!Utility.IsInChess(x2, y2))
                {
                    break;
                }
                if (panel.Positions[x2][y2] != side)
                {
                    break;
                }
            }
            return Math.Max(Math.Abs(x1 - x2), Math.Abs(y1 - y2)) >= 6;
        }

        public void OnMessage(MessageKey name, object param)
        {
            if (black == null || white == null || panel == null)
            {
                return;
            }
            if (!this.black.IsHuman() && !this.white.IsHuman())
            {
                return;
            }

            switch (name)
            {
                case MessageKey.MouseDown:
                    {

                        int[] array = param as int[];
                        int x = array[0];
                        int y = array[1];
                        if (panel.Has(x, y))
                        {
                            break;
                        }
                        if (turn == 棋子.白子)
                        {
                            white.OnMouseClick(x, y);
                        }
                        else if (turn == 棋子.黑子)
                        {
                            black.OnMouseClick(x, y);
                        }
                    }
                    break;
                case MessageKey.FinishTurn:
                    {
                        object[] os = param as object[];
                        int x = (int)os[1];
                        int y = (int)os[2];
                        棋子 p = (棋子)os[0];
                        FinishTurn(p, x, y);
                    }
                    break;

            }
        }
    }



}
