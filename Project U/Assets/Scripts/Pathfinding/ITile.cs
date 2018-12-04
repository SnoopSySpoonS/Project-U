using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project_U.Assets.Scripts.Pathfinding
{
    public interface ITile
    {
        Position Position {get; set; }
        List<ITile> Nabours {get; set;}
        List<IUnit> Units {get; set;}
        float CostToEnter(IUnit unit = null);
        
        bool Equals(object obj);
        int GetHashCode();
        
    }
    
    public class Position: IEquatable<Position>
    {
        public Position(float column = 0, float row = 0, float height = 0)
        {
            Column = column;
            Row = row;
            Height = height;
            RandomMargin = UnityEngine.Random.Range(0.0000000000001f, 0.0000000001f);
        }
        public float Column;
        public float Row;
        public float Height;
        public float RandomMargin;
        public Position DeltaTo(Position destination)
        {
            Position delta = new Position();
            delta.Column = Math.Abs(this.Column - destination.Column);
            delta.Row = Math.Abs(this.Row - destination.Row);
            delta.Height = Math.Abs(this.Height - destination.Height);
            return delta;
        }
        
        public float Scalar()
        {
            return (this.Row + this.Column + this.Height) / 3;
        }
        
        public float ScalarWithMargin()
        {
            return this.Scalar() + RandomMargin;
        }
        
        
        // override object.Equals
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
                        
            Position cmp = (Position)obj;
            if (this.Column == cmp.Column && this.Row == cmp.Row && this.Height == cmp.Height) 
                { return true; }
                
            return base.Equals (obj);
        }
        
        // override object.GetHashCode
        public override int GetHashCode()
        {
            // TODO: write your implementation of GetHashCode() here
            return (int)Column ^ (int)Row ^ (int)Height;
        }

        public bool Equals(Position other)
        {
            return Equals((object)other);
        }

        public static bool operator == (Position lhs, Position rhs)
        {
            // Check for null on left side.
            if (object.ReferenceEquals(lhs, null))
            {
                if (object.ReferenceEquals(rhs, null))
                {
                    // null == null = true.
                    return true;
                }

                // Only the left side is null.
                return false;
            }
            // Equals handles case of null on right side.
            return lhs.Equals(rhs);
        }
        public static bool operator !=(Position lhs, Position rhs)
        {
            return !(lhs == rhs);
        }
    }
}
