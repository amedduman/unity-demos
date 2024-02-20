using System;
using UnityEditor.Experimental.GraphView;

namespace UI_Animator
{
    public class UI_AnimatorNode : Node
    {
        public string guid;
        public string testText;
        public bool entry;

        public UI_AnimatorNode(Guid guid)
        {
            this.guid = guid.ToString();
        }
    }
}