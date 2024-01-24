using UnityEngine;
using XNode;

public class GraphOwner : MonoBehaviour
{
    [SerializeField] NodeGraph _graph;
    [SerializeField] BlackBoard _bb;

    void Start()
    {
        foreach (var node in _graph.nodes)
        {
            node.graph.BlackBoard = _bb;
            if (node is IStartNode)
            {
                var n = node as IExecutableNode;
                n.Execute();
            }
        }
    }
}