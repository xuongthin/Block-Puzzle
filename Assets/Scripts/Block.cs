using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Block : PooledObject
{
    private Image image;

    public override void OnCreated(ObjectPool pool)
    {
        base.OnCreated(pool);
        image = GetComponent<Image>();
    }

    public void SetColor(BlockColor color)
    {
        Sprite sprite = GameManager.Instance.BlockSetting.sprites[((int)color)];
        image.sprite = sprite;
    }
}
