using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.Domain
{
    public class BoardCoordinate : IEquatable<BoardCoordinate>
    {
        public int PointX { get; set; }
        public int PointY { get; set; }

        public string ShowCoordinate()
        {
            string str = "(" + PointX.ToString() + "," + PointY.ToString() + ")";
            return str;
        }

        public bool Equals(BoardCoordinate other)
        {
            if (ReferenceEquals(other, null))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return PointX.Equals(other.PointX) && PointY.Equals(other.PointY);
        }

        public override int GetHashCode()
        {
            int hashpointX = PointX.GetHashCode();
            int hashpointY = PointY.GetHashCode();

            return hashpointX ^ hashpointY;
        }
    }
}
