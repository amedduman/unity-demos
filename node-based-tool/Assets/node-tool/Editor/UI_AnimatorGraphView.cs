using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI_Animator
{
    public class UI_AnimatorGraphView : GraphView
    {
        public UI_AnimatorGraphView()
        {
            SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());

            AddElement(CreateStartNode());
        }

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            var compatiblePorts = new List<Port>();

            foreach (Port port in ports)
            {
                if(startPort == port || startPort.node == port.node) continue;
                
                compatiblePorts.Add(port);
            }

            return compatiblePorts;
        }

        UI_AnimatorNode CreateStartNode()
        {
            // var node = new UI_AnimatorNode(Guid.NewGuid())
            var node = new UI_AnimatorNode()
            {
                title = "Start",
                entry = true,
                testText = "Test"
            };

            node.SetPosition(new Rect(100, 200, 100, 150));
            
            AddPort(node, Direction.Output, "next");
            
            return node;
        }

        public void CreateNode(string nodeTitle)
        {
            // var node = new UI_AnimatorNode(Guid.NewGuid())
            var node = new UI_AnimatorNode()
            {
                title = nodeTitle,
                testText = nodeTitle
            };
            
            node.SetPosition(new Rect(300, 200, 100, 150));

            var btn = new Button(() => { AddOutputPort(node);});
            btn.text = "Add";
            node.titleContainer.Add(btn);

            AddPort(node, Direction.Input, "input");
            
            AddElement(node);
        }

        void AddOutputPort(UI_AnimatorNode node)
        {
            var outputPortCount = node.outputContainer.Query("connector").ToList().Count;
            var portName = $"output {outputPortCount + 1}";
            AddPort(node, Direction.Output, portName);
        }

        Port AddPort(UI_AnimatorNode node, Direction portDirection, string portName, Port.Capacity capacity = Port.Capacity.Single)
        {
            var p = node.InstantiatePort(Orientation.Horizontal, portDirection, capacity, typeof(float));
            p.portName = portName;
            switch (portDirection)
            {
                case Direction.Input:
                    node.inputContainer.Add(p);
                    break;
                case Direction.Output:
                    node.outputContainer.Add(p);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(portDirection), portDirection, null);
            }
            
            node.RefreshPorts();
            node.RefreshExpandedState();
            
            return p;
        }
    }
}