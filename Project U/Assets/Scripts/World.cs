using System;
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
            get { return tiles; }
            set { tiles = value; }
        }
        
        public Tile GetTile(Position position) 
        {
            return (Tile)Tiles[position];
        }
        public Tile GetTile(float column, float row, float height = 0) 
        {
            return (Tile)Tiles[new Position(column, row, height)];
        }
        
        float IWorld.TileDistanceEstimate(ITile origin, ITile goal, IUnit unit)
        {
            Position diff = new Position();
            diff.Row = Math.Abs(origin.Position.Row - goal.Position.Row);
            diff.Column = Math.Abs(origin.Position.Column - goal.Position.Column);
            diff.Height = Math.Abs(origin.Position.Height - goal.Position.Height);
            return diff.Row + diff.Column + diff.Height;
        }

        public void AddTile(ITile newTile, params ITile[] nabours)
        {
            if (tiles.ContainsKey(newTile.Position))
                { throw new System.Exception("There already exists a tile at position (" + newTile.Position + ")!"); }
            tiles[newTile.Position] = newTile;
            if (nabours == null)
                { nabours = newTile.Nabours.ToArray(); }
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

        ITile IWorld.GetTile(Position position)
        {
            return GetTile(position);
        }

        public void AddNabourship(Position tilePosition, params Position[] nabourPositions)
        {
            if (Tiles.ContainsKey(tilePosition) == false)
                { return; }
            Tile origin = GetTile(tilePosition);
            foreach (Position pos in nabourPositions)
            {
                if (tilePosition == pos)
                    { continue; }
                if (Tiles.ContainsKey(pos) == false)
                    { continue; }
                Tile nabour = GetTile(pos);
                if (origin.Nabours.Contains(nabour))
                    { continue; }
                origin.Nabours.Add(nabour);
                nabour.Nabours.Add(origin);
            }
        }

        ITile IWorld.GetTile(float column, float row, float height)
        {
            return GetTile(column, row, height);
        }
    }
}
