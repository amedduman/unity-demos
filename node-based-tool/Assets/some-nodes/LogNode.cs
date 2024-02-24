using System;
using UnityEngine;

namespace VisualVerse
{
    [NodeInfo("SomeNodes/Log")]
    public class LogNode : VV_NodeRuntime
    {
        [ExposedField]
        public string message;

        [ExposedField] public int intMessage;
        
        public override void Execute(VV_NodeRuntime previousNode)
        {
            intMessage = (int)previousNode.GetValue();
            Debug.Log("log node's message is: " +  intMessage);
        }

        public override object GetValue()
        {
            return null;
        }
    }
}
