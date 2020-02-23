using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 五子棋
{
    public class Recorder : IListener
    {
        private string _path = "";
        private string _name = "";
        private StreamWriter _writer = null;
        private Stream _stream = null;

        private void Register()
        {
            Messager.Instance.Register(MessageKey.Finish, this);
            Messager.Instance.Register(MessageKey.Equal, this);
            Messager.Instance.Register(MessageKey.FinishTurn, this);
            Messager.Instance.Register(MessageKey.Restart, this);
        }

        private void UnRegister()
        {
            Messager.Instance.UnRegister(MessageKey.Finish, this);
            Messager.Instance.UnRegister(MessageKey.Equal, this);
            Messager.Instance.UnRegister(MessageKey.FinishTurn, this);
            Messager.Instance.UnRegister(MessageKey.Restart, this);
        }

        public Recorder()
        {
            Register();
        }
        private void Initialize(string blackName, string whiteName)
        {
            string path = "";
            _path = "record/" + blackName + "_" + whiteName;
            DateTime now = DateTime.Now;
            _name = now.Ticks.ToString();
            if (!Directory.Exists(_path))
            {
                Directory.CreateDirectory(_path);
            }
            path = Path.Combine(_path, _name);
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            _stream = File.Create(path);
            _writer = new StreamWriter(_stream);
            _writer.WriteLine(now.ToString());
            _writer.WriteLine(blackName);
            _writer.WriteLine(whiteName);
        }
        private void Record(棋子 side, int x, int y)
        {
            if(_writer != null)
            {
                _writer.WriteLine("{0} {1} {2}", (int)side, x, y);
            }
        }

        private void Finish(棋子 side)
        {
            if(_writer != null)
            {
                _writer.Write((int)side);
                _writer.Close();
                _stream.Close();
                _writer = null;
            }
        }

        public void OnMessage(MessageKey name, object param)
        {
            switch(name)
            {
                case MessageKey.Restart:
                    ChessPlayer[] players = param as ChessPlayer[];
                    Initialize(players[0].Name, players[1].Name);
                    break;
                case MessageKey.FinishTurn:
                    object[] os = param as object[];
                    int x = (int)os[1];
                    int y = (int)os[2];
                    棋子 side = (棋子)os[0];
                    Record(side, x, y);
                    break;
                case MessageKey.Finish:
                    棋子 s = (棋子)param;
                    Finish(s);
                    break;
                case MessageKey.Equal:
                    Finish(棋子.无);
                    break;
            }
        }
    }
}
