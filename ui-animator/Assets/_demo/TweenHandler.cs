using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TweenHandler : MonoBehaviour
{
    [SerializeReference, SubclassSelector] public List<TweenHandle> tweens;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(ExecuteTweens());
        }
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

[Serializable]
public abstract class TweenHandle
{
    public abstract Tween Do();
}

[Serializable]
public class Mover : TweenHandle
{
    [SerializeField] RectTransform tr;
    [SerializeField] Vector3 pos;
    [SerializeField] float duration;
    
    public override Tween Do()
    {
        return tr.DOMove(pos, duration);
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
