using UnityEditor;
using UnityEditor.Experimental.GraphView;

namespace VisualVerse
{
    public static class GraphSaveUtility
    {
        public static void Save(VV_GraphData containerSo, VV_Graph graphView)
        {
            // containerSo.ResetData();
            //
            // foreach (var node in graphView.nodes)
            // {
            //     containerSo.nodes.Add(new NodeDataToSave(node.viewDataKey, node.GetPosition()));
            // }
            //
            // foreach (Edge edge in graphView.edges)
            // {
            //     var input = edge.input.viewDataKey;
            //     var output = edge.output.viewDataKey;
            //     containerSo.edges.Add(new EdgeData(input, output));
            // }
            //
            // EditorUtility.SetDirty(containerSo);
            // AssetDatabase.SaveAssets();
            // AssetDatabase.Refresh();
        }

        public static void Load(VV_GraphData containerSo, VV_Graph graphView)
        {
            // foreach (var node in containerSo.nodes)
            // {
            //     graphView.CreateNode("load node", node.rect, node.guid);
            // }
        }
    }
}
