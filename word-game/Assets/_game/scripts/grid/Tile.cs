using DG.Tweening;
using TMPro;
using UnityEngine;

namespace WordGame
{
    public class Tile : MonoBehaviour
    {
        public SpriteRenderer spriteRenderer;
        [HideInInspector] public Vector3Int cellPos;
        [SerializeField] GameObject Buttons;
        [SerializeField] TextMeshPro letter;
 
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

        public void SetLetter(char l)
        {
            letter.text = l.ToString();
        }

        public string GetLetter()
        {
            return letter.text;
        }

        public void SetWordDirection(WordCreationDirectionE dir)
        {
            Buttons.SetActive(false);
            Game.wordInputData.tile = this;
            Game.wordInputData.dir = dir;
            Game.onStartingTileAndDirectionSet.Invoke();
        }

        public void HideLetter()
        {
            letter.enabled = false;
        }

        public void RevealLetter()
        {
            letter.enabled = true;
            var sc = letter.transform.localScale;
            letter.transform.localScale = Vector3.zero;
            letter.transform.DOScale(sc, .2f);
        }
    }
}
