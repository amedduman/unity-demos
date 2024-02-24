using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace VisualVerse
{
    public class VV_GraphEditorWindow : EditorWindow
    {
        VV_Graph graphView;
        static int instanceIdOfGraphDataSo;
        // static VV_GraphData graphDataContainer;
        
        [OnOpenAsset]
        public static bool Open(int instanceId, int line)
        {
            UnityEngine.Object obj = EditorUtility.InstanceIDToObject(instanceId);

            if (obj is VV_GraphData containerSo)
            {
                instanceIdOfGraphDataSo = instanceId;  
                var window = GetWindow<VV_GraphEditorWindow>();
                window.titleContent = new GUIContent("Graph");
                // Open(containerSo);
                return true;
            }

            return false;
        }
        
        // static void Open(VV_GraphData container)
        // {
        //     graphDataContainer = container;
        //     
        //     var window = GetWindow<VV_GraphEditorWindow>();
        //     window.titleContent = new GUIContent("Graph");
        // }

        void OnEnable()
        {
            InstantiateGraphView();
            InstantiateToolbar();
        }

        void InstantiateGraphView()
        {
            UnityEngine.Object obj = EditorUtility.InstanceIDToObject(instanceIdOfGraphDataSo);

            if (obj is VV_GraphData graphData)
            {
                graphView = new VV_Graph(this, graphData);
                graphView.StretchToParentSize();
                rootVisualElement.Add(graphView);
            }
            else
            {
                Debug.LogError("you are trying to open the graph window without double clicking the scriptable object " + nameof(VV_GraphData));
            }
        }

        void InstantiateToolbar()
        {
            var tb = new Toolbar();
            
            tb.Add(new Button(SaveData) {text = "Save Data"});
            
            rootVisualElement.Add(tb);
        }

        void SaveData()
        {
            // GraphSaveUtility.Save(graphDataContainer, graphView);
            graphView.Save();
        }

        void OnDisable()
        {
            instanceIdOfGraphDataSo = 0; // we are resetting the int since it is a static var (just in case).
            rootVisualElement.Remove(graphView);
        }
    }
}
