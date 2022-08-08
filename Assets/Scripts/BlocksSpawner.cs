using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BlocksSpawner : MonoBehaviour
{
    [SerializeField] private Vector2[] spawnPositions;
    private Blocks[] blocksList;

    private void Start()
    {
        blocksList = new Blocks[3];
        GameManager.Instance.OnGameStart += SpawnBlocks;
    }

    public void SpawnBlocks()
    {
        for (int i = 0; i < 3; i++)
        {
            Blocks blocks = Pools.Instance.GetBlocks();
            blocks.transform.parent = transform;
            blocks.transform.position = transform.position + (Vector3)spawnPositions[i];
            blocks.InitByShapeData();
            // blocks.Init();
            blocksList[i] = blocks;
        }
    }

    public void RecreateBlocks()
    {
        for (int i = 0; i < 3; i++)
        {
            blocksList[i].Break();
            blocksList[i].InitByShapeData();
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

[CustomEditor(typeof(BlocksSpawner))]
public class BlocksSpawnerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Recreate blocks"))
        {
            BlocksSpawner script = (target as BlocksSpawner);
            script.RecreateBlocks();
        }
    }
}
