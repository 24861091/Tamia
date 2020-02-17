using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 五子棋.Rule
{
    public class Recorder
    {
        private string _path = "";
        private string _name = "";
        private StreamWriter _writer = null;
        private Stream _stream = null;

        public Recorder(string path, string blackName, string whiteName)
        {
            path = Path.GetDirectoryName(path);
            _path = path;
            DateTime now = DateTime.Now;
            _name = now.Ticks.ToString();
            if(!Directory.Exists(_path))
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
        }

    }
}
