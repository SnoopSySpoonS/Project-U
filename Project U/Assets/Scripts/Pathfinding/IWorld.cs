using System.Collections.Generic;

namespace Project_U.Assets.Scripts.Pathfinding
{
    public interface IWorld
    {
        Dictionary<Position, ITile> Tiles {get; set;}
        ITile GetTile(Position position);
        ITile GetTile(float column, float row, float height = 0);
        float TileDistanceEstimate(ITile origin, ITile goal, IUnit unit = null);
        void AddTile(ITile newTile, params ITile[] nabours);
        void AddNabourship(Position tilePosition, params Position[] nabourPositions);
        void RemoveTile(ITile removeTile);
    }
}
