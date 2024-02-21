using System.Collections.Generic;
using UnityEngine;

namespace UI_Animator
{
    public class GraphDataContainerSo : ScriptableObject
    {
        public List<NodeData> nodes = new List<NodeData>();
        public List<EdgeData> edges = new List<EdgeData>();

        [ContextMenu("resetData")]
        public void ResetData()
        {
            nodes.Clear();
            edges.Clear();
        }
    }

    [System.Serializable]
    public struct NodeData
    {
        [field:SerializeField] public string guid{get; private set;}
        [field:SerializeField] public Rect rect{get; private set;}

        public NodeData(string guid, Rect rect)
        {
            this.guid = guid;
            this.rect = rect;
        }
    }
    
    [System.Serializable]
    public struct EdgeData
    {
        [field:SerializeField] public string input { get; private set; }
        [field:SerializeField] public string output { get; private set; }

        public EdgeData(string input, string output)
        {
            this.input = input;
            this.output = output;
        }
    }
}