using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MatchHandler : MonoBehaviour
{
    public List<Tile> MatchedTiles { get; private set; }
    
    private Animations _animations;

    private void Awake()
    {
        _animations = GetComponent<Animations>();
        MatchedTiles = new List<Tile>();
    }

    public void DestroyMatches()
    {
         var matchTiles = MatchedTiles.Distinct().ToList();
        
        foreach (var tile in matchTiles)
        {
            _animations.PlayDisappearance(tile);
        }
        
        MatchedTiles.Clear();
    }
}