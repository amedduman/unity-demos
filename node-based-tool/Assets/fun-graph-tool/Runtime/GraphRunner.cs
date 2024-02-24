using UnityEngine;

namespace Fun
{
    public class GraphRunner : MonoBehaviour
    {
        GraphDataContainerSo graph;

        void Start()
        {
            foreach (NodeData nodeData in graph.nodeDataList)
            {
                if (nodeData.isStartNode)
                    nodeData.FollowFlow();
            }
        }
    }
}
