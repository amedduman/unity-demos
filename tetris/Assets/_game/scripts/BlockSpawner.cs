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
        Vector3Int topBlockPos;
        Vector3 spawnPos;

        public void Init(GridHandler.Data data)
        {
            var index = (data.height * data.width) - Mathf.FloorToInt((float)data.width/2 + 1);
            data.tiles[index].spriteRenderer.color = Color.cyan;
            topBlockPos = data.tiles[index].cellPos;
            spawnPos = new Vector3(topBlockPos.x, topBlockPos.y + 2, 0);
            StartCoroutine(SpawnCo());
        }

        IEnumerator SpawnCo()
        {
            while (true)
            {
                var blockPrefab = blockPrefabs[Random.Range(0, blockPrefabs.Count)];

                yield return new WaitForSeconds(1);
            
                var currentBlock = Instantiate(blockPrefab, spawnPos, Quaternion.identity);

                currentBlock.Move(topBlockPos);
            }
        }
    }
}
