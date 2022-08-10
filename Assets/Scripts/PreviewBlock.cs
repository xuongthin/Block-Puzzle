using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreviewBlock : PooledObject
{
    [SerializeField] private Image image;

    public void SetLocalPosition(Vector2 localPosition)
    {
        transform.localPosition = localPosition;
    }

    public void SetColor(BlockColor color)
    {
        Sprite sprite = GameManager.Instance.BlockSetting.sprites[((int)color)];
        image.sprite = sprite;
    }
}
