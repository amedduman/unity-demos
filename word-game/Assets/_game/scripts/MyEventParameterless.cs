using System;
using System.Collections.Generic;
using System.Linq;

namespace WordGame
{
    public class MyEventParameterless
    {
        List<( Action, int)> listeners = new List<(Action, int)>();
        
        public void AddListener(Action listener, int order = 0)
        {
            listeners.Add((listener, order));
            
            listeners = listeners.OrderBy(i => i.Item2).ToList();
        }

        public void RemoveListener(Action listenerToRemove)
        {
            int order = 0;
            foreach (var listener in listeners)
            {
                if (listener.Item1 == listenerToRemove)
                {
                    order = listener.Item2;
                }
            }

            listeners.Remove((listenerToRemove, order));
        }

        public void Invoke()
        {
            foreach (var listener in listeners)
            {
                listener.Item1();
            }
        }
    }
}