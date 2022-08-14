using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Particle Setting", menuName = "Particle Setting")]
public class ParticleSetting : ScriptableObject
{
    public ParticleSystem particle;
    public List<BlockSpriteSet> spriteSets;
}

[System.Serializable]
public class BlockSpriteSet
{
    public Sprite[] sprites;
}
