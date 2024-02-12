using System.Collections.Generic;
using UnityEngine;

namespace CasualPuzzle
{
    [CreateAssetMenu(menuName = "_game/data/items to spawn")]
    public class ItemsToSpawnData : ScriptableObject
    {
        [field:SerializeField] public List<Item> items { get; private set; }
    }
}