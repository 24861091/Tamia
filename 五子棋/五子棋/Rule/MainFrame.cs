using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 五子棋
{
    public class MainFrame
    {
        private Rule _rule = null;
        private Recorder _recorder = null;
        private static MainFrame sInstance = new MainFrame();
        public static MainFrame Instance
        {
            get
            {
                return sInstance;
            }
        }
        private MainFrame()
        {
            Initialize();
        }
        private void Initialize()
        {
            _rule = new Rule();
            _rule.Initialize(Utility.sizeX, Utility.sizeY);
            _recorder = new Recorder();
        }
        public 棋子[][] Chess
        {
            get
            {
                return _rule.GetPanel().positions;
            }
        }

        public List<Position> Mark
        {
            get
            {
                return _rule.GetPanel().Mark;
            }
        }
        public void Restart(ChessPlayer black, ChessPlayer white)
        {
            _rule.Clear();
            _rule.SetChessPlayers(black, white);
            Messager.Instance.SendMessageLater(MessageKey.Restart, new ChessPlayer[] { black, white });
            _rule.Restart();
        }

        public void StartLeague(string path, int times)
        {
            League.Instance.Initialize(path, times);
            League.Instance.Do();
            bool can = true;

            while(can)
            {
                ChessPlayer black = League.Instance.GetBlack();
                ChessPlayer white = League.Instance.GetWhite();
                
                _rule.Clear();
                _rule.SetChessPlayers(black, white);
                //Messager.Instance.SendMessageLater(MessageKey.Restart, new ChessPlayer[] { black, white });

                ChessMove move = StartOneChess();
                can = League.Instance.Finish(move.Side);
            }
            Messager.Instance.SendMessageLater(MessageKey.FinishLeague, null);
        }

        private ChessMove StartOneChess()
        {
            棋子 side = 棋子.无;
            int x = 0;
            int y = 0;
            while (true)
            {
                _rule.ChangeTurn();
                _rule.OnYourTurn();

                if (_rule.GetTurn() == 棋子.白子)
                {
                    side = _rule.GetWhite().LastMove.Side;
                    x = _rule.GetWhite().LastMove.Position.X;
                    y = _rule.GetWhite().LastMove.Position.Y;
                }
                else if (_rule.GetTurn() == 棋子.黑子)
                {
                    side = _rule.GetBlack().LastMove.Side;
                    x = _rule.GetBlack().LastMove.Position.X;
                    y = _rule.GetBlack().LastMove.Position.Y;
                }
                else
                {
                    MessageBox.Show("Error！过程中出现错误！");
                    return new ChessMove(side, new Position(x,y));
                }
                _rule.MakeStep(side, x, y);
                if (_rule.Judge(x, y, side))
                {
                    _rule.Finish(side);
                    return new ChessMove(side, new Position(x, y));
                }
                else if (_rule.JudgeEqual())
                {
                    _rule.Finish(棋子.无);
                    return new ChessMove(棋子.无, new Position(x, y));
                }
            }
        }
    }
}
