using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 五子棋.AI
{
    public class DNA
    {
        private Dictionary<string, float> _DNAValues = new Dictionary<string, float>();
        private DNAFile _file = null;
        private string _path = "default.xml";
        private string _name = "1";
        private int _generation = 1;
        private float _selfFactor = 1f;

        public DNA(string fileName, string name)
        {
            _path = fileName;
            _name = name;
            _file = new DNAFile();
            if(!Load())
            {
                InitDefaultValues();
            }
            Save();
        }

        public float GetValue(string key)
        {
            if(_DNAValues.ContainsKey(key))
            {
                return _DNAValues[key];
            }
            return 1f;
        }
        private List<List<string>> GenerateKeys()
        {
            List<List<string>> keys = new List<List<string>>();
            for (int i = 1; i <= 4; i++)
            {
                List<string> ks = new List<string>();
                keys.Add(ks);
                int space = 5 - i;
                int[] s = new int[space];

                for (int j = 0; j < space; j++)
                {
                    s[j] = 0;
                }
                int index = space - 1;
                while (true)
                {
                    bool shouldBreak = index < 0;
                    for (int n = 0; n < space; n++)
                    {
                        if (!shouldBreak)
                        {
                            break;
                        }
                        shouldBreak = shouldBreak && s[n] == 0;
                    }
                    if (shouldBreak)
                    {
                        break;
                    }
                    string key = Translate(s);
                    if(s[0] != 1)
                    {
                        ks.Add(key);
                    }
                    

                    index = space - 1;
                    while (index >= 0)
                    {
                        s[index]++;
                        if (s[index] == 2)
                        {
                            for (int m = index + 1; m <= space - 1; m++)
                            {
                                s[m] = 2;
                            }
                        }

                        if (s[index] / 3 > 0)
                        {
                            s[index] %= 3;
                            index--;
                        }
                        else
                        {
                            index = -1;
                        }
                    }
                }
            }
            return keys;
        }
        public void InitDefaultValues()
        {
            List<List<string>> keys = GenerateKeys();
            for(int i= 0; i < keys.Count; i ++)
            {
                List<string> ks = keys[i];
                for (int j = 0; j < ks.Count; j++)
                {
                    for (int m = 0; m < ks.Count; m++)
                    {
                        string r = Reverse(ks[j]);
                        string key = r + (5 - r.Length).ToString() + ks[m];
                        if(Utility.CalculateLength(key, 5 - r.Length) >= 5)
                        {
                            _DNAValues[key] = 5 - r.Length;
                        }
                        
                    }
                }
            }
            
        }
        private string Reverse(string source)
        {
            StringBuilder builder = new StringBuilder();
            if(string.IsNullOrEmpty(source))
            {
                return "";
            }
            for(int i = source.Length - 1; i >= 0; i --)
            {
                builder.Append(source[i]);
            }
            return builder.ToString();
        }
        private string Translate(int [] s)
        {
            StringBuilder builder = new StringBuilder();
            if(s != null && s.Length > 0)
            {
                for(int i = 0; i < s.Length; i ++)
                {
                    switch(s[i])
                    {
                        case 0:
                            builder.Append("e");
                            break;
                        case 2:
                            builder.Append("b");
                            break;
                        case 1:
                            builder.Append("s");
                            break;
                    }
                }
            }
            return builder.ToString();
        }
        public void Save()
        {
            foreach(KeyValuePair<string, float> pairs in _DNAValues)
            {
                _file.SetFloat(pairs.Key, pairs.Value);
            }
            _file.SetString("name", _name);
            _file.SetFloat("selfFactor", _selfFactor);
            _file.SetInt("generation", _generation);
            _file.Save(_path);
        }
        public bool Load()
        {
            if(_file.Load(_path))
            {
                foreach (KeyValuePair<string, float> pairs in _file.GetAllDatas())
                {
                    if(pairs.Key == "name" || pairs.Key == "selfFactor"|| pairs.Key == "generation")
                    {
                        continue;
                    }
                    _DNAValues[pairs.Key] = pairs.Value;
                }
                _name = _file.GetString("name", _name);
                _selfFactor = _file.GetFloat("selfFactor", _selfFactor);
                _generation = _file.GetInt("generation", _generation);
                return true;
            }
            return false;
        }
    }
}