using UnityEngine;

namespace VisualVerse
{
    public class VV_GraphRunner : MonoBehaviour
    {
        [SerializeField] VV_GraphData graph;

        void Start()
        {
            Debug.Log(graph.vvNodes.Count);
            foreach (var node in graph.vvNodes)
            {
                node.Execute();
                // if (nodeData.isStartNode)
                    // nodeData.FollowFlow();
            }
        }
    }
}
