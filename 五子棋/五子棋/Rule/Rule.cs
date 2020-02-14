using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 五子棋
{
    class Rule : IListener
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
            Messager.Instance.Register(MessageKey.NextTurn, this);
            Messager.Instance.Register(MessageKey.MakeStep, this);
            Messager.Instance.Register(MessageKey.FinishTurn, this);
        }
        public void SetChessPlayers(ChessPlayer black, ChessPlayer white)
        {
            this.black = black;
            this.black.SetSide(棋子.黑子);
            this.black.GameStart(panel.Positions);
            this.black.Name = this.black.GetType().ToString();
            this.white = white;
            this.white.SetSide(棋子.白子);
            this.white.GameStart(panel.Positions);
            this.white.Name = this.white.GetType().ToString();
        }
        public void SetBlack(ChessPlayer player)
        {
            black = player;
        }
        public void SetWhite(ChessPlayer player)
        {
            white = player;
        }
        public 棋子 GetTurn()
        {
            return turn;
        }
        public void OnYourTurn()
        {
            if (turn == 棋子.黑子)
            {
                ChessPannel panel = this.GetPanel();
                black.OnYourTurn(panel.Positions, panel.BlackList,panel.WhiteList);
            }
            else if (turn == 棋子.白子)
            {
                ChessPannel panel = this.GetPanel();
                white.OnYourTurn(panel.Positions, panel.BlackList, panel.WhiteList);
            }
            
        }
        public void FinishTurn(棋子 side, int x, int y)
        {
            if (Judge(x, y, side))
            {
                Finish(side);
            }
            else
            {
                if (turn != 棋子.无)
                {
                    Messager.Instance.SendMessage(MessageKey.NextTurn, null);
                }
            }
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

            Messager.Instance.SendMessage(MessageKey.ChangeTurn, turn);
        }
        public void MakeStep(棋子 side, int x, int y)
        {
            Messager.Instance.SendMessage(MessageKey.MakeStep, new object[] { side, x, y });
        }
        private void _MakeStep(棋子 side, int x, int y)
        {
            if (panel.Has(x, y))
            {
                //MessageBox.Show(string.Format("{2} 方输 ，({0},{1})处已经有子,或者出界了，将重新开始", x, y, turn));
                //Messager.Instance.SendMessage(MessageKey.Restart, null);
                return;
            }

            panel.MakeStep(x, y, side);
            //FinishTurn(side, x, y);
        }

        public void Finish(棋子 side)
        {
            turn = 棋子.无;
            Messager.Instance.SendMessage(MessageKey.Finish, side);
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

            int num = 1;
            bool left = true;
            bool right = true;
            for(int i = 1; i <= 4; i ++)
            {
                if (left)
                {
                    if (panel.GetSide(x - deltaX * i, y - deltaY * i) == side)
                        num++;
                }
                if(right)
                {
                    if (panel.GetSide(x + deltaX * i, y + deltaY * i) == side)
                        num++;
                }
                if (!left && !right)
                {
                    break;
                }
            }
            if (num >= 5) return true;
            return false;
        }

        public void OnMessage(MessageKey name, object param)
        {
            switch (name)
            {
                case MessageKey.MouseDown:
                    {
                        
                        int[] array = param as int[];
                        int x = array[0];
                        int y = array[1];
                        if (panel.Has(x, y))
                        {
                            //MessageBox.Show(string.Format("{2} 方输 ，({0},{1})处已经有子,或者出界了，将重新开始", x, y, turn));
                            //Messager.Instance.SendMessage(MessageKey.Restart, null);
                            break;
                        }

                        if (turn == 棋子.白子)
                        {
                            white.OnMouseClick(x, y);
                        }
                        else if(turn == 棋子.黑子)
                        {
                            black.OnMouseClick(x, y);
                        }
                    }
                    break;
                case MessageKey.MakeStep:
                    {
                        object[] os = param as object[];
                        int x = (int)os[1];
                        int y = (int)os[2];
                        棋子 p = (棋子)os[0];
                        _MakeStep(p, x, y);
                    }
                    break;
                case MessageKey.NextTurn:
                    {
                        ChangeTurn();
                        OnYourTurn();
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
