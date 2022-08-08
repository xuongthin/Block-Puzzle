using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Block Setting", menuName = "Block Setting")]
public class BlockSetting : ScriptableObject
{
    public Sprite[] sprites;
}

public enum BlockColor
{
    Blue,
    Cyan,
    Green,
    Orange,
    Purple,
    Red,
    Yellow,
}