using System;
using UnityEngine;
using UnityEngine.Serialization;

public class BoardGenerator : MonoBehaviour
{
    [SerializeField] private Transform _tilePrefab;
    [SerializeField] private Transform _boardParent;
    [SerializeField] private SpriteProvider _spriteProvider;

    public Tile[,] Tiles { get; set; }
    public int Width { get; private set; } = 7;
    public int Height { get; private set; } = 7;

    private Animations _animations;

    private float _spacing = 1.1f;

    private void Awake()
    {
        _animations = GetComponent<Animations>();
        Tiles = new Tile[Width, Height];
    }

    public void GenerateTilesBoard()
    {
        var offsetX = (Width - 1) / 2f;
        var offsetY = (Height - 1) / 2f;
        
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                var tile = Instantiate(_tilePrefab, _boardParent);
                Tiles[x, y] = tile.gameObject.GetComponent<Tile>();
                Sprite sprite = _spriteProvider.GetRandomSprite(Tiles, x, y);
                Tiles[x, y].Init(sprite, x, y);
                _animations.PlaySpawnAnimation(tile.GetComponent<Tile>().TileSpriteRenderer);

                tile.localPosition = new Vector3(
                    (x - offsetX) * _spacing,
                    (y - offsetY) * _spacing,
                    0);
            }
        }
    }
}