using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.Domain
{
    public class BattleShip
    {
        public BattleShip()
        {
            OccupiedCoordinte = new List<Domain.BoardCoordinate>();
            HittedCoordinte = new List<Domain.BoardCoordinate>();
        }
        public BoardCoordinate StartingCoordinate { get; set; }

        public bool IsVertical { get; set; }

        public int Length { get; set; }

        public List<BoardCoordinate> OccupiedCoordinte { get; set; }

        public List<BoardCoordinate> HittedCoordinte { get; set; }

    }
}
