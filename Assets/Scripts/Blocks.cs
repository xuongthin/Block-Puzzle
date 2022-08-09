using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Blocks : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] private Transform virtualTransform;
    private BlocksSetting setting;
    private List<Block> blockList;

    [SerializeField] private CanvasGroup canvasGroup;
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

        virtualTransform.position = transform.position;

        color = (BlockColor)Random.Range(0, 7);

        bool[,] matrix = shape.matrix;
        float maxX = 0.0f, maxY = 0.0f;
        float minX = 1000.0f, minY = 1000.0f;

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (matrix[i, j])
                {
                    Block block = Pools.Instance.GetBlock();
                    block.transform.parent = virtualTransform;
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
        CheckPlayability();
    }

    public void CheckPlayability()
    {
        isPlayable = Grid.Instance.CheckBlockPlayability(shape);
        SetBlocksStatus(isPlayable);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.position = (eventData.position + setting.offset);
        BlocksManager.Instance.SetMovingBlocks(this);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = (eventData.position + setting.offset);
        blockList[0].CheckOnCell();
        Vector2Int cellPosition = blockList[0].Cell;
        if (cellPosition != previousCellPosition)
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
            previousCellPosition = cellPosition;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (isPlacable)
        {
            isPlacable = false;
            isPlayed = true;
            while (blockList.Count > 0)
            {
                blockList[0].PlaceOnGrid();
                blockList.RemoveAt(0);
            }
            GameManager.Instance.OnBlockPlaced();
        }
        else
        {
            // TODO: return to init position
            transform.position = initPosition;
        }
    }

    private void Recenter(Vector3 offset)
    {
        virtualTransform.position -= offset;
        foreach (Block block in blockList)
            block.transform.parent = transform;
    }

    private void SetBlocksStatus(bool active)
    {
        canvasGroup.alpha = active ? 1 : 0.5f;
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

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (shape != null)
        {
            bool[,] matrix = shape.matrix;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Color color = matrix[i, j] ? Color.green : Color.gray;
                    Gizmos.color = color;
                    Vector3 position = transform.position + new Vector3((i - 2) * setting.size, (j - 2) * setting.size, 0);
                    Gizmos.DrawWireCube(position, Vector3.one * setting.size * 0.8f);
                }
            }
        }
    }
#endif
}
