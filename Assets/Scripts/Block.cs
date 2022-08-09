using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Block : PooledObject
{
    [SerializeField] private Image image;
    private BlockColor color;
    private Vector2Int cell;
    public Vector2Int Cell => cell;

    public override void OnCreated(ObjectPool pool)
    {
        base.OnCreated(pool);
    }

    public void SetColor()
    {
        SetColor(color, false);
    }

    public void SetColor(BlockColor color, bool temporary = false)
    {
        if (!temporary)
            this.color = color;

        Sprite sprite = GameManager.Instance.BlockSetting.sprites[((int)color)];
        image.sprite = sprite;
    }

    public void CheckOnCell()
    {
        cell = Grid.Instance.Position2Cell(transform.position);
    }

    public bool OnEmptyCell()
    {
        return Grid.Instance.CheckCellEmpty(cell);
    }

    public void PlacePreview()
    {
        Grid.Instance.Fill(this, cell, true);
    }

    public void PlaceOnGrid()
    {
        // TODO: create animation by DoTween
        transform.parent = Grid.Instance.transform;
        transform.localPosition = Grid.Instance.Cell2LocalPosition(cell);
        Grid.Instance.Fill(this, cell);
    }
}
