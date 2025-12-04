using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemsSet", menuName = "ScriptableObjects/ItemsSet", order = 50)]
public class ItemsSet : ScriptableObject
{
    [SerializeField] private Sprite[] _sprites;

    public Sprite GetRandomSprite()
    {
        var index = Random.Range(0, _sprites.Length);
        return _sprites[index];
    }
}
