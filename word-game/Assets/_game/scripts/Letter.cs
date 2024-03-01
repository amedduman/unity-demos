using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace WordGame
{
    public class Letter : MonoBehaviour, IPointerMoveHandler
    {
        [SerializeField] TextMeshProUGUI text;
        [SerializeField] Image img;
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

        public void OnPointerMove(PointerEventData eventData)
        {
            if (hasRegistered) return;
            hasRegistered = true;
            img.color = Color.cyan;
            letterWheel.AddLetterToWord(text.text);
        }
    }
}
