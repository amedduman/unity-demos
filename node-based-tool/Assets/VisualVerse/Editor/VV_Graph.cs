using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace VisualVerse
{
    public class VV_Graph : GraphView
    {
        readonly NodeSearchProvider searchProvider;
        public VV_GraphEditorWindow window { get; private set; }
        public VV_GraphData graphData;
        readonly List<VV_NodeRuntime> nodeDataList;
        
        public VV_Graph(VV_GraphEditorWindow window, VV_GraphData graphDataContainerSo)
        {
            SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());

            this.window = window;
            graphData = graphDataContainerSo;

            nodeDataList = new List<VV_NodeRuntime>();
            
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
        
        public void AddNode(VV_NodeRuntime nodeData)
        {
            var node = new VV_NodeEditor(nodeData);

            nodeDataList.Add(nodeData);
            
            node.RefreshPorts();
            node.RefreshExpandedState();
            
            AddElement(node);
        }

        public void Save()
        {
            VV_NodeRuntime startNode = null;
            foreach (VV_NodeRuntime nodeData in nodeDataList)
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

            while (true)
            {
                var exitPort = GetExitPort(node);
                
                if (!exitPort.connections.Any())
                {
                    break;
                }
                
                if (exitPort.connections.Count() > 1)
                {
                    Debug.LogError("the exit port capacity should be single");
                    return;
                }
                    
                foreach (var connection in exitPort.connections)
                {
                    Node nextNode = connection.input.node;
                        
                    foreach (VV_NodeRuntime nodeData in nodeDataList)
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

            foreach (VV_NodeRuntime nodeData in nodeDataList)
            {
                Debug.Log(nodeData.title + " " + nodeData.order);
                nodeData.Execute();
            }
        }

        Port GetExitPort(Node node)
        {
            var nodePorts = node.outputContainer.Query<Port>().ToList();
            foreach (var port in nodePorts)
            {
                if (port.portName == "exit")
                {
                    return port;
                }
            }

            throw new NotImplementedException();
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