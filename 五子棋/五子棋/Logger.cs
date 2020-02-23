using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 五子棋
{
    public static class Logger
    {
        private static int _time = 1;
        private static long _tick = 0;

        public static void Begin()
        {
            _tick = DateTime.Now.Ticks;
        }
        public static void Log(string mark)
        {
            //Debug.WriteLine("{2} : {0} Time: {1}", _time++, new TimeSpan(DateTime.Now.Ticks - _tick).TotalSeconds.ToString(), mark);
        }
    }
}
