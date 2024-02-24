using UnityEngine;

namespace Fun
{
    [System.Serializable]
    public class NodeData
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