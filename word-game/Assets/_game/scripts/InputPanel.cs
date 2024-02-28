using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WordGame
{
    public class InputPanel : MonoBehaviour
    {
        [SerializeField] WordCreationAction wordCreationAction;
        [SerializeField] OnStartEnteringWord startEnteringWordEvent;
        [SerializeField] GameObject panel;

        void Awake()
        {
            panel.SetActive(false);
        }

        void OnEnable()
        {
            startEnteringWordEvent.AddListener(HandleStartEnteringWord);
            wordCreationAction.AddListener(HandleWordCreationActon);
        }

        void HandleWordCreationActon(WordCreationAction.Data obj)
        {
            panel.SetActive(false);
        }

        void HandleStartEnteringWord(OnStartEnteringWord.Data obj)
        {
            panel.SetActive(true);
        }

        void OnDisable()
        {
            startEnteringWordEvent.RemoveListener(HandleStartEnteringWord);
            wordCreationAction.RemoveListener(HandleWordCreationActon);
        }
    }
}
