using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

public class TweenHandler : MonoBehaviour
{
    [SerializeReference, SubclassSelector] public List<TweenHandle> tweens;

    [Button]
    void Start()
    {
        StartCoroutine(ExecuteTweens());
    }

    IEnumerator ExecuteTweens()
    {
        foreach (var t in tweens)
        {
            yield return t.Do().WaitForCompletion();
        }
    }

    void InitializeOffScreen()
    {
        // Set the initial position of the panel outside the screen
        Vector2 offScreenPosition = new Vector2(Screen.width, 0f);
        // background.anchoredPosition = offScreenPosition;
    }
}

public enum SpaceE
{
    world = 0,
    local = 10,
}

[Serializable]
public abstract class TweenHandle
{
    public string name;
    public abstract Tween Do();
}

[Serializable]
public class Mover : TweenHandle
{
    [SerializeField] SpaceE space;
    [SerializeField] RectTransform tr;
    [SerializeField] Vector3 pos;
    [SerializeField] float duration;
    
    public override Tween Do()
    {
        switch (space)
        {
            case SpaceE.world:
                return tr.DOMove(pos, duration);
            case SpaceE.local:
                return tr.DOLocalMove(pos, duration);
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}

[Serializable]
public class Scaler : TweenHandle
{
    [SerializeField] RectTransform tr;
    [SerializeField] Vector3 scale;
    [SerializeField] float duration;
    
    public override Tween Do()
    {
        return tr.DOScale(scale, duration);
    }
}

[Serializable]
public class Rotator : TweenHandle
{
    [SerializeField] RectTransform tr;
    [SerializeField] Vector3 rot;
    [SerializeField] float duration;
    
    public override Tween Do()
    {
        return tr.DORotate(rot, duration);
    }
}
