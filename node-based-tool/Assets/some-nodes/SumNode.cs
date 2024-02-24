namespace VisualVerse
{
    [NodeInfo("SomeNodes/Sum")]
    public class SumNode : VV_NodeRuntime
    {
        [ExposedField] public int a;
        [ExposedField] public int b;

        [ExposedField(false)] public int sum;

        public override void Execute(VV_NodeRuntime previousNode)
        {
            sum = a + b;
        }

        public override object GetValue()
        {
            return sum;
        }
    }
}