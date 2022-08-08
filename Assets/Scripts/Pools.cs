using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pools : MonoBehaviour
{
    public static Pools Instance;
    private void Awake()
    {
        Instance = this;
    }

    private List<ObjectPool> pools;
    [SerializeField] private Blocks blocksPrefab;
    [SerializeField] private Block blockPrefab;
    [SerializeField] private PooledObject ghostBlockPrefab;
    private ObjectPool blockPool;
    private ObjectPool blocksPool;
    private ObjectPool ghostBlockPool;

    private void Start()
    {
        blockPool = new ObjectPool(blockPrefab, 100, transform);
        blocksPool = new ObjectPool(blocksPrefab, 3, transform);
    }

    public Block GetBlock()
    {
        return (blockPool.Release() as Block);
    }

    public Blocks GetBlocks()
    {
        return (blocksPool.Release() as Blocks);
    }
}
