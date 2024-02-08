// ----------------------------------------
// Created by Jirre Verkerk
// given out for educational purposes
// ----------------------------------------
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Zelda.Internal
{
    [CreateAssetMenu(menuName = "2D/Tiles/Multi Match Tile", fileName = "MultiMatchTile")]
    public class MultiMatchTile : RuleTile<RuleTile.TilingRuleOutput.Neighbor>
    {
        public List<TileBase> _Matches;
        
        public override bool RuleMatch(int neighbor, TileBase tile) {
            switch (neighbor) 
            {
                case TilingRuleOutput.Neighbor.This: return tile == this || _Matches.Contains(tile);
                case TilingRuleOutput.Neighbor.NotThis: return tile != this && !_Matches.Contains(tile);
            }

            return false;
        }
    }
}
