using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI_Animator
{
    public class UI_AnimatorGraphWindow : EditorWindow
    {
        UI_AnimatorGraphView graphView;
        
        [MenuItem("Tools/UI  Animator")]
        public static void Open()
        {
            var window = GetWindow<UI_AnimatorGraphWindow>();
            window.titleContent = new GUIContent("UI Animator");
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

            var nodeCreateBtn = new Button(() => { graphView.CreateNode("CreatedNode"); });
            nodeCreateBtn.text = "Create Node";
            tb.Add(nodeCreateBtn);
            
            rootVisualElement.Add(tb);
        }

        void OnDisable()
        {
            rootVisualElement.Remove(graphView);
        }
    }
}
