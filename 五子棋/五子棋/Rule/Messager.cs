using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace 五子棋
{
    public class Messager : IListened
    {
        private static Messager instance = new Messager();
        private bool _isDealingMessage = false;
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
        private Queue<KeyValuePair<MessageKey, object>> stack = new Queue<KeyValuePair<MessageKey, object>>();
        private Queue<KeyValuePair<MessageKey, IListener>> unregisterQueue = new Queue<KeyValuePair<MessageKey, IListener>>();
        public void SendMessageLater(MessageKey name, object param)
        {
            if(!_isDealingMessage)
            {
                SendMessage(name, param);
            }
            else
            {
                stack.Enqueue(new KeyValuePair<MessageKey, object>(name, param));
            }
            
        }
        public void SendMessage(MessageKey name, object param)
        {
            _isDealingMessage = true;
            if (listeners.ContainsKey(name))
            {
                List<IListener> ls = listeners[name];
                if (ls != null)
                {
                    foreach (IListener listener in ls)
                    {
                        listener.OnMessage(name, param);
                    }
                }
            }
            while(stack.Count > 0)
            {
                KeyValuePair<MessageKey, object> pair = stack.Dequeue();
                SendMessage(pair.Key, pair.Value);
            }
            while (unregisterQueue.Count > 0)
            {
                KeyValuePair<MessageKey, IListener> pair = unregisterQueue.Dequeue();
                List<IListener> ls = listeners[pair.Key];
                ls.Remove(pair.Value);
            }

            _isDealingMessage = false;
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
            if(!_isDealingMessage)
            {
                if (listeners.ContainsKey(name))
                {
                    List<IListener> ls = null;
                    ls = listeners[name];
                    ls.Remove(listener);
                }
            }
            else
            {
                unregisterQueue.Enqueue(new KeyValuePair<MessageKey, IListener>(name, listener));
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
        //ChangeTurn,
        Finish = 2,
        MouseDown,
        //MakeStep,
        //NextTurn,
        Restart = 6,
        FinishTurn,
        RefreshDebug,
        Equal,
        FinishLeague,
    }
}
