using System.Collections.Generic;
using UnityEngine;

namespace Project_U.Assets.Scripts.Pathfinding
{
    public interface ITile
    {
        Position Position {get; set; }
        List<ITile> Nabours {get; set;}
        float CostToEnter(IUnit unit = null);
        
        bool Equals(object obj);
        int GetHashCode();
        
    }
    
    public class Position
    {
        public Position(float x = 0, float y = 0, float z = 0)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public float X;
        public float Y;
        public float Z; 
    }
}
