using System;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Fun
{
    public class GraphEditorWindow : EditorWindow
    {
        BaseGraph graphView;
        static GraphDataContainerSo graphDataContainer;
        
        [OnOpenAsset]
        public static bool Open(int instanceId, int line)
        {
            UnityEngine.Object obj = EditorUtility.InstanceIDToObject(instanceId);

            if (obj is GraphDataContainerSo containerSo)
            {
                Open(containerSo);
                return true;
            }

            return false;
        }
        
        static void Open(GraphDataContainerSo container)
        {
            graphDataContainer = container;
            
            var window = GetWindow<GraphEditorWindow>();
            window.titleContent = new GUIContent("Graph");
        }

        void OnEnable()
        {
            InstantiateGraphView();
            InstantiateToolbar();
            
            LoadData();
        }

        void InstantiateGraphView()
        {
            graphView = new BaseGraph(this);
            graphView.StretchToParentSize();
            rootVisualElement.Add(graphView);
        }

        void InstantiateToolbar()
        {
            var tb = new Toolbar();
            
            tb.Add(new Button(SaveData) {text = "Save Data"});
            
            rootVisualElement.Add(tb);
        }

        void SaveData()
        {
            GraphSaveUtility.Save(graphDataContainer, graphView);
        }

        void LoadData()
        {
            GraphSaveUtility.Load(graphDataContainer, graphView);
        }

        void OnDisable()
        {
            graphDataContainer = null;
            rootVisualElement.Remove(graphView);
        }
    }
}
