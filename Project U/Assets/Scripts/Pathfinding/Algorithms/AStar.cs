using System;
using System.Collections.Generic;
using  Project_U.Assets.Scripts.Pathfinding.Priority_Queue;
namespace Project_U.Assets.Scripts.Pathfinding.Algorithms
{
    public class AStar : IAlgorithm
    {
        public AStar() { }

        SimplePriorityQueue<ITile> openNodes;        
        SimplePriorityQueue<ITile> closedNodes;
        
        Queue<ITile> Path;

        public List<ITile> CalculatePath(ITile start, ITile goal, CostEstimateDelegate costEstimateFunction, IUnit unit = null)
        {
            openNodes = new SimplePriorityQueue<ITile>();
            closedNodes = new SimplePriorityQueue<ITile>();
            Path = new Queue<ITile>();
            Dictionary<ITile, ITile> cameFrom = new Dictionary<ITile, ITile>();
            Dictionary<ITile, float> gScore = new Dictionary<ITile, float>();
            Dictionary<ITile, float> fScore = new Dictionary<ITile, float>();
            gScore[start] = 0;
            gScore[start] = costEstimateFunction(start, goal);
            ITile current;
            
            openNodes.Enqueue(start, 0);
            
            while (openNodes.Count > 0)
            {
                current = openNodes.Dequeue();
                if (current == goal) 
                    { return ReconstructPath(cameFrom, current); }
                closedNodes.Enqueue(current, costEstimateFunction(current, goal));
                foreach (ITile neighbor in current.GetNabours())
                {
                    float costToEnter = neighbor.CostToEnter(unit);
                    if (costToEnter < 0) // negative entercost means unit is not able to move there
                        { continue; }
                    float tentativeGScore = gScore[current] + neighbor.CostToEnter(unit);
                    if (openNodes.Contains(neighbor) == false)
                        { openNodes.Enqueue(neighbor, costEstimateFunction(neighbor, goal)); }
                    else if (tentativeGScore >= gScore[neighbor])
                        { continue; }
                    cameFrom[neighbor] = current;
                    gScore[neighbor] = tentativeGScore;
                    fScore[neighbor] = gScore[neighbor] + costEstimateFunction(neighbor, goal);
                } 
            }
            return new List<ITile>{};
        }
            
        public List<ITile> ReconstructPath(Dictionary<ITile, ITile> cameFrom, ITile current)
        {
            List<ITile> path = new List<ITile>{ current };
            while (cameFrom.ContainsKey(current))
            { 
                current = cameFrom[current]; 
                path.Add(current);
            }
            return path;
        }
    }
}
