using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;
    public GameObject coinPrefab;

    private void Awake() => Instance = this;

    public void SpawnCoinsAlongStack()
    {
        foreach (Transform block in StackManager.Instance.stackRoot)
        {
            if (block.CompareTag("Block"))
            {
                Block blockComp = block.GetComponent<Block>();
                if (blockComp == null) continue;

                float halfWidth = blockComp.Width / 2f;

                float randomX = Random.Range(-halfWidth + 0.2f, halfWidth - 0.2f);
                Vector3 spawnPos = new Vector3(
                    block.position.x + randomX,
                    block.position.y + 0.5f,
                    block.position.z
                );

                Instantiate(coinPrefab, spawnPos, Quaternion.identity);
            }
        }
    }
}