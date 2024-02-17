using UnityEngine;
using UnityEditor;

namespace CasualPuzzle
{
    [CustomEditor(typeof(TileData))]
    public class TileDataEditor : Editor
    {
        SerializedProperty gridValues;

        private void OnEnable()
        {
            gridValues = serializedObject.FindProperty("gridValues");
        }
        
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            // EditorGUILayout.BeginHorizontal();
            // EditorGUILayout.PropertyField(gridValues.GetArrayElementAtIndex(0), GUIContent.none, GUILayout.Width(10));
            // EditorGUILayout.PropertyField(gridValues.GetArrayElementAtIndex(0), GUIContent.none, GUILayout.Width(10));
            // EditorGUILayout.EndHorizontal();
            //
            // return;

            // EditorGUILayout.BeginHorizontal();

            int width = 6; // Number of elements in each row
            int height = 8;
            for (int x = 0; x < width; x++)
            {
                EditorGUILayout.BeginHorizontal();
                for (int y = 0; y < height; y++)
                {
                    EditorGUILayout.PropertyField(gridValues.GetArrayElementAtIndex(x), GUIContent.none, GUILayout.Width(40));
                    // if (y == height - 1)
                    // {
                    //     EditorGUILayout.BeginHorizontal();
                    //     EditorGUILayout.EndHorizontal();  
                    // }
                }
                EditorGUILayout.EndHorizontal();
            }

            // for (int i = 0; i < gridValues.arraySize; i++)
            // {
            //     if (i % columnSize == 0 && i != 0)
            //     {
            //         EditorGUILayout.BeginVertical();
            //         EditorGUILayout.EndVertical();
            //     }
            //     // if (i % rowSize == 0 && i != 0)
            //     // {
            //     //     EditorGUILayout.BeginHorizontal();
            //     //     EditorGUILayout.EndHorizontal();
            //     // }
            //
            //     EditorGUILayout.PropertyField(gridValues.GetArrayElementAtIndex(i), GUIContent.none, GUILayout.Width(40));
            // }

            // EditorGUILayout.EndHorizontal();

            serializedObject.ApplyModifiedProperties();
        }

        // public override void OnInspectorGUI()
        // {
        //     serializedObject.Update();
        //     
        //     gridValues = serializedObject.FindProperty("gridValues");
        //
        //     EditorGUILayout.BeginHorizontal();
        //
        //     for (int i = 0; i < gridValues.arraySize; i++)
        //     {
        //         // if ((i + 1) % 4 == 0) // Break to the next line after every 4 elements
        //         //     EditorGUILayout.BeginVertical();
        //         
        //         EditorGUILayout.PropertyField(gridValues.GetArrayElementAtIndex(i), GUIContent.none, GUILayout.Width(40));
        //         if ((i + 1) % 4 == 0) // Break to the next line after every 4 elements
        //             EditorGUILayout.EndVertical();
        //     }
        //
        //     EditorGUILayout.EndHorizontal();
        //
        //     serializedObject.ApplyModifiedProperties();
        // }
    }

}
