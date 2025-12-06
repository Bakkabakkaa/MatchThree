using System;
using UnityEngine;

public class SwapMechanics : MonoBehaviour
{
    [SerializeField] private LayerMask _candyLayer;

    private Camera _camera;
    private Tile _selectedTile;
    private Vector2 _startMousePosition;

    private BoardGenerator _boardGenerator;
    private Animations _animations;

    private void Awake()
    {
        _camera = Camera.main;
        
        _boardGenerator = GetComponent<BoardGenerator>();
        _animations = GetComponent<Animations>();
    }

    private void Update()
    {
        Vector2 mouse = _camera.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(mouse, Vector2.zero, Mathf.Infinity, _candyLayer);
            _startMousePosition = mouse;
            
            if (hit.collider != null)
            {
                _selectedTile = hit.collider.GetComponent<Tile>();
                Debug.Log($"Select X {_selectedTile.TileX}, Y {_selectedTile.TileY}");
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (_selectedTile != null)
            {
                Vector2 endPosition = mouse;
                float distance = Vector2.Distance(_startMousePosition, endPosition);
                
                if (distance < 0.3f) // порог в мировых координатах
                {
                    _selectedTile = null;
                    return; // просто клик, без перемещения
                    
                }
                var swapDirection = GetSwapDirection(_startMousePosition, endPosition);
                
                Debug.Log($"Select X {_selectedTile.TileX}, Y {_selectedTile.TileY}");
                int neighborX = _selectedTile.TileX + swapDirection.x;
                int neighborY = _selectedTile.TileY + swapDirection.y;

                if (CheckingArrayBounds(neighborX, neighborY))
                {
                    var neighbor = _boardGenerator.Tiles[neighborX, neighborY];
                    Debug.Log($"Neighbor X {neighbor.TileX}, Y {neighbor.TileY}");
                    SwapTiles(_selectedTile, neighbor);
                }
                
                _selectedTile = null;
            }
        }
    }

    private Vector2Int GetSwapDirection(Vector2 startPosition, Vector2 endPosition)
    {
        var delta = endPosition - startPosition;

        if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
        {
            return delta.x > 0 ? Vector2Int.right : Vector2Int.left;
        }
        else
        {
            return delta.y > 0 ? Vector2Int.up : Vector2Int.down; 
        }
    }

    private void SwapTiles(Tile selectedTile, Tile neighbor)
    {
        (_boardGenerator.Tiles[selectedTile.TileX, selectedTile.TileY], _boardGenerator.Tiles[neighbor.TileX, neighbor.TileY])
            = (_boardGenerator.Tiles[neighbor.TileX, neighbor.TileY], _boardGenerator.Tiles[selectedTile.TileX, selectedTile.TileY]);
        
        (selectedTile.TileX, neighbor.TileX) = (neighbor.TileX, selectedTile.TileX);
        (selectedTile.TileY, neighbor.TileY) = (neighbor.TileY, selectedTile.TileY);

        _animations.PlaySwapAnimation(selectedTile.TileSpriteRenderer, neighbor.TileSpriteRenderer, () =>
        {
            // ПОСЛЕ АНИМАЦИИ: МЕНЯЕМ МИРОВЫЕ ПОЗИЦИИ ТАЙЛОВ (КОЛЛАЙДЕРЫ ПЕРЕЕЗЖАЮТ)
            (selectedTile.transform.position, neighbor.transform.position) =
                (neighbor.transform.position, selectedTile.transform.position);

            // Локальные позиции спрайтов уже сброшены в анимации
            // Здесь можно запускать проверку матчей, откат, и т.д.
        });
    }

    private bool CheckingArrayBounds(int neighborX, int neighborY )
    {
        return neighborX >= 0 && neighborX < _boardGenerator.Width &&
               neighborY >= 0 && neighborY < _boardGenerator.Height;
    }
}