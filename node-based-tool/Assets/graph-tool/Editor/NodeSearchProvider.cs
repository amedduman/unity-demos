using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI_Animator
{
    public class NodeSearchProvider : ScriptableObject, ISearchWindowProvider
    {
        public GraphView graphView;
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
                entry.userData = new SearchContextElement(element.target, element.title);
                tree.Add(entry);
            }

            return tree;
        }

        public bool OnSelectEntry(SearchTreeEntry SearchTreeEntry, SearchWindowContext context)
        {
            switch (SearchTreeEntry.userData)
            {
                case BaseNode:
                    Debug.Log("base node selected");
                    return true;
                default:
                    return true;
            }
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
