using System;
using System.Collections.Generic;
using UnityEngine;

public class MatchFinder : MonoBehaviour
{
    private BoardGenerator _boardGenerator;
    private MatchHandler _matchHandler;

    private void Awake()
    {
        _boardGenerator = GetComponent<BoardGenerator>();
        _matchHandler = GetComponent<MatchHandler>();
    }

    public bool HasMatchesAfterSwap(Tile firstTile, Tile secondTile)
    {
        return HasMatchesAt(firstTile) || HasMatchesAt(secondTile);
    }

    private bool HasMatchesAt(Tile tile)
    {
        List<Tile> horizontalLine = new List<Tile> { tile };

        // Влево
        for (int x = tile.TileX - 1; x >= 0; x--)
        {
            if (_boardGenerator.Tiles[x, tile.TileY].TileSpriteRenderer.sprite ==
                _boardGenerator.Tiles[x + 1, tile.TileY].TileSpriteRenderer.sprite)
            {
                horizontalLine.Add(_boardGenerator.Tiles[x, tile.TileY]);
            }
            else break;
        }

        // Вправо
        for (int x = tile.TileX + 1; x < _boardGenerator.Width; x++)
        {
            if (_boardGenerator.Tiles[x, tile.TileY].TileSpriteRenderer.sprite ==
                _boardGenerator.Tiles[x - 1, tile.TileY].TileSpriteRenderer.sprite)
            {
                horizontalLine.Add(_boardGenerator.Tiles[x, tile.TileY]);
            }
            else break;
        }

        if (horizontalLine.Count >= 3)
        {
            _matchHandler.MatchedTiles.AddRange(horizontalLine);
            return true;
        }

        // Проверка по вертикали
        List<Tile> verticalLine = new List<Tile> { tile };

        // Вниз
        for (int y = tile.TileY - 1; y >= 0; y--)
        {
            if (_boardGenerator.Tiles[tile.TileX, y].TileSpriteRenderer.sprite ==
                _boardGenerator.Tiles[tile.TileX, y + 1].TileSpriteRenderer.sprite)
            {
                verticalLine.Add(_boardGenerator.Tiles[tile.TileX, y]);
            }
            else break;
        }

        // Вверх
        for (int y = tile.TileY + 1; y < _boardGenerator.Height; y++)
        {
            if (_boardGenerator.Tiles[tile.TileX, y].TileSpriteRenderer.sprite ==
                _boardGenerator.Tiles[tile.TileX, y - 1].TileSpriteRenderer.sprite)
            {
                verticalLine.Add(_boardGenerator.Tiles[tile.TileX, y]);
            }
            else break;
        }

        if (verticalLine.Count >= 3)
        {
            _matchHandler.MatchedTiles.AddRange(verticalLine);
            return true;
        }

        return false;
    }
}