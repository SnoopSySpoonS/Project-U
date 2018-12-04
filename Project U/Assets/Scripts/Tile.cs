using System;
using System.Collections.Generic;
using System.Linq;
using Project_U.Assets.Scripts.Pathfinding;
using UnityEngine;

namespace Project_U.Assets.Scripts
{
    public class Tile : ITile, IEquatable<Tile>
    {
        public Tile(float column, float row, float height = 0, params ITile[] nabours)
        {
            Position = new Position(column, row, height);
            Nabours = nabours.ToList();
        }
        private Position position;
        private List<ITile> nabours = new List<ITile>();
        private List<IUnit> units = new List<IUnit>();
        public Position Position
        {
            get { return position; }

            set { position = value; }
        }

        public float CostToEnter(IUnit unit = null)
        {
            return 1 + Position.RandomMargin;
        }

        public bool Equals(Tile other)
        {
            if (other.Position == this.Position) 
                { return true; }
            return false;
        }

        public List<ITile> Nabours
        {
            get { return nabours; }
            set { nabours = value; }
        }

        public List<IUnit> Units
        {
            get { return units; }
            set { units = value; }
        }
    }
}
