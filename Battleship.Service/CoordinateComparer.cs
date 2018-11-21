using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleShip.Domain;

namespace BattleShip.Service
{
    public class CoordinateComparer:IEqualityComparer<BoardCoordinate>
    {
        public bool Equals(BoardCoordinate x, BoardCoordinate y)
        {
            return ((x.PointX == y.PointX) && (x.PointY == y.PointY));
        }

        public int GetHashCode(BoardCoordinate obj)
        {
            int hashpointX = obj.PointX.GetHashCode();
            int hashpointY = obj.PointY.GetHashCode();

            return hashpointX ^ hashpointY;
        }
    }
}
