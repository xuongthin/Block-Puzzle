using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Blocks Setting", menuName = "Blocks Setting")]
public class BlocksSetting : ScriptableObject
{
    public float size;
    public Vector2 offset;
    public BlocksData[] blocksData;
}

[System.Serializable]
public class BlocksData
{
    public BlocksType type;
    public long bitMask;
    public int bitMaskLength;
    public Vector2 bitMaskPosition;
    public Vector2[] encodedPositions;
}

public enum BlocksType
{
    I1,
    I2,
    I3,
    I4,
    I5,
    L2,
    L3,
    L33,
    T,
    O2,
    O3
}