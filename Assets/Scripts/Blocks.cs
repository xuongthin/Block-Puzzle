using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Blocks : PooledObject, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private BlocksSetting setting;
    private BlockColor color;
    private bool isPlayable;
    private BlocksData data;
    private List<Block> blockList;

    public override void OnCreated(ObjectPool pool)
    {
        base.OnCreated(pool);
        setting = GameManager.Instance.BlocksSetting;
    }

    public void Init(Matrix5x5 shapes)
    {

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
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.position = (eventData.position + setting.offset);
    }

    private void CheckPlayability()
    {

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
