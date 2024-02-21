using System.Collections.Generic;
using UnityEngine;

namespace UI_Animator
{
    public class GraphDataContainerSo : ScriptableObject
    {
        // public List<string> nodeGuids = new List<string>();
        // public List<int> indexList = new List<int>();
        public List<string> keys = new List<string>();
        public List<Rect> rects = new List<Rect>();

        public void ResetData()
        {
            keys.Clear();
            rects.Clear();
        }
    }
}