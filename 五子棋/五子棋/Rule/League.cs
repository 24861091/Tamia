using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 五子棋.AI;

namespace 五子棋
{
    public class League : IListener
    {
        private string _folder = "";
        private List<DNAPlayer> _players = new List<DNAPlayer>();
        private int _times = 0;
        private LeagueResult[][] _performance = null;
        private Queue<KeyValuePair<int, int>> _queue = new Queue<KeyValuePair<int, int>>();
        private MainFrame _main = MainFrame.Instance;

        private KeyValuePair<int, int> _pair = new KeyValuePair<int, int>();
        public Action<LeagueResult[][]> OnFinish = null;
        public Action<棋子, int> OnFinishOne = null;

        public League()
        {

        }
        public void Clear()
        {
            _folder = "";
            _times = 0;

        }
        public void Initialize(string folder, int times)
        {
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
                if(_performance != null && _performance.Length != _players.Count && _performance != null && _performance[0].Length != _players.Count)
                {

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
            Messager.Instance.Register(MessageKey.Restart, this);
            Messager.Instance.Register(MessageKey.Finish, this);
            Messager.Instance.Register(MessageKey.Equal, this);

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
                KeyValuePair<int, int> pair = _queue.Dequeue();
                _main.Restart(_players[pair.Key], _players[pair.Value]);
                _pair = pair;
            }
        }

        public void OnMessage(MessageKey name, object param)
        {
            棋子 side = 棋子.无;
            switch (name)
            {
                case MessageKey.Restart:
                    ChessPlayer[] players = param as ChessPlayer[];
                    Restart(players[0], players[1]);
                    break;
                case MessageKey.Finish:
                    side = (棋子)(param);
                    Finish(side);
                    break;
                case MessageKey.Equal:
                    Finish(棋子.无);
                    break;
            }
        }
        private void Restart(ChessPlayer black, ChessPlayer white)
        {

        }
        private void Finish(棋子 side)
        {
            LeagueResult result = _performance[_pair.Key][_pair.Value];
            switch(side)
            {
                case 棋子.无:
                    result.Equal ++;
                    break;
                case 棋子.白子:
                    result.Lose++;
                    break;
                case 棋子.黑子:
                    result.Win++;
                    break;
            }
            _performance[_pair.Key][_pair.Value] = result;
            if(OnFinishOne != null)
            {
                OnFinishOne(side, result.Win + result.Lose + result.Equal);
            }
            if(result.Win + result.Lose + result.Equal < _times)
            {
                _main.Restart(_players[_pair.Key], _players[_pair.Value]);
            }
            else if(_queue.Count > 0)
            {
                _pair = _queue.Dequeue();
                _main.Restart(_players[_pair.Key], _players[_pair.Value]);
            }
            else
            {
                Finish();
            }
        }
        private void Finish()
        {
            Messager.Instance.UnRegister(MessageKey.Restart, this);
            Messager.Instance.UnRegister(MessageKey.Finish, this);
            Messager.Instance.UnRegister(MessageKey.Equal, this);

            if (OnFinish != null)
            {
                OnFinish(_performance);
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
