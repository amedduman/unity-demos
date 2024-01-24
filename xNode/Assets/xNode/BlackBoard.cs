using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackBoard : MonoBehaviour
{
    [SerializeField] List<SceneObject> SceneObjects;

    void OnValidate()
    {
        foreach (var obj in SceneObjects)
        {
            if (obj.Id == string.Empty)
            {
                obj.Cmp = null;
                return;
            }

            if (obj.ComponentType == EComponentType.None)
            {
                obj.Cmp = null;
                return;
            }

            switch (obj.ComponentType)
            {
                case EComponentType.None:
                    break;
                case EComponentType.Image:
                    obj.Cmp = obj.Cmp.gameObject.GetComponent<Image>();
                    break;
                case EComponentType.Transform:
                    obj.Cmp = obj.Cmp.gameObject.GetComponent<Transform>();
                    break;
                case EComponentType.RectTransform:
                    obj.Cmp = obj.Cmp.gameObject.GetComponent<RectTransform>();
                    break;
                case EComponentType.Rigidbody:
                    obj.Cmp = obj.Cmp.gameObject.GetComponent<Rigidbody>();
                    break;
                default:
                    obj.Cmp = null;
                    Debug.Log("type is not defined");
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    public Component GetSceneObject(string id)
    {
        foreach (var sceneObject in SceneObjects)
        {
            if (sceneObject.Id == id)
            {
                return sceneObject.Cmp;
            }
        }

        throw new NotImplementedException();
    }
}

[Serializable]
internal class SceneObject
{
    public string Id;
    public EComponentType ComponentType;
    public Component Cmp;
}

internal enum EComponentType
{
    None,
    Image,
    Transform,
    RectTransform,
    Rigidbody
}