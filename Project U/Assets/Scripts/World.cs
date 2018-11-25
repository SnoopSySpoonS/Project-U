using System.Collections.Generic;
using Project_U.Assets.Scripts.Pathfinding;
using UnityEngine;

namespace Project_U.Assets.Scripts
{
    public class World : IWorld
    {
        Dictionary<Position, ITile> tiles = new Dictionary<Position, ITile>();
        public Dictionary<Position, ITile> Tiles
        {
            get
            {
                return tiles;
            }

            set
            {
                tiles = value;
            }
        }

        public void AddTile(ITile newTile, List<ITile> nabours = null)
        {
            if (tiles.ContainsKey(newTile.Position))
                { throw new System.Exception("There already exists a tile at position (" + newTile.Position + ")!"); }
            tiles[newTile.Position] = newTile;
            if (nabours == null)
                { nabours = newTile.Nabours; }
            foreach (ITile nabour in nabours)
            {
                nabour.Nabours.Add(newTile);
            }
        }

        public void RemoveTile(ITile removeTile)
        {
            if (tiles.ContainsKey(removeTile.Position) == false)
                { throw new System.Exception("There doesn't exist a tile at position (" + removeTile.Position + ")!"); }
            tiles.Remove(removeTile.Position);
            foreach (ITile nabour in removeTile.Nabours)
            {
                nabour.Nabours.Remove(removeTile);
            }
        }
    }
}
