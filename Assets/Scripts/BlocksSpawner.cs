using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocksSpawner : MonoBehaviour
{
    [SerializeField] private Vector2[] spawnPositions;

    private void Start()
    {
        GameManager.Instance.OnGameStart += SpawnBlocks;
    }

    public void SpawnBlocks()
    {
        for (int i = 0; i < 3; i++)
        {
            Blocks blocks = Pools.Instance.GetBlocks();
            blocks.transform.parent = transform;
            blocks.transform.position = transform.position + (Vector3)spawnPositions[i];
            blocks.Init();
        }
    }

    private void CheckGameOver()
    {

    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        foreach (var pos in spawnPositions)
        {
            Gizmos.DrawWireSphere(transform.position + (Vector3)pos, 10f);
        }
    }
#endif
}
