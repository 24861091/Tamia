using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 五子棋
{
    public class Messager : IListened
    {
        private static Messager instance = new Messager();
        private bool isBreak = false;
        public bool IsBreak
        {
            set
            {
                isBreak = value;
            }
        }
        public static Messager Instance
        {
            get
            {
                return instance;
            }
        }
        private Messager()
        {

        }
        private Dictionary<MessageKey, List<IListener>> listeners = new Dictionary<MessageKey, List<IListener>>();
        public void SendMessage(MessageKey name, object param)
        {
            if(listeners.ContainsKey(name))
            {
                List<IListener> ls = listeners[name];
                if(ls != null)
                {
                    foreach(IListener listener in ls)
                    {
                        if(isBreak)
                        {
                            isBreak = false;
                            break;
                        }
                        listener.OnMessage(name, param);
                    }
                }
            }
        }
        public void Register(MessageKey name, IListener listener)
        {
            List<IListener> ls = null;
            if(listeners.ContainsKey(name))
            {
                ls = listeners[name];
            }
            else
            {
                ls = new List<IListener>();
                listeners[name] = ls;
            }
            ls.Add(listener);
        }

        public void UnRegister(MessageKey name, IListener listener)
        {
            if (listeners.ContainsKey(name))
            {
                List<IListener> ls = null;
                ls = listeners[name];
                ls.Remove(listener);
            }
        }

    }

    public interface IListener
    {
        void OnMessage(MessageKey name,object param);
    }
    public interface IListened
    {
        void SendMessage(MessageKey name, object param);
    }

    public enum MessageKey
    {
        ChangeTurn,
        Finish,
        MouseDown,
        MakeStep,
        NextTurn,
        Restart,
        FinishTurn,
        RefreshDebug,
    }
}
