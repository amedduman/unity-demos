using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CasualPuzzle
{
    public class EventSO<T> : ScriptableObject
    {
        List<( Action<T>, int)> listeners = new List<(Action<T>, int)>();
        
        [field: SerializeField] bool DoWaitForListeners { get; set; } = false;

        public void AddListener(Action<T> listener, int order = 0)
        {
            listeners.Add((listener, order));
            
            listeners = listeners.OrderBy(i => i.Item2).ToList();
        }

        public void RemoveListener(Action<T> listenerToRemove)
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

        public void Invoke(T data)
        {
            foreach (var listener in listeners)
            {
                listener.Item1(data);
            }
        }
    }
}
