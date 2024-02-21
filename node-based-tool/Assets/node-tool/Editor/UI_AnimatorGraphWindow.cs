using System;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI_Animator
{
    public class UI_AnimatorGraphWindow : EditorWindow
    {
        BaseGraphView graphView;
        static GraphDataContainerSo graphDataContainer;
        
        public static void Open(GraphDataContainerSo container)
        {
            graphDataContainer = container;
            
            var window = GetWindow<UI_AnimatorGraphWindow>();
            window.titleContent = new GUIContent("UI Animator");
        }

        void OnEnable()
        {
            InstantiateGraphView();
            InstantiateToolbar();
            
            LoadData();
        }

        void InstantiateGraphView()
        {
            graphView = new BaseGraphView();
            graphView.StretchToParentSize();
            rootVisualElement.Add(graphView);
        }

        void InstantiateToolbar()
        {
            var tb = new Toolbar();
            
            tb.Add(new Button(SaveData) {text = "Save Data"});
            tb.Add(new Button(() => { graphView.CreateNode("CreatedNode", new Rect(300, 200, 100, 150)); }) { text = "Create Node" });
            
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
