using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockParticle : PooledObject
{
    [SerializeField] private ParticleSystem particle;
    [SerializeField] private ParticleSetting setting;

    public void SetColor(BlockColor color)
    {
        BlockSpriteSet spriteSet = setting.spriteSets[((int)color)];
        for (int i = 0; i < 3; i++)
        {
            particle.textureSheetAnimation.RemoveSprite(0);
            particle.textureSheetAnimation.AddSprite(spriteSet.sprites[i]);
        }
    }

    public void Play()
    {
        particle.Play();
    }

    private void OnParticleSystemStopped()
    {
        ReturnToPool();
    }
}
