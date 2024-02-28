using UnityEngine;

namespace WordGame
{
    public class Tile : MonoBehaviour
    {
        public SpriteRenderer spriteRenderer;
        [HideInInspector] public Vector3Int cellPos;
        [SerializeField] WordCreationAction wordCreationAction;
        [SerializeField] GameObject Buttons;

        public void OnTileSelected()
        {
            Invoke(nameof(ActivateButtons), .1f);
        }

        void ActivateButtons()
        {
            Buttons.SetActive(true);
        }

        public void HandleWordCreationBtnClicked(WordCreationDirectionE dir)
        {
            Buttons.SetActive(false);
            wordCreationAction.SetData(this, this, dir);
        }
    }
}
