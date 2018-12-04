using System;
using System.Collections.Generic;
using  Project_U.Assets.Scripts.Pathfinding.Priority_Queue;
using UnityEngine;

namespace Project_U.Assets.Scripts.Pathfinding.Algorithms
{
    public class AStar : IAlgorithm
    {
        public AStar(IWorld world) 
        {
            this.world = world;
        }

        SimplePriorityQueue<ITile> openNodes;
        SimplePriorityQueue<ITile> closedNodes;
        
        Queue<ITile> Path;
        
        IWorld world;

        public IWorld World
        {
            get { return world; }
            set { world = value; }
        }

        public List<ITile> CalculatePath(ITile start, ITile goal, IUnit unit = null)
        {
            openNodes = new SimplePriorityQueue<ITile>();
            closedNodes = new SimplePriorityQueue<ITile>();
            Path = new Queue<ITile>();
            Dictionary<ITile, ITile> cameFrom = new Dictionary<ITile, ITile>();
            Dictionary<ITile, float> gScore = new Dictionary<ITile, float>();
            Dictionary<ITile, float> fScore = new Dictionary<ITile, float>();
            gScore[start] = 0;
            gScore[start] = MovecostEstimate(start, goal);
            ITile current;
            
            openNodes.Enqueue(start, 0);
            
            while (openNodes.Count > 0)
            {
                current = openNodes.Dequeue();
                if (current == goal) 
                    { return ReconstructPath(cameFrom, current); }
                closedNodes.Enqueue(current, MovecostEstimate(current, goal));
                foreach (ITile neighbor in current.Nabours)
                {
                    float costToEnter = neighbor.CostToEnter(unit);
                    if (costToEnter < 0) // negative entercost means unit is not able to move there
                        { continue; }
                    float tentativeGScore = gScore[current] + neighbor.CostToEnter(unit);
                    if (openNodes.Contains(neighbor) == false)
                        { openNodes.Enqueue(neighbor, MovecostEstimate(neighbor, goal)); }
                    else if (tentativeGScore >= gScore[neighbor])
                        { continue; }
                    cameFrom[neighbor] = current;
                    gScore[neighbor] = tentativeGScore;
                    fScore[neighbor] = gScore[neighbor] + MovecostEstimate(neighbor, goal);
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

        public float MovecostEstimate(ITile origin, ITile destination, IUnit unit = null)
        {
            float estimate = origin.Position.DeltaTo(destination.Position).ScalarWithMargin();
            //Debug.Log("From (" + origin.Position.Column + ", " + origin.Position.Row + ") to (" + destination.Position.Column + ", " + destination.Position.Row + ") the estimated cost is: " + estimate);
            return estimate;
        }
    }
}
