using System;
using UnityEngine;

namespace WordGame
{
    public class Tile : MonoBehaviour
    {
        public SpriteRenderer spriteRenderer;
        [HideInInspector] public Vector3Int cellPos;
        [SerializeField] WordCreationAction wordCreationAction;
        [SerializeField] OnStartEnteringWord startEnteringWordEvent;
        [SerializeField] GameObject Buttons;

        void Awake()
        {
            Buttons.SetActive(false);
        }

        public void Select()
        {
            Invoke(nameof(ActivateButtons), .1f);
        }

        void ActivateButtons()
        {
            Buttons.SetActive(true);
        }

        public void SetWordDirection(WordCreationDirectionE dir)
        {
            Buttons.SetActive(false);
            wordCreationAction.SetData(this, this, dir);
            startEnteringWordEvent.Invoke(new OnStartEnteringWord.Data());
        }
    }
}
