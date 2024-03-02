using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace WordGame
{
    public class Letter : MonoBehaviour
    {
        [SerializeField] TextMeshPro text;
        [SerializeField] SpriteRenderer img;
        LetterWheel letterWheel;
        bool hasRegistered;

        void Awake()
        {
            letterWheel = GetComponentInParent<LetterWheel>();
        }

        public void SetText(char c)
        {
            text.text = c.ToString();
        }

        public void UnRegister()
        {
            hasRegistered = false;
            img.color = Color.white;
        }
        
        void OnMouseEnter()
        {
            if (hasRegistered) return;
            hasRegistered = true;
            img.color = Color.cyan;
            letterWheel.AddLetterToWord(text.text);
        }

        // void OnMouseDown()
        // {
        //     throw new NotImplementedException();
        // }
        //
        // void OnMouseDrag()
        // {
        //     Debug.Log("drag");
        // }
        

        // public void OnPointerMove(PointerEventData eventData)
        // {
        //     // if (hasRegistered) return;
        //     // hasRegistered = true;
        //     // img.color = Color.cyan;
        //     // letterWheel.AddLetterToWord(text.text);
        // }
    }
}
