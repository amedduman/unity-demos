using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace VisualVerse
{
    public class VV_GraphData : ScriptableObject
    {
        [SerializeReference] public List<VV_NodeRuntime> vvNodes = new List<VV_NodeRuntime>();
        // public List<NodeDataToSave> nodes = new List<NodeDataToSave>();
        // public List<EdgeData> edges = new List<EdgeData>();

        // [ContextMenu("resetData")]
        // public void ResetData()
        // {
            // nodes.Clear();
            // edges.Clear();
        // }
    }

    // [System.Serializable]
    // public struct NodeDataToSave
    // {
    //     [field:SerializeField] public string guid{get; private set;}
    //     [field:SerializeField] public Rect rect{get; private set;}
    //
    //     public NodeDataToSave(string guid, Rect rect)
    //     {
    //         this.guid = guid;
    //         this.rect = rect;
    //     }
    // }
    //
    // [System.Serializable]
    // public struct EdgeData
    // {
    //     [field:SerializeField] public string input { get; private set; }
    //     [field:SerializeField] public string output { get; private set; }
    //
    //     public EdgeData(string input, string output)
    //     {
    //         this.input = input;
    //         this.output = output;
    //     }
    // }
}