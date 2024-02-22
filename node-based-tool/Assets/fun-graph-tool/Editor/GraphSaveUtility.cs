using UnityEditor;
using UnityEditor.Experimental.GraphView;

namespace Fun
{
    public static class GraphSaveUtility
    {
        public static void Save(GraphDataContainerSo containerSo, BaseGraph graphView)
        {
            containerSo.ResetData();

            foreach (var node in graphView.nodes)
            {
                containerSo.nodes.Add(new NodeDataToSave(node.viewDataKey, node.GetPosition()));
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

        public static void Load(GraphDataContainerSo containerSo, BaseGraph graphView)
        {
            foreach (var node in containerSo.nodes)
            {
                graphView.CreateNode("load node", node.rect, node.guid);
            }
        }
    }
}
