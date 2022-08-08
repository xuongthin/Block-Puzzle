using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Blocks : PooledObject, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private BlocksSetting setting;
    private BlockColor color;
    private BlocksData data;
    private List<Block> blockList;
    private Shape shape;

    private bool isPlayable;
    private bool isPlacable;

    public override void OnCreated(ObjectPool pool)
    {
        base.OnCreated(pool);
        blockList = new List<Block>();
        setting = GameManager.Instance.BlocksSetting;
    }

    public void InitByShapeData()
    {
        color = (BlockColor)Random.Range(0, 7);
        shape = ShapeManager.Instance.GetRandomShape();

        bool[,] matrix = shape.matrix;
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (matrix[i, j])
                {
                    Block block = Pools.Instance.GetBlock();
                    block.transform.parent = transform;
                    block.transform.localPosition = new Vector2(i, j) * setting.size;
                    block.SetColor(color);

                    blockList.Add(block);
                }
            }
        }

        CheckPlayability();
    }

    public void Init()
    {
        // TODO: fix it later
        color = (BlockColor)Random.Range(0, 7);
        data = setting.blocksData[Random.Range(0, 11)];

        foreach (var position in data.encodedPositions)
        {
            Block block = Pools.Instance.GetBlock();
            block.transform.parent = transform;
            block.transform.localPosition = position * setting.size;
            block.SetColor(color);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.position = (eventData.position + setting.offset);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = (eventData.position + setting.offset);
        // TODO: check grid
        if (CheckPlacability())
        {
            isPlacable = true;
            ShowGhost();
        }
        else
        {
            isPlacable = false;
            HideGhost();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (isPlacable)
        {
            while (blockList.Count > 0)
            {
                blockList[0].PlaceOnGrid();
                blockList.RemoveAt(0);
            }
        }
        else
        {
            // TODO: return to init position
        }
    }

    // NOTE: this method use to test recreate blocks continuously 
    // TODO: remove this
    public void Break()
    {
        while (blockList.Count > 0)
        {
            blockList[0].ReturnToPool();
            blockList.RemoveAt(0);
        }
    }

    private bool CheckPlacability()
    {
        foreach (Block block in blockList)
        {
            if (!block.CheckOnGrid())
                return false;
        }
        return true;
    }

    private void ShowGhost()
    {
        foreach (Block block in blockList)
        {
            block.PlaceGhost();
        }
    }

    private void HideGhost()
    {

    }

    private void CheckPlayability()
    {
        isPlayable = Grid.Instance.CheckBlockPlayability(shape.bitMask);

        if (isPlayable)
        {
            // TODO: set all block are visiable
        }
        else
        {
            // TODO: set all block are transparent
        }
    }
}
