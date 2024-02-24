using UnityEngine;

namespace VisualVerse
{
    public class VV_GraphRunner : MonoBehaviour
    {
        VV_GraphData graph;

        void Start()
        {
            foreach (VV_NodeRuntime nodeData in graph.nodeDataList)
            {
                if (nodeData.isStartNode)
                    nodeData.FollowFlow();
            }
        }
    }
}
