using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WordGame
{
    public class MyAwaitableEvent<T>
    {
        List<( Func<T, Coroutine>, int)> listeners = new List<(Func<T, Coroutine>, int)>();
        
        public void AddListener(Func<T, Coroutine> listener, int order = 0)
        {
            listeners.Add((listener, order));
            
            listeners = listeners.OrderBy(i => i.Item2).ToList();
        }

        public void RemoveListener(Func<T, Coroutine> listenerToRemove)
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
            CoRunner.instance.StartCoroutine(CoInvoke());
            
            IEnumerator CoInvoke()
            {
                foreach (var listener in listeners)
                {
                    yield return listener.Item1(data);
                }
            }
        }
    }
}