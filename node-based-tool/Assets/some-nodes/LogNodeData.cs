using UnityEngine;

namespace Fun
{
    [NodeInfo("SomeNodes/Log")]
    public class LogNodeData : NodeData
    {
        [ExposedField]
        public string message;
    }
}
