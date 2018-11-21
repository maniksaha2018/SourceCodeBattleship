using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.Domain
{
    public class Board
    {
        public List<BoardCoordinate> AvailableCoordinate { get; set; }
        public List<BoardCoordinate> HittedCoordinate { get; set; }

        public Board() {
            AvailableCoordinate = new List<Domain.BoardCoordinate>();
            HittedCoordinate = new List<BoardCoordinate>();
        }

        public void ShowAllCoordinate()
        {
            Console.WriteLine("Show all available coordinate:");
            int count = 1;
            AvailableCoordinate.ForEach(c => {
                Console.WriteLine(count.ToString() + " " + c.ShowCoordinate());
                count++;
            });
        }
    }
}
