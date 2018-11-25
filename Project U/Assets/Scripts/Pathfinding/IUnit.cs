namespace Project_U.Assets.Scripts.Pathfinding
{
    public interface IUnit
    {
         ITile CurrentTile {get; set;}         
         float GetEnterCost(ITile tile);
    }
}
