using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Blocks Setting", menuName = "Blocks Setting")]
public class BlocksSetting : ScriptableObject
{
    public float size;
    public Vector2 offset;
}