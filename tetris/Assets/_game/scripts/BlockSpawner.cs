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
        GridHandler.Data gridData;
        Vector3Int topBlockPos;
        Vector3 spawnPos;

        public void Init(GridHandler.Data in_gridData)
        {
            gridData = in_gridData;
            topBlockPos = gridData.TopBlockCellPos;
            spawnPos = new Vector3(topBlockPos.x, topBlockPos.y + 2, 0);
            StartCoroutine(SpawnCo());
        }

        IEnumerator SpawnCo()
        {
            while (true)
            {
                var blockPrefab = blockPrefabs[Random.Range(0, blockPrefabs.Count)];

                yield return new WaitForSeconds(5);
            
                var currentBlock = Instantiate(blockPrefab, spawnPos, Quaternion.identity);

                currentBlock.OnSpawn(topBlockPos, gridData);
            }
        }
    }
}
