using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 五子棋.AI;

namespace 五子棋.Rule
{
    public class League : IListener
    {
        private string _folder = "";
        private List<DNAPlayer> _players = new List<DNAPlayer>();
        private int _times = 0;
        private LeagueResult[][] _performance = null;
        private Queue<KeyValuePair<int, int>> _queue = new Queue<KeyValuePair<int, int>>();
        private MainFrame _main = MainFrame.Instance;

        public League(string folder, int times)
        {
            _folder = folder;
            _times = times;

            if (Directory.Exists(_folder))
            {
                string[] files = Directory.GetFiles(_folder);
                if(files != null)
                {
                    for(int i = 0; i < files.Length; i ++)
                    {
                        string file = files[i];
                        DNAPlayer player = Utility.CreateDNAPlayer(file);
                        _players.Add(player);
                    }
                }
            }
            if(_players.Count > 1)
            {
                _performance = new LeagueResult[_players.Count][];
                for(int i = 0;i < _players.Count; i ++)
                {
                    _performance[i] = new LeagueResult[_players.Count];
                    for(int j = 0; j < _players.Count; j ++)
                    {
                        _performance[i][j] = new LeagueResult(0, 0, 0);
                    }
                }
            }
        }
        public void Do()
        {
            Messager.Instance.Register(MessageKey.Restart, this);
            Messager.Instance.Register(MessageKey.FinishTurn, this);
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
            }
        }

        public void OnMessage(MessageKey name, object param)
        {
            switch(name)
            {
                case MessageKey.Restart:
                    break;
                case MessageKey.FinishTurn:
                    break;
                case MessageKey.Finish:
                    break;
                case MessageKey.Equal:
                    break;
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
