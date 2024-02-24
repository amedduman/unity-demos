using System;
using System.Reflection;
using UnityEditor.Experimental.GraphView;

namespace VisualVerse
{
    public sealed class VV_NodeEditor : Node
    {
        readonly VV_NodeRuntime vNode;

        public VV_NodeEditor(VV_NodeRuntime n)
        {
            vNode = n;

            vNode.editorNodeGuid = viewDataKey;
            
            Type t = vNode.GetType();
            var info = t.GetCustomAttribute<NodeInfoAttribute>();

            if (info.isStartNode) vNode.isStartNode = true;
            
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
            SetPosition(vNode.rect);
            title = vNode.title;
        }
    }
}
