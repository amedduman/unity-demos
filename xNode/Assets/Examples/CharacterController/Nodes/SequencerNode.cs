using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using XNode;

public class SequencerNode : Node, IExecutableNode
{
    [Output] public IExecutableNode _1;
    [Output] public IExecutableNode _2;

    public bool DoesExecutionEnd { get; set; }

    public async void Execute()
    {
        var tasks = new List<IExecutableNode>();

        tasks.Add(_1);
        tasks.Add(_2);
        while (true)
        {
            foreach (var task in tasks)
            {
                task.Execute();
            }

            await UniTask.Yield();
        }
    }
}