namespace VisualVerse
{
    [NodeInfo("SomeNodes/Log")]
    public class LogNodeData : VV_NodeRuntime
    {
        [ExposedField]
        public string message;
        
        public override void Execute()
        {
            // Debug.Log("log node called");
        }
    }
}
