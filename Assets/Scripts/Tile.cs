using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class Tile : MonoBehaviour
{
    [field:SerializeField] public SpriteRenderer TileSpriteRenderer;
    public Sprite CurrentSprite { get; private set; }
    public int TileX { get; set; }
    public int TileY { get; set; }

    public void Init(Sprite sprite, int x, int y)
    {
        TileSpriteRenderer.sprite = sprite;
        TileSpriteRenderer.transform.localScale = Vector3.zero;
        CurrentSprite = sprite;

        TileX = x;
        TileY = y;
    }
}