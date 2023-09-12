using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class Direction
    {
        public readonly static Direction Left = new Direction(0, -1);
        public readonly static Direction Right = new Direction(0, 1);
        public readonly static Direction Up = new Direction(-1, 0);
        public readonly static Direction Down = new Direction(1, 0);

        public int RowOffSet { get; }
        public int ColumnOffSet { get; }

        private Direction(int rowOffSet, int columnOffSet)
        {
            RowOffSet = rowOffSet;
            ColumnOffSet = columnOffSet;
        }

        public Direction Opposite()
        {
            return new Direction(-RowOffSet, -ColumnOffSet);
        }

        public override bool Equals(object obj)
        {
            return obj is Direction direction &&
                   RowOffSet == direction.RowOffSet &&
                   ColumnOffSet == direction.ColumnOffSet;
        }

        public override int GetHashCode()
        {
            int hashCode = 945188820;
            hashCode = hashCode * -1521134295 + RowOffSet.GetHashCode();
            hashCode = hashCode * -1521134295 + ColumnOffSet.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(Direction left, Direction right)
        {
            return EqualityComparer<Direction>.Default.Equals(left, right);
        }

        public static bool operator !=(Direction left, Direction right)
        {
            return !(left == right);
        }
    }
}
