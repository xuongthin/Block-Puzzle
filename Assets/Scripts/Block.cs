using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Block : PooledObject
{
    [SerializeField] private Image image;
    private BlockColor color;
    private BlockColor tempColor;
    private Vector2Int cell;
    public Vector2Int Cell => cell;
    public BlockColor Color => color;

    public void SetColor()
    {
        SetColor(color, false);
    }

    public void SetColor(BlockColor color, bool temporary = false)
    {
        if (!temporary)
        {
            this.color = color;
            tempColor = color;
        }
        else
            tempColor = color;

        Sprite sprite = GameManager.Instance.BlockSetting.sprites[((int)color)];
        image.sprite = sprite;
    }

    public void CheckOnCell(bool quickCheck = false)
    {
        cell = Grid.Instance.Position2Cell(transform.position, quickCheck);
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
        transform.parent = Grid.Instance.transform;
        Vector3 targetLocalPosition = Grid.Instance.Cell2LocalPosition(cell);
        transform.DOLocalMove(targetLocalPosition, 0.25f);
        Grid.Instance.Fill(this, cell);
    }

    public override void ReturnToPool()
    {
        BlockParticle particle = Pools.Instance.GetBlockParticle();
        particle.transform.position = transform.position;
        particle.SetColor(tempColor);
        particle.Play();
        transform.DOScale(Vector3.zero, 0.25f).OnComplete(() =>
        {
            base.ReturnToPool();
            transform.localScale = Vector3.one;
        });
    }
}
