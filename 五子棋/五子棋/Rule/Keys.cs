using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 五子棋
{
    public class Keys
    {
        private static Keys sInstance = new Keys();
        public static Keys Instance
        {
            get
            {
                return sInstance;
            }
        }
        private Keys()
        {

        }

        private Dictionary<string, bool> startRecord = new Dictionary<string, bool>();
        private Dictionary<string,Dictionary<string, byte>> dics = new Dictionary<string, Dictionary<string, byte>>();

        public void Begin(string code)
        {
            startRecord[code] = true;
            if (!dics.ContainsKey(code))
            {
                dics[code] = new Dictionary<string, byte>();
            }
            else
            {
                dics[code].Clear();
            }
        }

        public void End(string code)
        {
            startRecord[code] = false;
        }

        public void Record(string key)
        {
            foreach(KeyValuePair<string, bool> pair in startRecord)
            {
                string code = pair.Key;
                if (startRecord[code])
                {
                    Dictionary<string, byte> dic = dics[code];
                    dic[key] = 0;
                }
            }
        }
        public string[] GetAllKeys(string code)
        {
            Dictionary<string, byte> dic = null;
            if (dics.ContainsKey(code))
            {
                dic = dics[code];
            }
            if (dic == null)
            {
                return null;
            }
            dic["selfFactor"] = 1;
            return dic.Keys.ToArray<string>();
        }
    }
}
