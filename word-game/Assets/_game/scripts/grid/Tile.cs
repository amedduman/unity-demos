using UnityEngine;

namespace WordGame
{
    public class Tile : MonoBehaviour
    {
        public SpriteRenderer spriteRenderer;
        [HideInInspector] public Vector3Int cellPos;
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
            Game.wordInputData.tile = this;
            Game.wordInputData.dir = dir;
            Game.onStartingTileAndDirectionSet.Invoke();
        }
    }
}
