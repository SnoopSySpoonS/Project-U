using System.Collections.Generic;

namespace Project_U.Assets.Scripts.Pathfinding
{
    public interface IWorld
    {
        Dictionary<Position, ITile> Tiles {get; set;}
        void AddTile(ITile newTile, List<ITile> nabours);
        void RemoveTile(ITile removeTile);
    }
}
