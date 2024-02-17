using UnityEngine;
using UnityEditor;

namespace CasualPuzzle
{
    [CustomEditor(typeof(TileData))]
    public class TileDataEditor : Editor
    {
        SerializedProperty gridValues;

        void OnEnable()
        {
            gridValues = serializedObject.FindProperty("gridValues");
        }
        
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            int columns = 6;
            int rows = 8;
            // int index = 0;
            // for (int y = 0; y < height; y++)
            // {
            //     EditorGUILayout.BeginHorizontal();
            //     for (int x = 0; x < width; x++)
            //     {
            //         EditorGUILayout.PropertyField(gridValues.GetArrayElementAtIndex(index), GUIContent.none, GUILayout.Width(40));
            //         index++;
            //     }
            //     EditorGUILayout.EndHorizontal();
            // }
            
            
            EditorGUI.BeginChangeCheck();

            for (int i = 0; i < rows; i++)
            {
                EditorGUILayout.BeginHorizontal();

                for (int j = columns - 1; j >= 0; j--)
                {
                    // Display each element of the 2D array
                    EditorGUI.PropertyField(GUILayoutUtility.GetRect(18, 18), gridValues.GetArrayElementAtIndex(
                        (columns * rows - 1) - (i * columns + j)), GUIContent.none);
                }

                EditorGUILayout.EndHorizontal();
            }

            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
            }

            
            serializedObject.ApplyModifiedProperties();
        }
    }

}
