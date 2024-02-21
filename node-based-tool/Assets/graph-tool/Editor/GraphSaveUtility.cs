using UnityEditor;
using UnityEditor.Experimental.GraphView;

namespace UI_Animator
{
    public static class GraphSaveUtility
    {
        public static void Save(GraphDataContainerSo containerSo, BaseGraphView graphView)
        {
            containerSo.ResetData();

            foreach (var node in graphView.nodes)
            {
                containerSo.nodes.Add(new NodeData(node.viewDataKey, node.GetPosition()));
            }

            foreach (Edge edge in graphView.edges)
            {
                var input = edge.input.viewDataKey;
                var output = edge.output.viewDataKey;
                containerSo.edges.Add(new EdgeData(input, output));
            }
            
            EditorUtility.SetDirty(containerSo);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        public static void Load(GraphDataContainerSo containerSo, BaseGraphView graphView)
        {
            foreach (var node in containerSo.nodes)
            {
                graphView.CreateNode("load node", node.rect, node.guid);
            }
        }
    }
}
