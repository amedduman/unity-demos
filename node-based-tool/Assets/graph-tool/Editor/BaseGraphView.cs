using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI_Animator
{
    public class BaseGraphView : GraphView
    {
        NodeSearchProvider searchProvider = ScriptableObject.CreateInstance<NodeSearchProvider>();
        
        public BaseGraphView()
        {
            SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());

            searchProvider = ScriptableObject.CreateInstance<NodeSearchProvider>();
            searchProvider.graphView = this;
            nodeCreationRequest = ShowSearchWindow;
            AddSearchWindow();
        }

        void ShowSearchWindow(NodeCreationContext context)
        {
            
        }

        void AddSearchWindow()
        {
            
            nodeCreationRequest = context =>    
                SearchWindow.Open(new SearchWindowContext(context.screenMousePosition), searchProvider);
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

        Node CreateStartNode()
        {
            // var node = new BaseNode()
            // {
            //     // title = "Start",
            //     entry = true,
            //     testText = "Test"
            // };
            var node = new Node();

            node.SetPosition(new Rect(100, 200, 100, 150));
            
            AddPort(node, Direction.Output, "next");
            
            return node;
        }

        public void CreateNode(string nodeTitle, Rect rect, string guid = "")
        {
            //var node = new BaseNode()
            //{
            //     title = nodeTitle,
            //     testText = nodeTitle
            //};
            var node = new Node();

            if (guid != "")
                node.viewDataKey = guid;
            
            node.SetPosition(rect);

            var btn = new Button(() => { AddOutputPort(node);});
            btn.text = "Add";
            node.titleContainer.Add(btn);

            AddPort(node, Direction.Input, "input");
            
            AddElement(node);
        }

        void AddOutputPort(Node node)
        {
            var outputPortCount = node.outputContainer.Query("connector").ToList().Count;
            var portName = $"output {outputPortCount + 1}";
            AddPort(node, Direction.Output, portName);
        }

        Port AddPort(Node node, Direction portDirection, string portName, Port.Capacity capacity = Port.Capacity.Single)
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