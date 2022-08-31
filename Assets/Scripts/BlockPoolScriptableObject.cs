using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "test")]
public class BlockPoolScriptableObject : RandomAccessListSO<Blocks, BlocksProbability>
{

}

[System.Serializable]
public class BlocksProbability : RALItem<Blocks>
{
    [Range(0, 100)] public float sss;
}