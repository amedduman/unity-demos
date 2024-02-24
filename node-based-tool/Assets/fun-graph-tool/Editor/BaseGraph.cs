using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Fun
{
    public class BaseGraph : GraphView
    {
        readonly NodeSearchProvider searchProvider;
        public GraphEditorWindow window { get; private set; }
        public GraphDataContainerSo graphDataContainerSo;
        readonly List<NodeData> nodeDataList;
        
        public BaseGraph(GraphEditorWindow window, GraphDataContainerSo graphDataContainerSo)
        {
            SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());

            this.window = window;
            this.graphDataContainerSo = graphDataContainerSo;

            nodeDataList = new List<NodeData>();
            
            searchProvider = ScriptableObject.CreateInstance<NodeSearchProvider>();
            searchProvider.graphView = this;
            nodeCreationRequest = ShowSearchWindow;
            graphViewChanged = HandleGraphViewChanged;
        }

        GraphViewChange HandleGraphViewChanged(GraphViewChange graphviewchange)
        {
            if (graphviewchange.elementsToRemove != null)
            {
                foreach (var toRemove in graphviewchange.elementsToRemove)
                {
                    if (toRemove is Node node)
                    {
                        for (int i = nodeDataList.Count - 1; i >= 0; i--)
                        {
                            if (node.viewDataKey == nodeDataList[i].editorNodeGuid)
                            {
                                nodeDataList.Remove(nodeDataList[i]);
                            }
                        }
                    }
                }
            }
            
            return graphviewchange;
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
        
        void ShowSearchWindow(NodeCreationContext context)
        {
            searchProvider.target = (VisualElement)focusController.focusedElement;
            SearchWindow.Open(new SearchWindowContext(context.screenMousePosition), searchProvider);
        }
        
        public void AddNode(NodeData nodeData)
        {
            var node = new NodeEditor(nodeData);

            nodeDataList.Add(nodeData);
            
            node.RefreshPorts();
            node.RefreshExpandedState();
            
            AddElement(node);
        }

        public void Save()
        {
            NodeData startNode = null;
            foreach (NodeData nodeData in nodeDataList)
            {
                if (nodeData.isStartNode)
                {
                    startNode = nodeData;
                }
            }

            if (startNode == null)
            {
                Debug.LogError("There is no start node");
                return;
            }

            int order = 0;
            startNode.order = order;

            Node node = GetNodeByGuid(startNode.editorNodeGuid);

            bool isRunning = true;
            while (isRunning)
            {
                foreach (Port port in ports)
                {
                    if (port.node == node && port.portName == "exit")
                    {
                        if (!port.connections.Any())
                        {
                            isRunning = false;
                            break;
                        }
                        // Debug.Log(port.connections.Count() + " " + port.portName + " " + node.title);
                        if (port.connections.Count() > 1)
                        {
                            Debug.LogError("the exit port capacity should be single");
                            return;
                        }
                    
                        foreach (var connection in port.connections)
                        {
                            Node nextNode = connection.input.node;
                        
                            foreach (NodeData nodeData in nodeDataList)
                            {
                                if (nodeData.editorNodeGuid == nextNode.viewDataKey)
                                {
                                    order += 1;
                                    nodeData.order = order;
                                }
                            }

                            node = nextNode;
                        }
                    }
                }
            }            

            foreach (NodeData nodeData in nodeDataList)
            {
                Debug.Log(nodeData.title + " " + nodeData.order);
                nodeData.Execute();
            }
        }

        // public void CreateNode(string nodeTitle, Rect rect, string guid = "")
        // {
        //     var node = new Node();
        //
        //     if (guid != "")
        //         node.viewDataKey = guid;
        //     
        //     node.SetPosition(rect);
        //
        //     var btn = new Button(() => { AddOutputPort(node);});
        //     btn.text = "Add";
        //     node.titleContainer.Add(btn);
        //
        //     AddPort(node, Direction.Input, "input");
        //     
        //     AddElement(node);
        // }

        // void AddOutputPort(Node node)
        // {
        //     var outputPortCount = node.outputContainer.Query("connector").ToList().Count;
        //     var portName = $"output {outputPortCount + 1}";
        //     AddPort(node, Direction.Output, portName);
        // }

        // Port AddPort(Node node, Direction portDirection, string portName, Port.Capacity capacity = Port.Capacity.Single)
        // {
        //     var p = node.InstantiatePort(Orientation.Horizontal, portDirection, capacity, typeof(float));
        //     p.portName = portName;
        //     switch (portDirection)
        //     {
        //         case Direction.Input:
        //             node.inputContainer.Add(p);
        //             break;
        //         case Direction.Output:
        //             node.outputContainer.Add(p);
        //             break;
        //         default:
        //             throw new ArgumentOutOfRangeException(nameof(portDirection), portDirection, null);
        //     }
        //     
        //     node.RefreshPorts();
        //     node.RefreshExpandedState();
        //     
        //     return p;
        // }
    }
}