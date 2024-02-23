namespace Fun
{
    [NodeInfo("SomeNodes/Sum")]
    public class SumNode : NodeData
    {
        [ExposedField] public int a;
        [ExposedField] public int b;

        [ExposedField(false)] public int sum;
    }
}