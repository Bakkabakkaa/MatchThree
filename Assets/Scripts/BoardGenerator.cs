using System;
using UnityEngine;

public class BoardGenerator : MonoBehaviour
{
    [SerializeField] private Transform _tilePrefab;
    [SerializeField] private Transform _boardParent;
    [SerializeField] private SpriteProvider _spriteProvider;

    private Animations _animations;
        
    private int _width = 7;
    private int _height = 7;
    private float _spacing = 1.1f;
    private Tile[,] _tiles;

    private void Awake()
    {
        _animations = GetComponent<Animations>();
        _tiles = new Tile[_width, _height];
    }

    public void GenerateTilesBoard()
    {
        var offsetX = (_width - 1) / 2f;
        var offsetY = (_height - 1) / 2f;
        
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                var tile = Instantiate(_tilePrefab, _boardParent);
                _tiles[x, y] = tile.gameObject.GetComponent<Tile>();
                Sprite sprite = _spriteProvider.GetRandomSprite(_tiles, x, y);
                _tiles[x, y].Init(sprite);
                _animations.PlaySpawnAnimation(tile.GetComponent<Tile>().TileSpriteRenderer);

                tile.localPosition = new Vector3(
                    (x - offsetX) * _spacing,
                    (y - offsetY) * _spacing,
                    0);
            }
        }
    }
}