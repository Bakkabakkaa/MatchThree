using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class Tile : MonoBehaviour
{
    [field:SerializeField] public SpriteRenderer TileSpriteRenderer;
    public Sprite CurrentSprite { get; set; }

    public void Init(Sprite sprite)
    {
        TileSpriteRenderer.sprite = sprite;
        TileSpriteRenderer.transform.localScale = Vector3.zero;
        CurrentSprite = sprite; ;
    }
}