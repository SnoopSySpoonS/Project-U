namespace Project_U.Assets.Scripts.Pathfinding
{
    public interface IUnit
    {
         ITile PositionedTile {get; set;}         
         float GetEnterCost(ITile tile);
         float BaseSpeed {get; set;}
         float RemainingSpeed {get; set;}
    }
}
