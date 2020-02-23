using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 五子棋.AI;

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

        public DNA[] StartLeague(int generation, int times, int topNum)
        {
            Logger.Log("StartLeague 1");
            string path = Utility.CreateTargetPath(generation);
            Logger.Log("StartLeague 2");
            League.Instance.Initialize(path, times);
            Logger.Log("StartLeague 3");
            League.Instance.Do();
            Logger.Log("StartLeague 4");
            bool can = true;

            while(can)
            {
                Logger.Log("StartLeague 5");
                ChessPlayer black = League.Instance.GetBlack();
                ChessPlayer white = League.Instance.GetWhite();
                Logger.Log("StartLeague 6");
                _rule.Clear();
                _rule.SetChessPlayers(black, white);
                Logger.Log("StartLeague 7");
                ChessMove move = StartOneChess();
                Logger.Log("StartLeague 8");
                can = League.Instance.Finish(move.Side);
                Logger.Log("StartLeague 9");
            }
            Logger.Log("StartLeague 10");
            Messager.Instance.SendMessageLater(MessageKey.FinishLeague, null);
            LeagueResult[][] result = League.Instance.Performance;
            List<DNAPlayer> players = League.Instance.GetPlayers();
            Logger.Log("StartLeague 11");
            return Calculate(result, players, topNum);
        }

        private DNA[] Calculate(LeagueResult[][] result, List<DNAPlayer> players, int topNum)
        {
            Logger.Log("Calculate 1");
            if (result == null || result.Length <= 0)
            {
                return null;
            }
            Logger.Log("Calculate 2");
            LinkedList<KeyValuePair<int,int>> list = new LinkedList<KeyValuePair<int, int>>();
            for(int i = 0;  i < result.Length; i ++)
            {
                Logger.Log("Calculate 3");
                KeyValuePair<int, int> pair = new KeyValuePair<int, int>();
                for (int j = 0; j < result.Length; j ++)
                {
                    if(i != j)
                    {
                        pair = new KeyValuePair<int, int>(i, pair.Value + result[i][j].Win + result[j][i].Win);
                    }
                }
                LinkedListNode<KeyValuePair<int, int>> linkNode = list.First;
                Logger.Log("Calculate 4");
                while (true)
                {
                    Logger.Log("Calculate 5");
                    if (linkNode == null)
                    {
                        list.AddLast(pair);
                        break;
                    }
                    if (pair.Value > linkNode.Value.Value)
                    {
                        list.AddBefore(linkNode, pair);
                        break;
                    }
                    linkNode = linkNode.Next;
                }

            }
            Logger.Log("Calculate 6");
            int n = Math.Min(topNum, list.Count);
            List<DNAPlayer> ps = new List<DNAPlayer>();
            LinkedListNode<KeyValuePair<int, int>> node = list.First;
            Logger.Log("Calculate 7");
            while (n > 0 && node != null)
            {
                ps.Add(players[node.Value.Key]);
                node = node.Next;
                n--;
            }
            DNA[] dnas = new DNA[ps.Count];
            for(int i = 0; i < ps.Count; i ++)
            {
                dnas[i] = ps[i].GetDNA();
            }
            Logger.Log("Calculate 8");
            return dnas;
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
