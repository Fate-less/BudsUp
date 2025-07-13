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
                Vector3 spawnPos = new Vector3(block.position.x, block.position.y + 0.5f, block.position.z);
                Instantiate(coinPrefab, spawnPos, Quaternion.identity);
            }
        }
    }
}