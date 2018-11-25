using  Project_U.Assets.Scripts.Pathfinding.Algorithms;
namespace Project_U.Assets.Scripts.Pathfinding
{
    public class Pathfinding
    {
        private float Cost(ITile origin, ITile goal, IUnit unit)
        {
            return 1;
        }
        public IAlgorithm algo = new AStar();
    }
}
