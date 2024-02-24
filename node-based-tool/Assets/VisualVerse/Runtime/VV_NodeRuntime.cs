using UnityEngine;

namespace VisualVerse
{
    [System.Serializable]
    public abstract class VV_NodeRuntime
    {
        public string title;
        public int order = -1;
        public bool isStartNode { get; set; }
        public Rect rect { get; set; }
        public string editorNodeGuid;
        
        public string FollowFlow()
        {
            Execute();
            return "";
        }

        public abstract void Execute();
    }
}