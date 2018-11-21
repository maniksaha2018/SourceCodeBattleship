using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleShip.Domain;
using System.IO;

namespace BattleShip.Service
{
    public class GameSetup : ISetup
    {
        private BoardCoordinate GetStartingCoordinate()
        {
            bool bNotValid = true;
            int X = 0;
            int Y = 0;
            while (bNotValid)
            {
                Console.WriteLine("Please input for X point (0-9).....");
                while (!int.TryParse(Console.ReadLine(), out X))
                {
                    Console.WriteLine("Please Enter a valid number between 0 and 9....");
                }
                if (X >= 0 && X <= 9)
                {
                    bNotValid = false;
                }
                else
                {
                    Console.WriteLine("Please Enter a valid number between 0 and 9....");
                }
            }
            bNotValid = true;
            while (bNotValid)
            {
                Console.WriteLine("Please input for Y point (0-9).....");
                while (!int.TryParse(Console.ReadLine(), out Y))
                {
                    Console.WriteLine("Please Enter a valid number between 0 and 9");
                }
                if (Y >= 0 && Y < 9)
                {
                    bNotValid = false;
                }
                else
                {
                    Console.WriteLine("Please Enter a valid number between 0 and 9");
                }
            }

            var point = new BoardCoordinate();
            point.PointX = X;
            point.PointY = Y;
            return point;
        }

        private BattleShip.Domain.BattleShip HandleForVertial()
        {
            bool bNotValid = true;
            int length = 0;
            var point = GetStartingCoordinate();
            int maxLength = 9 - point.PointY;
            if (maxLength > 1)
            {
                while (bNotValid)
                {
                    Console.WriteLine("Please input for length (1-" + maxLength.ToString() + ")..");
                    while (!int.TryParse(Console.ReadLine(), out length))
                    {
                        Console.WriteLine("Please Enter a valid number between 1 and "  + maxLength.ToString() );
                    }
                    if (length >= 1 && length <= maxLength)
                    {
                        bNotValid = false;
                    }
                    else
                    {
                        Console.WriteLine("Please Enter a valid number between 1 and "  + maxLength.ToString() );
                    }
                }
            }
            else
            {
                length = maxLength;
            }
            Domain.BattleShip ship = new Domain.BattleShip();
            ship.StartingCoordinate = point;
            ship.Length = length;
            ship.IsVertical = true;
            List<int> pointY = Enumerable.Range(point.PointY, length).ToList();
            pointY.ForEach(y =>
            {
                ship.OccupiedCoordinte.Add(new BoardCoordinate() { PointX = point.PointX, PointY = y });
            });

            return ship;
        }

        private BattleShip.Domain.BattleShip HandleForHorizontal()
        {
            bool bNotValid = true;
            int length = 0;
            var point = GetStartingCoordinate();
            int maxLength = 9 - point.PointX;
            if (maxLength > 1)
            {
                while (bNotValid)
                {
                    Console.Write("Please input for length (1-" + maxLength.ToString() + ") ?");
                    while (!int.TryParse(Console.ReadLine(), out length))
                    {
                        Console.WriteLine("Please Enter a valid number between 1 and " + maxLength.ToString());
                    }
                    if (length >= 1 && length <= maxLength)
                    {
                        bNotValid = false;
                    }
                    else
                    {
                        Console.WriteLine("Please Enter a valid number between 1 and " + maxLength.ToString());
                    }
                }
            }
            else
            {
                length = maxLength;
            }
            Domain.BattleShip ship = new Domain.BattleShip();
            ship.StartingCoordinate = point;
            ship.Length = length;
            ship.IsVertical = false;
            List<int> pointX = Enumerable.Range(point.PointX, length).ToList();
            pointX.ForEach(x =>
            {
                ship.OccupiedCoordinte.Add(new BoardCoordinate() { PointX = x, PointY = point.PointY });
            });

            return ship;
            
        }
        public bool AddBattleShip(List<Domain.BattleShip> fleet)
        {
            bool bAdded = false;
            List<BoardCoordinate> occupiedPoints = new List<BoardCoordinate>();
            fleet.ForEach(p => { occupiedPoints.AddRange(p.OccupiedCoordinte); });
            bool notDone = true;
            Domain.BattleShip AddedShip = null;
            while (notDone) // Loop indefinitely
            {
                Console.WriteLine("Is the new ship will be placed vertically(y/n, c for Cancel)?");
                string response = Console.ReadLine(); 
                switch(response.ToUpper())
                {
                    case "Y":
                        AddedShip = HandleForVertial();
                        notDone = false;
                        break;
                    case "N":
                        AddedShip = HandleForHorizontal();
                        notDone = false;
                        break;
                    case "C":
                        notDone = false;
                        break;
                    default:
                        Console.WriteLine("Please type {Y/y} for yes, {N/n} for no and {C/c} for cancel.");
                        break;
                }
                if (AddedShip != null)
                {
                    var result = occupiedPoints.Union(AddedShip.OccupiedCoordinte, new CoordinateComparer()).ToList();
                    if (result.Count == (occupiedPoints.Count + AddedShip.OccupiedCoordinte.Count))
                    {
                        fleet.Add(AddedShip);
                        Console.WriteLine("Addition Successful");
                        bAdded = true;
                    }
                    else
                    {
                        bAdded = false;
                        Console.WriteLine("New ship position is overlapping...");
                    }
                }
                
            }
            return bAdded;
        }

        public Board CreateBoard()
        {
            try
            {
                Board playerBoard = new Board();
                List<int> pointX = Enumerable.Range(0, 10).ToList();
                List<int> pointY = Enumerable.Range(0, 10).ToList();
                pointX.ForEach(x => {
                    pointY.ForEach(y =>
                    {
                        playerBoard.AvailableCoordinate.Add(new BoardCoordinate() { PointX = x, PointY = y });
                    });
                });
                Console.WriteLine("Game board Created with 10X10 matrix...");
                return playerBoard;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public PlayerInfo CreatePlayer()
        {
            bool bcontinue = true;
            try
            {
                PlayerInfo player = new PlayerInfo();
                while (bcontinue)
                {
                    Console.WriteLine("Enter Player Name...");
                    player.Name = Console.ReadLine();
                    bcontinue = string.IsNullOrEmpty(player.Name.Trim());
                }
                return player;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


    }
}
