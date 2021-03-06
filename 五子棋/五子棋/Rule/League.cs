using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 五子棋.AI;

namespace 五子棋
{
    public class League 
    {
        private string _folder = "";
        private List<DNAPlayer> _players = new List<DNAPlayer>();
        private int _times = 0;
        private LeagueResult[][] _performance = null;
        private Queue<KeyValuePair<int, int>> _queue = new Queue<KeyValuePair<int, int>>();
        private MainFrame _main = MainFrame.Instance;

        private KeyValuePair<int, int> _pair = new KeyValuePair<int, int>();
        //public Action<LeagueResult[][]> OnFinish = null;
        //public Action<棋子, int> OnFinishOne = null;

        private static League sInstace = new League();
        public static League Instance
        {
            get
            {
                return sInstace;
            }
        }
        public League()
        {

        }
        public int Times
        {
            get
            {
                return _times;
            }
        }
        public LeagueResult[][] Performance
        {
            get
            {
                return _performance;
            }
        }
        public List<DNAPlayer> GetPlayers()
        {
            return _players;
        }
        public void Clear()
        {
            _folder = "";
            _times = 0;
        }
        public KeyValuePair<int,int> CurrentPlayers
        {
            get
            {
                return _pair;
            }
        }
        public ChessPlayer GetBlack()
        {
            return _players[_pair.Key];
        }
        public ChessPlayer GetWhite()
        {
            return _players[_pair.Value];
        }

        public void Initialize(string folder, int times)
        {
            _players.Clear();
            _folder = folder;
            _times = times;

            if (Directory.Exists(_folder))
            {
                string[] files = Directory.GetFiles(_folder);
                if (files != null)
                {
                    for (int i = 0; i < files.Length; i++)
                    {
                        string file = files[i];
                        DNAPlayer player = Utility.CreateDNAPlayer(file);
                        _players.Add(player);
                    }
                }
            }
            if (_players.Count > 1)
            {
                if(_performance != null)
                {
                    if (_performance.Length != _players.Count && _performance[0].Length != _players.Count)
                    {
                        _performance = new LeagueResult[_players.Count][];
                        for (int i = 0; i < _players.Count; i++)
                        {
                            _performance[i] = new LeagueResult[_players.Count];
                            for (int j = 0; j < _players.Count; j++)
                            {
                                _performance[i][j] = new LeagueResult(0, 0, 0);
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < _players.Count; i++)
                        {
                            for (int j = 0; j < _players.Count; j++)
                            {
                                _performance[i][j] = new LeagueResult(0, 0, 0);
                            }
                        }
                    }
                }
                else
                {
                    _performance = new LeagueResult[_players.Count][];
                    for (int i = 0; i < _players.Count; i++)
                    {
                        _performance[i] = new LeagueResult[_players.Count];
                        for (int j = 0; j < _players.Count; j++)
                        {
                            _performance[i][j] = new LeagueResult(0, 0, 0);
                        }
                    }

                }

            }

        }
        public void Do()
        {
            for (int i = 0; i < _players.Count; i++)
            {
                for (int j = 0; j < _players.Count; j++)
                {
                    if (i == j)
                    {
                        continue;
                    }
                    _queue.Enqueue(new KeyValuePair<int, int>(i, j));
                }
            }
            if(_queue.Count > 0)
            {
                _pair = _queue.Dequeue();
            }
        }

        private LeagueResult RecordPerformance(棋子 side)
        {
            LeagueResult result = _performance[_pair.Key][_pair.Value];
            switch (side)
            {
                case 棋子.无:
                    result.Equal++;
                    break;
                case 棋子.白子:
                    result.Lose++;
                    break;
                case 棋子.黑子:
                    result.Win++;
                    break;
            }
            _performance[_pair.Key][_pair.Value] = result;
            return result;
        }

        public bool Finish(棋子 side)
        {
            LeagueResult result = RecordPerformance(side);
            if (result.Win + result.Lose + result.Equal < _times)
            {
                return true;
            }
            else if(_queue.Count > 0)
            {
                _pair = _queue.Dequeue();
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public struct LeagueResult
    {
        public int Win;
        public int Lose;
        public int Equal;
        public LeagueResult(int win, int lose, int equal)
        {
            Win = win;
            Lose = lose;
            Equal = equal;
        }
    }
}
