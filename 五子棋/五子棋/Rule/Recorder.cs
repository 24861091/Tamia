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

        public void Register()
        {
            Messager.Instance.Register(MessageKey.ChangeTurn, this);
            Messager.Instance.Register(MessageKey.Finish, this);
            Messager.Instance.Register(MessageKey.MakeStep, this);
            Messager.Instance.Register(MessageKey.Restart, this);
        }

        public void UnRegister()
        {
            Messager.Instance.UnRegister(MessageKey.ChangeTurn, this);
            Messager.Instance.UnRegister(MessageKey.Finish, this);
            Messager.Instance.UnRegister(MessageKey.MakeStep, this);
            Messager.Instance.UnRegister(MessageKey.Restart, this);
        }

        public Recorder(string path, string blackName, string whiteName)
        {
            path = Path.GetDirectoryName(path);
            _path = path;
            DateTime now = DateTime.Now;
            _name = now.ToString();
            if (!Directory.Exists(_path))
            {
                Directory.CreateDirectory(_path);
            }
            path = Path.Combine(_path, _name);
            if(File.Exists(path))
            {
                File.Delete(path);
            }

            FileStream stream = File.Create(path);
            _writer = new StreamWriter(stream);
            _writer.WriteLine(now.ToString());
            _writer.WriteLine(blackName);
            _writer.WriteLine(whiteName);

            Register();
        }

        public void Record(棋子 side, int x, int y)
        {
            if(_writer != null)
            {
                _writer.WriteLine("{0} {1} {2}", (int)side, x, y);
            }
        }

        public void Finish()
        {
            _writer.Close();
            _stream.Close();
            UnRegister();
        }

        public void OnMessage(MessageKey name, object param)
        {
            switch(name)
            {
                case MessageKey.Restart:

                    break;
                case MessageKey.ChangeTurn:
                    break;
                case MessageKey.NextTurn:
                    break;
                case MessageKey.MakeStep:
                    break;
                case MessageKey.FinishTurn:
                    棋子 side = (棋子)(param);
                    break;
                case MessageKey.Finish:
                    break;
            }
        }
    }
}
