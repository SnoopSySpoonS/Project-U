using System.Collections.Generic;
using Project_U.Assets.Scripts.Pathfinding;
using UnityEngine;

namespace Project_U.Assets.Scripts
{
    public class Tile : ITile
    {
        private Vector3 position;
        private List<ITile> nabours;
        public Vector3 Position
        {
            get
            {
                return position;
            }

            set
            {
                position = value;
            }
        }

        public float CostToEnter(IUnit unit = null)
        {
            return 1;
        }

        public List<ITile> Nabours
        {
            get {
                return nabours;
            }
            set 
            {
                nabours = value;
            }
        }
    }
}
