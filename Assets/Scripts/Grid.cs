using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public static Grid Instance;
    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] private float cellSize;
    private long bitMask;
    private Block[] grid;
    private float size;

    private void Start()
    {
        grid = new Block[64];
        size = cellSize * UIManager.Instance.UIScale;
    }

    public int GetCellIdAt(Vector2 position)
    {
        position -= (Vector2)transform.position;

        if (position.x < 0 || position.y < 0)
            return -1;

        int x = (int)(position.x / size);
        int y = (int)(position.y / size);

        if (x > 7 || y > 7)
            return -1;

        return x + y * 8;
    }

    public Vector2 GetCellAt(Vector2 position)
    {
        int cellId = GetCellIdAt(position);
        int x = cellId % 8;
        int y = cellId / 8;
        return new Vector2(size / 2 + x * size, size / 2 * y * size);
    }

    public bool CheckValid(int cellId, long blockBitMask, int bitMaskLength)
    {
        if (cellId + bitMaskLength > 64)
            return false;

        blockBitMask = blockBitMask << cellId;
        return (bitMask & blockBitMask) == 0;
    }

    public bool CheckBlocksType(BlocksData data)
    {
        long blockBitMask = data.bitMask;
        int bitMaskLength = data.bitMaskLength;

        for (int i = 0; i <= 64 - bitMaskLength; i++)
        {
            if (CheckValid(i, blockBitMask, bitMaskLength))
                return true;
        }

        return false;
    }

    public bool CheckBlockPlayability(long blockBitMask)
    {
        long forward = blockBitMask;
        long backward = blockBitMask;

        while (forward >= 0)
        {
            forward = forward << 1;
            if ((bitMask & forward) == 0)
                return true;
        }

        while (backward >= 0)
        {
            if ((bitMask & backward) == 0)
                return true;

            backward = backward << -1;
        }

        return false;
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        float size = cellSize * 0.3268518f;
        Vector3 startPosition = transform.position + new Vector3(size / 2, size / 2, 0);
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                Vector3 position = startPosition + new Vector3(i * size, j * size);
                Gizmos.DrawWireCube(position, new Vector3(size, size, 0));
            }
        }
    }
#endif
}
