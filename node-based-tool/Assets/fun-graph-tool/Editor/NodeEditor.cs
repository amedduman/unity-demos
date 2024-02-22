using System;
using System.Reflection;
using UnityEditor.Experimental.GraphView;

namespace Fun
{
    public sealed class NodeEditor : Node
    {
        readonly NodeData nodeData;
        
        public NodeEditor(NodeData nodeData)
        {
            this.nodeData = nodeData;

            Type t = nodeData.GetType();
            var info = t.GetCustomAttribute<NodeInfoAttribute>();

            if (info.hasEnter)
            {
                var p = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(float));
                p.portName = "enter";
                inputContainer.Add(p);
                
                RefreshPorts(); 
                RefreshExpandedState();
            }
            if (info.hasExit)
            {
                var p = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(float));
                p.portName = "exit";
                outputContainer.Add(p);
                
                RefreshPorts(); 
                RefreshExpandedState();
            }

            SetNode();
            SetPorts();
        }

        void SetNode()
        {
            SetPosition(nodeData.rect);
            title = nodeData.title;
        }

        void SetPorts()
        {
            
        }
    }
}
