using System.Collections.Generic;

namespace Project_U.Assets.Scripts.Pathfinding
{
    public delegate float CostEstimateDelegate(ITile start, ITile goal);
    public interface IAlgorithm
    {
        IWorld World { get; set; }
        List<ITile> CalculatePath(ITile origin, ITile destination, IUnit unit = null);
        float MovecostEstimate(ITile origin, ITile destination, IUnit unit = null);
    }
}
