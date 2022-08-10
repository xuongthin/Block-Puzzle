using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class Blocks : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] private Transform container;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Image image;
    private BlocksSetting setting;
    private List<Block> blockList;
    private Shape shape;
    private BlockColor color;
    private Vector3 initPosition;
    private Vector2Int previousCellPosition;
    private bool isPlayable;
    private bool isPlacable;
    private bool isPlayed;

    public bool IsPlayable => isPlayable;
    public bool IsPlayed => isPlayed;
    public BlockColor BlockColor => color;


    private void Start()
    {
        blockList = new List<Block>();
        setting = GameManager.Instance.BlocksSetting;
    }

    public void Init(Shape shape)
    {
        previousCellPosition = Vector2Int.down;
        this.shape = shape;
        initPosition = transform.position;
        isPlayed = false;

        container.localPosition = Vector3.zero;

        color = (BlockColor)Random.Range(0, 7);

        bool[,] matrix = shape.matrix;
        float maxX = -1000.0f, maxY = -1000.0f;
        float minX = 1000.0f, minY = 1000.0f;

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (matrix[i, j])
                {
                    Block block = Pools.Instance.GetBlock();
                    block.transform.parent = container;
                    Vector3 position = new Vector2(i, j) * setting.size;
                    block.transform.localPosition = position;
                    block.SetColor(color);
                    blockList.Add(block);

                    if (position.x > maxX)
                        maxX = position.x;
                    if (position.x < minX)
                        minX = position.x;

                    if (position.y > maxY)
                        maxY = position.y;
                    if (position.y < minY)
                        minY = position.y;
                }
            }
        }

        Vector3 offset = new Vector2((minX + maxX) / 2, (minY + maxY) / 2);
        Recenter(offset);
        container.localScale = Vector3.one * setting.onBoardScale;
        image.raycastTarget = true;
        CheckPlayability();
    }

    public void CheckPlayability()
    {
        isPlayable = Grid.Instance.CheckBlockPlayability(shape);
        SetBlocksStatus(isPlayable);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        HightLightBlocks();
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;

        if (CheckMovement())
        {
            if (CheckPlacability())
            {
                isPlacable = true;
                RefreshPreview();
            }
            else
            {
                isPlacable = false;
                ClearPreview();
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (isPlacable)
        {
            PlaceBlocks();
            GameManager.Instance.OnBlockPlaced();
        }
        else
        {
            ResetBlock();

        }

        ClearPreview();
    }

    private void Recenter(Vector3 offset)
    {
        foreach (Block block in blockList)
            block.transform.localPosition -= offset;
    }

    private void SetBlocksStatus(bool active)
    {
        canvasGroup.alpha = active ? 1 : 0.5f;
    }

    private bool CheckMovement()
    {
        blockList[0].CheckOnCell();
        Vector2Int cellPosition = blockList[0].Cell;
        if (cellPosition != previousCellPosition)
        {
            previousCellPosition = cellPosition;
            return true;
        }
        return false;
    }

    private bool CheckPlacability()
    {
        foreach (Block block in blockList)
        {
            block.CheckOnCell();
            if (!block.OnEmptyCell())
                return false;
        }
        return true;
    }

    private void HightLightBlocks()
    {
        transform.SetAsLastSibling();
        container.DOLocalMove(setting.offset, 0.25f);
        // container.localScale *= 2;
        container.DOScale(Vector3.one, 0.25f);
        BlocksManager.Instance.SetMovingBlocks(this);
    }

    private void RefreshPreview()
    {
        ClearPreview();
        foreach (Block block in blockList)
            block.PlacePreview();

        Grid.Instance.CheckPreview();
    }

    private void ClearPreview()
    {
        Grid.Instance.ClearPreview();
    }

    private void PlaceBlocks()
    {
        isPlacable = false;
        isPlayable = false;
        isPlayed = true;
        image.raycastTarget = false;
        GameManager.Instance.AddScore(blockList.Count);
        while (blockList.Count > 0)
        {
            blockList[0].PlaceOnGrid();
            blockList.RemoveAt(0);
        }
    }

    private void ResetBlock()
    {
        image.raycastTarget = false;
        // transform.position = initPosition;
        transform.DOMove(initPosition, 0.25f);
        // container.localPosition -= (Vector3)setting.offset;
        container.DOLocalMove(Vector3.zero, 0.25f);
        // container.localScale *= 0.5f;
        container.DOScale(Vector3.one * setting.onBoardScale, 0.25f).OnComplete(() => image.raycastTarget = true);
    }
}
