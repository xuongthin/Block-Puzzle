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

    [SerializeField] private Block blockPrefab;
    [SerializeField] private PreviewBlock previewBlockPrefab;
    private ObjectPool blockPool;
    private ObjectPool previewBlockPool;

    private void Start()
    {
        blockPool = new ObjectPool(blockPrefab, 100, transform);
        previewBlockPool = new ObjectPool(previewBlockPrefab, 20, transform);
    }

    public Block GetBlock()
    {
        return (blockPool.Release() as Block);
    }

    public PreviewBlock GetPreviewBlock()
    {
        return (previewBlockPool.Release() as PreviewBlock);
    }
}
