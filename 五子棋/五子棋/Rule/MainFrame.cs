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

        public MainFrame()
        {
            Initialize();
        }
        public void Initialize()
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
        public void Restart(string black, string white)
        {
            _rule.Clear();
            _rule.SetChessPlayers(CreatePlayer(black), CreatePlayer(white));
            _rule.ChangeTurn();
            _rule.OnYourTurn();
            Messager.Instance.SendMessageLater(MessageKey.Restart, new string[] { black, white });
        }
        public void Restart(ChessPlayer black, ChessPlayer white)
        {
            _rule.Clear();
            _rule.SetChessPlayers(black, white);
            _rule.ChangeTurn();
            _rule.OnYourTurn();
            Messager.Instance.SendMessageLater(MessageKey.Restart, new string[] { black.GetType().Name, white.GetType().Name });
        }

        private ChessPlayer CreatePlayer(string typeName)
        {
            string[] s = typeName.Split('_');
            if (s != null)
            {
                Type type = Type.GetType("五子棋.AI." + s[0]);
                if (type != null)
                {
                    ChessPlayer player = Activator.CreateInstance(type) as ChessPlayer;
                    if (s.Length == 2)
                    {
                        player.Name = s[1];
                    }
                    else
                    {
                        player.Name = s[0];
                    }

                    return player;
                }

            }
            return null;

        }

    }
}
