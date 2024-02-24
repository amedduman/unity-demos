using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Fun
{
    public class NodeSearchProvider : ScriptableObject, ISearchWindowProvider
    {
        public BaseGraph graphView;
        public VisualElement target;
        public List<SearchContextElement> elements;
        
        public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
        {
            var tree = new List<SearchTreeEntry>();
            
            tree.Add(new SearchTreeGroupEntry(new GUIContent("Nodes")));
            
            elements = new List<SearchContextElement>();

            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (Assembly assembly in assemblies)
            {
                foreach (Type t in assembly.GetTypes())
                {
                    var attribute = t.GetCustomAttribute(typeof(NodeInfoAttribute));
                    if (attribute != null)
                    {
                        var att = (NodeInfoAttribute)attribute;
                        var node = Activator.CreateInstance(t);
                        if(string.IsNullOrEmpty(att.menuItem)) continue;
                        elements.Add(new SearchContextElement(node, att.menuItem));
                    }
                }
            }

            List<string> groups = new List<string>();
            foreach (var element in elements)
            {
                string[] entryTitle = element.title.Split("/");
                string groupName = "";

                for (var i = 0; i < entryTitle.Length - 1; i++)
                {
                    groupName += entryTitle[i];
                    if (groups.Contains(groupName) == false)
                    {
                        tree.Add(new SearchTreeGroupEntry(new GUIContent(entryTitle[i]), i+1));
                        groups.Add(groupName);
                    }
                    groupName += "/";
                }

                var entry = new SearchTreeEntry(new GUIContent(entryTitle.Last()));
                entry.level = entryTitle.Length;
                entry.userData = element;
                tree.Add(entry);
            }

            return tree;
        }

        public bool OnSelectEntry(SearchTreeEntry SearchTreeEntry, SearchWindowContext context)
        {
            var windowMousePos = graphView.ChangeCoordinatesTo(graphView,
                context.screenMousePosition - graphView.window.position.position);
            var graphMousePos = graphView.contentViewContainer.WorldToLocal(windowMousePos);

            var element = (SearchContextElement)SearchTreeEntry.userData;
            NodeData nodeData = (NodeData)element.target;
            nodeData.rect = new Rect(graphMousePos, Vector2.zero); // given size seems to be doesn't matter
            nodeData.title = element.title.Split("/").Last();
            graphView.AddNode(nodeData);
            return true;
        }
    }

    public struct SearchContextElement
    {
        public object target { get; private set; }
        public string title { get; private set; }

        public SearchContextElement(object target, string title)
        {
            this.target = target;
            this.title = title;
        }
    }
}
