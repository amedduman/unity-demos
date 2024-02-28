using System;
using UnityEngine;

namespace WordGame
{
    public class CoRunner : MonoBehaviour
    {
        public static CoRunner instance { get; private set; }

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Debug.LogError("two CoRunner in the scene");
            }
        }
    }
}