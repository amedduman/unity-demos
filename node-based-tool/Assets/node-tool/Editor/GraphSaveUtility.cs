using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace UI_Animator
{
    public static class GraphSaveUtility
    {
        public static void Save(GraphDataContainerSo containerSo, List<Node> nodes)
        {
            // var dialogueContainerObject = ScriptableObject.CreateInstance<GraphDataContainerSo>();
            //
            // if (AssetDatabase.IsValidFolder("Assets/Resources") == false)
            //     AssetDatabase.CreateFolder("Assets", "Resources");
            //
            // Object loadedAsset = AssetDatabase.LoadAssetAtPath($"Assets/Resources/{fileName}.asset", typeof(GraphDataContainerSo));
            //
            // if (loadedAsset == null || AssetDatabase.Contains(loadedAsset) == false) 
            // {
            //     AssetDatabase.CreateAsset(dialogueContainerObject, $"Assets/Resources/{fileName}.asset");
            // }
            //
            // Object loadedAsset2 = AssetDatabase.LoadAssetAtPath($"Assets/Resources/{fileName}.asset", typeof(GraphDataContainerSo));

            List<ISavedNodeData> savedNodeDataList = new List<ISavedNodeData>();
            foreach (Node node in nodes)
            {
                savedNodeDataList.Add(node as ISavedNodeData);
            }
            
            WriteData(containerSo, savedNodeDataList);
        }

        static void WriteData(GraphDataContainerSo container, List<ISavedNodeData> nodeDataList)
        {
            container.nodeGuids.Clear();

            foreach (var node in nodeDataList)
            {
                container.nodeGuids.Add(node.guid.ToString());
            }
        }
    }
}
