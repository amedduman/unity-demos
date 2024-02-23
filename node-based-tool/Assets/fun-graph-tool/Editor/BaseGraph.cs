using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Fun
{
    public class BaseGraph : GraphView
    {
        readonly NodeSearchProvider searchProvider;
        public GraphEditorWindow window { get; private set; }
        
        public BaseGraph(GraphEditorWindow window)
        {
            SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());

            this.window = window;
            
            searchProvider = ScriptableObject.CreateInstance<NodeSearchProvider>();
            searchProvider.graphView = this;
            nodeCreationRequest = ShowSearchWindow;
        }

        void ShowSearchWindow(NodeCreationContext context)
        {
            searchProvider.target = (VisualElement)focusController.focusedElement;
            SearchWindow.Open(new SearchWindowContext(context.screenMousePosition), searchProvider);
        }

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            var compatiblePorts = new List<Port>();

            foreach (Port port in ports)
            {
                if(startPort == port || startPort.node == port.node) continue;

                if(startPort.portType == port.portType)
                    compatiblePorts.Add(port);
            }

            return compatiblePorts;
        }

        public void CreateNode(string nodeTitle, Rect rect, string guid = "")
        {
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

        public void AddNode(NodeData nodeData)
        {
            var node = new NodeEditor(nodeData);
            
            node.RefreshPorts();
            node.RefreshExpandedState();
            
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