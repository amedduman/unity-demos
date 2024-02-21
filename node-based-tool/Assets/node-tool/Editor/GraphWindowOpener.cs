using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace UI_Animator
{
    public static class GraphWindowOpener 
    {
        [OnOpenAsset]
        public static bool Open(int instanceId, int line)
        {
            Object obj = EditorUtility.InstanceIDToObject(instanceId);

            if (obj is GraphDataContainerSo containerSo)
            {
                UI_AnimatorGraphWindow.Open(containerSo);
                return true;
            }

            return false;
        }
    }
}
