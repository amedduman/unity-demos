using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;

namespace UI_Animator
{
    public static class GraphSaveUtility
    {
        public static void Save(GraphDataContainerSo containerSo, List<Node> nodes)
        {
            // var savedNodeDataList = nodes.Select(node => node as ISavedNodeData).ToList();
            WriteData(containerSo, nodes);
            // WriteData(containerSo, savedNodeDataList);
        }

        // static void WriteData(GraphDataContainerSo container, List<ISavedNodeData> nodeDataList)
        // {
        //     container.nodeGuids.Clear();
        //
        //     foreach (var node in nodeDataList)
        //     {
        //         container.nodeGuids.Add(node.guid.ToString());
        //     }
        // }
        
        static void WriteData(GraphDataContainerSo container, List<Node> nodes)
        {
            container.ResetData();

            foreach (var node in nodes)
            {
                container.keys.Add(node.viewDataKey);
                container.rects.Add(node.GetPosition());
            }
        }
    }
}
