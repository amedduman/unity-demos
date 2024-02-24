using UnityEngine;

namespace VisualVerse
{
    [NodeInfo("SomeNodes/Start",false, true, true)]
    public class StartNode : VV_NodeRuntime 
    {
        public override void Execute()
        {
            Debug.Log("start node executed");
        }
    }
}