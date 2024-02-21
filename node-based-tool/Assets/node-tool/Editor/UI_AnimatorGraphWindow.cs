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
            
            string fileName = "New Graph";
            // var fileNameTextField = new TextField("File Name:");
            // fileNameTextField.SetValueWithoutNotify(fileName);
            // fileNameTextField.MarkDirtyRepaint();
            // fileNameTextField.RegisterValueChangedCallback(evt => fileName = evt.newValue);
            // tb.Add(fileNameTextField);
            
            tb.Add(new Button(() => SaveData(fileName)) {text = "Save Data"});

            tb.Add(new Button(() => { graphView.CreateNode("CreatedNode"); }) { text = "Create Node" });
            
            rootVisualElement.Add(tb);
        }

        void SaveData(string fileName)
        {
            // if (string.IsNullOrEmpty(fileName))
            // {
            //     EditorUtility.DisplayDialog("Invalid File name", "Please Enter a valid filename", "OK");
            //     return;
            // }
            GraphSaveUtility.Save(graphDataContainer, graphView.nodes.ToList());
        }

        void OnDisable()
        {
            rootVisualElement.Remove(graphView);
        }
    }
}
