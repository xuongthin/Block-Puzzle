using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Block : PooledObject
{
    private Image image;
    private int cellId;

    public override void OnCreated(ObjectPool pool)
    {
        base.OnCreated(pool);
        image = GetComponent<Image>();
    }

    public void SetColor(BlockColor color)
    {
        Sprite sprite = GameManager.Instance.BlockSetting.sprites[((int)color)];
        image.sprite = sprite;
    }

    public bool CheckOnGrid()
    {
        cellId = Grid.Instance.GetCellIdAt(transform.position);
        return Grid.Instance.GetCellStatus(cellId);
    }

    public void PlaceGhost()
    {
        // TODO: 
    }

    public void PlaceOnGrid()
    {
        // TODO: create animation by DoTween
        transform.parent = Grid.Instance.transform;
        transform.localPosition = Grid.Instance.CellId2Position(cellId);
    }
}
