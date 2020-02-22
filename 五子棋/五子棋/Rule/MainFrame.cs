using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            //_rule.ChangeTurn();
            //_rule.OnYourTurn();
        }
    }
}
