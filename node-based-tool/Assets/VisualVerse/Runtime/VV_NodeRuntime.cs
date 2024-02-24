using UnityEngine;

namespace VisualVerse
{
    [System.Serializable]
    public class VV_NodeRuntime
    {
        public bool isStartNode { get; set; }
        public int order = -1;
        public string title;
        public Rect rect;
        public string editorNodeGuid;
        
        public string FollowFlow()
        {
            Execute();
            return "";
        }

        public virtual void Execute()
        {
            
        }
    }
}