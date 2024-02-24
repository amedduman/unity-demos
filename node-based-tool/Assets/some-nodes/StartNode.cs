using System.Collections.Generic;
using UnityEngine;

namespace VisualVerse
{
    [NodeInfo("SomeNodes/Start",false, true, true)]
    public class StartNode : VV_NodeRuntime 
    {
        public override void Execute(VV_NodeRuntime previousNode)
        {
            Debug.Log("start node executed");
        }

        public override object GetValue()
        {
            return null;
        }
    }
}