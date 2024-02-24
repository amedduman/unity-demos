using UnityEngine;

namespace VisualVerse
{
    [NodeInfo("SomeNodes/Log")]
    public class LogNode : VV_NodeRuntime
    {
        [ExposedField]
        public string message;
        
        public override void Execute()
        {
            Debug.Log("log node executed");
        }
    }
}
