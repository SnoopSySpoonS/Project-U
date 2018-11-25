using System.Collections.Generic;

namespace Project_U.Assets.Scripts.Pathfinding
{
    public delegate float CostEstimateDelegate(ITile start, ITile goal);
    public interface IAlgorithm
    {
        List<ITile> CalculatePath(ITile origin, ITile destination, CostEstimateDelegate costEstimateFunction, IUnit unit = null);
         
    }
}
