using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Tetris
{
    public class BlockSpawner : MonoBehaviour
    {
        [SerializeField] List<Block> blockPrefabs;
        Vector3 spawnPos;
        // Block currentBlock;

        void Start()
        {
            spawnPos = new Vector3(0, 20, 0);
            StartCoroutine(SpawnCo());
        }

        IEnumerator SpawnCo()
        {
            while (true)
            {
                var blockPrefab = blockPrefabs[Random.Range(0, blockPrefabs.Count)];

                yield return new WaitForSeconds(1);
            
                // var currentBlock = Instantiate(blockPrefab, spawnPos, Quaternion.identity);

                var data = GridHandler.data;
                var index = (data.height * data.width) - Random.Range(1, data.width + 1);
                Vector3Int blockPos = data.tiles[index].cellPos;
                data.tiles[index].spriteRenderer.color = Color.cyan;
                Debug.Log(index);
                // currentBlock.Move(blockPos);
            }
        }

        // public Block GetCurrentSpawnedBlock()
        // {
        //     if (currentBlock == null)
        //     {
        //         throw new SystemException("you are requesting new block but there is no block.");
        //     }
        //
        //     return currentBlock;
        // }
    }
}
