using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI_Animator
{
    public class UI_AnimatorGraphWindow : EditorWindow
    {
        UI_AnimatorGraphView graphView;
        static GraphDataContainerSo graphDataContainer;
        
        public static void Open(GraphDataContainerSo container)
        {
            var window = GetWindow<UI_AnimatorGraphWindow>();
            window.titleContent = new GUIContent("UI Animator");

            graphDataContainer = container;
        }

        void OnEnable()
        {
            InstantiateGraphView();
            InstantiateToolbar();
        }

        void InstantiateGraphView()
        {
            graphView = new UI_AnimatorGraphView();
            graphView.StretchToParentSize();
            rootVisualElement.Add(graphView);
        }

        void InstantiateToolbar()
        {
            var tb = new Toolbar();
            
            tb.Add(new Button(SaveData) {text = "Save Data"});
            tb.Add(new Button(() => { graphView.CreateNode("CreatedNode"); }) { text = "Create Node" });
            
            rootVisualElement.Add(tb);
        }

        void SaveData()
        {
            GraphSaveUtility.Save(graphDataContainer, graphView.nodes.ToList());
        }

        void OnDisable()
        {
            graphDataContainer = null;
            rootVisualElement.Remove(graphView);
        }
    }
}
