using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocksManager : MonoBehaviour
{
    public static BlocksManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] private Blocks[] blocksList;
    [SerializeField] private Vector2[] spawnPositions;
    [SerializeField] private ShapePool shapeManager;

    private Blocks movingBlocks;

    public Blocks MovingBlocks => movingBlocks;

    private void Start()
    {
        shapeManager = new ShapePool();
        GameManager.Instance.OnGameStart += SpawnBlocks;
        GameManager.Instance.OnBlockPlaced += CheckRemainBlocks;
    }

    public void SpawnBlocks()
    {
        for (int i = 0; i < 3; i++)
        {
            Shape shape = shapeManager.GetRandomShape();
            blocksList[i].transform.localPosition = (Vector3)spawnPositions[i];
            blocksList[i].Init(shape);
        }

        CheckRemainBlocks();
    }

    public void SetMovingBlocks(Blocks blocks)
    {
        movingBlocks = blocks;
    }

    private void CheckRemainBlocks()
    {
        int countPlaced = 0;
        bool isPlayable = false;
        foreach (Blocks blocks in blocksList)
        {
            if (blocks.IsPlayed)
                countPlaced++;
            else
            {
                blocks.CheckPlayability();
                if (blocks.IsPlayable)
                    isPlayable = true;
            }
        }

        if (countPlaced == 3)
            SpawnBlocks();
        else if (!isPlayable)
            GameManager.Instance.OnGameOver();
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
