using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Fun
{
    public sealed class NodeEditor : Node
    {
        readonly NodeData nodeData;
        readonly Port exitPort;
        BaseGraph graph;

        public NodeEditor(NodeData nodeData)
        {
            this.nodeData = nodeData;

            this.nodeData.editorNodeGuid = viewDataKey;
            // this.nodeData.OnGraphSave = SetConnections;
            
            Type t = nodeData.GetType();
            var info = t.GetCustomAttribute<NodeInfoAttribute>();

            if (info.isStartNode) nodeData.isStartNode = true;
            
            if (info.hasEnter)
            {
                var p = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(ExecutionPort));
                p.portName = "enter";
                inputContainer.Add(p);

                RefreshPorts(); 
                RefreshExpandedState();
            }
            if (info.hasExit)
            {
                var p = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(ExecutionPort));
                p.portName = "exit";
                outputContainer.Add(p);

                exitPort = p;
                
                RefreshPorts(); 
                RefreshExpandedState();
            }

            foreach (var field in t.GetFields())
            {
                var att = field.GetCustomAttribute<ExposedFieldAttribute>();
                
                if (att == null) continue;
                
                var p = InstantiatePort(Orientation.Horizontal, att.direction , Port.Capacity.Single, field.FieldType);
                p.portName = field.Name;
                    
                switch (att.direction)
                {
                    case Direction.Input:
                        inputContainer.Add(p);
                        break;
                    case Direction.Output:
                        outputContainer.Add(p);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                
                RefreshPorts(); 
                RefreshExpandedState();
            }

            SetNode();
        }

        void SetNode()
        {
            SetPosition(nodeData.rect);
            title = nodeData.title;
        }

        // public void Update()
        // {
        //     // if (exitPort.connections.Count() > 1)
        //     // {
        //     //     Debug.LogError("the exit port capacity should be single");
        //     //     return;
        //     // }
        //
        //     // foreach (var connection in exitPort.connections)
        //     // {
        //     //     foreach (var VARIABLE in graph.)
        //     //     {
        //     //         
        //     //     }
        //     //     connection.output.node.viewDataKey
        //     //     nodeData.connectedNode = connection.output.node;
        //     // }
        //     
        // }

        // void SetConnections()
        // {
        //     
        // }
    }
}
