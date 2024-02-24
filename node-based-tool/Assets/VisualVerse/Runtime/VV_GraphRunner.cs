using UnityEngine;

namespace VisualVerse
{
    public class VV_GraphRunner : MonoBehaviour
    {
        [SerializeField] VV_GraphData graph;

        void Start()
        {
            for (var i = 0; i < graph.vvNodes.Count; i++)
            {
                VV_NodeRuntime node = graph.vvNodes[i];
                if (i == 0)
                {
                    node.Execute(null);
                }
                else
                {
                    node.Execute(graph.vvNodes[i - 1]);
                }
            }
        }
    }
}
