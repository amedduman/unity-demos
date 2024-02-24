using UnityEngine;

namespace Fun
{
    [NodeInfo("SomeNodes/Start",false, true, true)]
    public class StartNodeData : NodeData 
    {
        public override void Execute()
        {
            // Debug.Log("start node called");
        }
    }
}