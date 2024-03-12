using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tetris
{
    public class BlockSpawner : MonoBehaviour
    {
        [SerializeField] List<Block> blockPrefabs;
        Vector3 spawnPos;
        
        public void OnStart()
        {
            spawnPos = new Vector3(0, 10, 0);
            StartCoroutine(SpawnCo());
        }

        IEnumerator SpawnCo()
        {
            while (true)
            {
                var blockPrefab = blockPrefabs[Random.Range(0, blockPrefabs.Count)];

                yield return new WaitForSeconds(1);
            
                Instantiate(blockPrefab, spawnPos, Quaternion.identity);
            }
        }
    }
}
