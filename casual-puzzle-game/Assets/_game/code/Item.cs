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
    }
}
