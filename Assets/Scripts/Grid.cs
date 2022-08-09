using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Grid : MonoBehaviour
{
    public static Grid Instance;
    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] private float cellSize;
    private float size;
    private Block[,] grid;
    private bool[,] fill;
    private bool[,] preview;
    private HashSet<int> checkListX;
    private HashSet<int> checkListY;
    private HashSet<int> fullfilListX;
    private HashSet<int> fullfilListY;

    private void Start()
    {
        size = cellSize * UIManager.Instance.UIScale;
        grid = new Block[8, 8];
        fill = new bool[8, 8];
        preview = new bool[8, 8];
        ResetPreview();
        checkListX = new HashSet<int>();
        checkListY = new HashSet<int>();
        fullfilListX = new HashSet<int>();
        fullfilListY = new HashSet<int>();

        GameManager.Instance.OnBlockPlaced += AfterFill;
    }

    public Vector2Int Position2Cell(Vector2 position)
    {
        position -= (Vector2)transform.position;

        if (position.x < 0 || position.y < 0)
            return Vector2Int.down;

        int x = (int)(position.x / size);
        int y = (int)(position.y / size);

        return new Vector2Int(x, y);
    }

    public bool CheckCellEmpty(Vector2Int cell)
    {
        if (cell.x < 0 || cell.x >= fill.GetLength(0)
         || cell.y < 0 || cell.y >= fill.GetLength(1))
            return false;
        return !fill[cell.x, cell.y];
    }

    public Vector2 Cell2LocalPosition(Vector2Int cell)
    {
        return new Vector2(size / 2 + cell.x * size, size / 2 + cell.y * size);
    }

    public void Fill(Block block, Vector2Int cell, bool temporary = false)
    {
        if (temporary)
        {
            // Block block = Pools.Instance.
            preview[cell.x, cell.y] = true;
            checkListX.Add(cell.x);
            checkListY.Add(cell.y);
        }
        else
        {
            grid[cell.x, cell.y] = block;
            fill[cell.x, cell.y] = true;
        }
    }

    public bool CheckBlockPlayability(Shape shape)
    {
        bool[,] matrix = shape.matrix;
        Vector4Int space = shape.space;
        for (int i = -2; i <= 5; i++)
        {
            for (int j = -2; j <= 5; j++)
            {
                if (!CheckMatrixOverlap(matrix, new Vector2Int(i, j)))
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void CheckPreview()
    {
        BlockColor color = BlocksManager.Instance.MovingBlocks.BlockColor;

        foreach (int x in checkListX)
        {
            if (CheckColumnFilled(true, x))
            {
                fullfilListX.Add(x);
                for (int y = 0; y < 8; y++)
                {
                    if (grid[x, y] != null)
                        grid[x, y].SetColor(color, true);
                }
            }
        }

        foreach (int y in checkListY)
        {
            if (CheckRowFilled(true, y))
            {
                fullfilListY.Add(y);
                for (int x = 0; x < 8; x++)
                {
                    if (grid[x, y] != null)
                        grid[x, y].SetColor(color, true);
                }
            }
        }
    }

    public void ClearPreview()
    {
        foreach (int x in fullfilListX)
            for (int y = 0; y < 8; y++)
            {
                if (grid[x, y] != null)
                    grid[x, y].SetColor();
            }

        fullfilListX.Clear();

        foreach (int y in fullfilListY)
            for (int x = 0; x < 8; x++)
            {
                if (grid[x, y] != null)
                    grid[x, y].SetColor();
            }

        fullfilListY.Clear();
        ResetPreview();
    }

    public void AfterFill()
    {
        foreach (int x in fullfilListX)
        {
            for (int y = 0; y < 8; y++)
            {
                grid[x, y].ReturnToPool();
                grid[x, y] = null;
                fill[x, y] = false;
            }
        }

        foreach (int y in fullfilListY)
        {
            for (int x = 0; x < 8; x++)
            {
                if (fill[x, y])
                {
                    grid[x, y].ReturnToPool();
                    grid[x, y] = null;
                    fill[x, y] = false;
                }
            }
        }

        ResetPreview();
    }

    private void ResetPreview()
    {
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                preview[x, y] = fill[x, y];
            }
        }
    }

    private bool CheckMatrixOverlap(bool[,] matrix, Vector2Int shift)
    {
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                int x = i + shift.x;
                int y = j + shift.y;

                if (0 < x && x < fill.GetLength(0) && 0 < y && y < fill.GetLength(1))
                {
                    if (matrix[i, j] && fill[x, y])
                        return true;
                }
                else if (matrix[i, j])
                {
                    return true;
                }
            }
        }

        return false;
    }

    private bool CheckRowFilled(bool checkPreview, int y)
    {
        bool[,] matrix = checkPreview ? preview : fill;
        for (int x = 0; x < matrix.GetLength(0); x++)
        {
            if (!matrix[x, y])
                return false;
        }
        return true;
    }

    private bool CheckColumnFilled(bool checkPreview, int x)
    {
        bool[,] matrix = checkPreview ? preview : fill;
        for (int y = 0; y < matrix.GetLength(1); y++)
        {
            if (!matrix[x, y])
                return false;
        }
        return true;
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Vector3 startPosition = transform.position + new Vector3(cellSize / 2, cellSize / 2, 0);
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (fill[i, j])
                    Gizmos.color = Color.red;
                else
                    Gizmos.color = Color.blue;

                Vector3 position = startPosition + new Vector3(i * cellSize, j * cellSize);
                Gizmos.DrawWireCube(position, new Vector3(cellSize, cellSize, 0) * 0.9f);
            }
        }
    }
#endif
}

[CustomEditor(typeof(Grid))]
public class GridEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
    }
}