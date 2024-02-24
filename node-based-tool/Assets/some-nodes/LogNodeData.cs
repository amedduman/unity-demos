using UnityEngine;

namespace Fun
{
    [NodeInfo("SomeNodes/Log")]
    public class LogNodeData : NodeData
    {
        [ExposedField]
        public string message;
        
        public override void Execute()
        {
            Debug.Log("log node called");
        }
    }
}
