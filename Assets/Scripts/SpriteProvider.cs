using UnityEngine;

[CreateAssetMenu(fileName = "SpriteProvider", menuName = "ScriptableObjects/SpriteProvider", order = 50)]
public class SpriteProvider : ScriptableObject
{
    [SerializeField] private ItemsSet _itemsSet;

    public Sprite GetRandomSprite(Tile[,] tiles, int x, int y)
    {
        Sprite chosenSprite = _itemsSet.GetRandomSprite();

        if (x >= 2 && tiles[x - 1, y].CurrentSprite == chosenSprite
                   && tiles[x - 2, y].CurrentSprite == chosenSprite)
        {
            return GetRandomSprite(tiles, x, y);
        }

        if (y >= 2 && tiles[x, y - 1].CurrentSprite == chosenSprite
                   && tiles[x, y - 2].CurrentSprite == chosenSprite)
        {
            return GetRandomSprite(tiles, x, y);
        }

        return chosenSprite;
    }
}