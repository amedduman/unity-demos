using DG.Tweening;
using UnityEngine;

namespace CasualPuzzle
{
    public enum ItemE
    {
        none = 0,
        Blue = 10,
        Red = 20,
        Green = 30,
        Yellow = 40,
        Orange = 50,
    }
    
    public class Item : MonoBehaviour
    {
        [field:SerializeField] public ItemE myType { get; private set; }

        public Tween MoveToPos(Vector3 pos)
        {
            return transform.DOMove(pos, .3f).SetEase(Ease.Linear);
        }

        public Tween DestroyProcess()
        {
            return transform.DOScale(Vector3.zero, .2f).OnComplete(() => Destroy(gameObject));
        }

        void OnDisable()
        {
            DOTween.Kill(transform);
        }
    }
}
