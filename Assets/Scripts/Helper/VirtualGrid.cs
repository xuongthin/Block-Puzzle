using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VirtualGrid : MonoBehaviour
{
    public enum Corner
    {
        Center,
        LowerLeft,
        UpperLeft
    }

    [SerializeField] private int rowCount = 2;
    [SerializeField] private int columnCount = 2;
    [SerializeField] private Vector2 cellSize = new Vector2(10, 10);
    [SerializeField] private Vector2 spacing = new Vector2(1, 1);
    [SerializeField] private Corner startCorner = Corner.LowerLeft;
    private Vector2 scaledCellSize;
    private Vector2 scaledSpacing;
    private Vector2 cellSpace;
    private Vector2 lowerLeftOffset;

    private void Start()
    {
        ApplyScale();
        SetCornerOffset();
    }

    private void OnValidate()
    {
        ApplyScale();
        SetCornerOffset();
    }

    private void ApplyScale()
    {
        scaledCellSize = cellSize * transform.lossyScale;
        scaledSpacing = spacing * transform.lossyScale;
        cellSpace = scaledCellSize + scaledSpacing;
    }

    private void SetCornerOffset()
    {

        switch (startCorner)
        {
            case Corner.Center:
                lowerLeftOffset = new Vector2(-cellSpace.x * ((float)columnCount / 2)
                                                 , -cellSpace.y * ((float)rowCount / 2));
                break;
            case Corner.UpperLeft:
                lowerLeftOffset = new Vector2(0, -cellSpace.y * rowCount);
                break;
            case Corner.LowerLeft:
            default:
                lowerLeftOffset = Vector2.zero;
                break;
        }
    }

    public Vector2Int Position2CellId(Vector2 position)
    {
        Vector2 targetLocalPosition = position - (Vector2)transform.position;
        Vector2 distanceWithLowerLeft = targetLocalPosition - lowerLeftOffset;

        // check if pointer is out of grid
        if (distanceWithLowerLeft.x < 0 || distanceWithLowerLeft.y < 0)
            return -Vector2Int.one;

        int x = (int)(distanceWithLowerLeft.x / cellSpace.x);
        int y = (int)(distanceWithLowerLeft.y / cellSpace.y);

        if (x >= columnCount || y >= rowCount)
            return Vector2Int.down;

        // check if pointer is on space between cells
        Vector2Int cellId = new Vector2Int(x, y);
        Vector2 cellPosition = CellId2Position(cellId);
        if (position.OutOfRect(cellPosition, scaledCellSize))
            return Vector2Int.left;

        return cellId;
    }

    public Vector2 CellId2Position(Vector2Int cellId)
    {
        if (cellId.x < 0 || cellId.y < 0)
            return Vector2.zero;

        Vector2 lowerLeftPos = (Vector2)transform.position + lowerLeftOffset;

        Vector2 cellPosition = lowerLeftPos;
        cellPosition.x += (cellId.x + 0.5f) * cellSpace.x;
        cellPosition.y += (cellId.y + 0.5f) * cellSpace.y;

        return cellPosition;
    }

#if UNITY_EDITOR
    [SerializeField] private Color cellColor = Color.white;
    [SerializeField] private Color cellSpaceColor = Color.white;

    private void OnDrawGizmosSelected()
    {
        cellSpace = scaledCellSize + scaledSpacing;

        for (int x = 0; x < columnCount; x++)
        {
            for (int y = 0; y < rowCount; y++)
            {
                Vector2 cellPos = lowerLeftOffset + (Vector2)transform.position + cellSpace / 2;
                cellPos.x += cellSpace.x * x;
                cellPos.y += cellSpace.y * y;

                Gizmos.color = cellColor;
                Gizmos.DrawWireCube(cellPos, scaledCellSize);
                Gizmos.color = cellSpaceColor;
                Gizmos.DrawWireCube(cellPos, cellSpace);
            }
        }
    }
#endif
}
