using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI_Animator
{
    public class UI_AnimatorGraphView : GraphView
    {
        public UI_AnimatorGraphView()
        {
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());

            AddElement(CreateStartNode());
        }

        UI_AnimatorNode CreateStartNode()
        {
            var node = new UI_AnimatorNode(Guid.NewGuid())
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
            var node = new UI_AnimatorNode(Guid.NewGuid())
            {
                title = nodeTitle,
                testText = nodeTitle
            };
            
            node.SetPosition(new Rect(300, 200, 100, 150));

            AddPort(node, Direction.Input, "input");
            
            AddElement(node);
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