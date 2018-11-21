using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleShip.Domain;

namespace BattleShip.Service
{
    public class BattleShipGame : IPlayGame
    {
        private GamePlayer Player1;
        private GamePlayer Player2;
        private ISetup PlayerSetUp;
        

        public BattleShipGame (ISetup palyersetup)
        {
            PlayerSetUp = palyersetup;
        }

        public void Fire(GamePlayer target)
        {
            bool IsAHit = false;
            BoardCoordinate SelectedCoordinate = null;
            target.GameBoard.ShowAllCoordinate();
            bool bNotValid = true;
            int selectedindex = 0;
            while (bNotValid)
            {
                Console.WriteLine("Choose Coordinate to Fire (1 -" + target.GameBoard.AvailableCoordinate.Count().ToString() + ")");
                while (!int.TryParse(Console.ReadLine(), out selectedindex))
                {
                    Console.WriteLine("Please select a valid coordinate.");
                }
                if (selectedindex >= 0 && selectedindex <= target.GameBoard.AvailableCoordinate.Count())
                {
                    bNotValid = false;
                    SelectedCoordinate = target.GameBoard.AvailableCoordinate[selectedindex - 1];
                }
                else
                {
                    Console.WriteLine("Please select a valid coordinate.");
                }
            }

            target.Fleet.ForEach(s =>
            {
                if (s.OccupiedCoordinte.Contains(SelectedCoordinate) && (!(s.HittedCoordinte.Contains(SelectedCoordinate))))
                {
                    s.HittedCoordinte.Add(SelectedCoordinate);
                    IsAHit = true;
                }
            });

            if (IsAHit)
            {
                Console.WriteLine("HIT");
            }
            else
            {
                Console.WriteLine("MISSED");
            }

        }

        private void PrintPlayerStatus(GamePlayer player)
        {
            Console.WriteLine("Summary Status");
            Console.WriteLine("Player:" + player.Player.Name);
            Console.WriteLine("==================================");
            int count = 1;
            int sinkCount = 0;
            string status = string.Empty;
            player.Fleet.ForEach(f =>
            {
                status = string.Empty;
                if (f.HittedCoordinte.Count == f.OccupiedCoordinte.Count)
                {
                    status = "Sink";
                    sinkCount++;
                }
                else if (f.HittedCoordinte.Count > 0)
                {
                    status = "Fighting But Got Hit";
                }
                else
                {
                    status = "Fighting";
                }
                Console.WriteLine("Ship " + count.ToString() + "---------------------" + status);
                count++;
            });
            if (sinkCount == player.Fleet.Count)
            {
                Console.WriteLine("Player : " + player.Player.Name + " lost the fight");
            }
            else
            {
                Console.WriteLine("Player : " + player.Player.Name + " still in fight");
            }
        }

        public void ShowStatus()
        {
            PrintPlayerStatus(Player1);
            PrintPlayerStatus(Player2);
        }

        public void DoSetup()
        {
            Console.WriteLine("Input For Player 1...");
            Player1 = new GamePlayer();
            Player1.Player = PlayerSetUp.CreatePlayer();
            Player1.GameBoard = PlayerSetUp.CreateBoard();
            PrepareFleet(Player1);
            Console.WriteLine("Input For Player 2...");
            Player2 = new GamePlayer();
            Player2.Player = PlayerSetUp.CreatePlayer();
            Player2.GameBoard = PlayerSetUp.CreateBoard();
            PrepareFleet(Player2);
        }

        private void PrepareFleet( GamePlayer player )
        {
            bool bContinue = true;
            while(bContinue)
            {
                Console.WriteLine("Add new ship...");
                PlayerSetUp.AddBattleShip(player.Fleet);
                string res = string.Empty;
                Console.WriteLine("Do you want to add another ship (y/n)?");
                res = Console.ReadLine();
                if (res.ToUpper() == "Y") bContinue = true;
                else bContinue = false;
            }
        }

        public void PlayGame()
        {
            bool bcontinue = true;
            string res = string.Empty;
            while (bcontinue)
            {
                Console.WriteLine("Turn for Player1....");
                Fire(Player2);
                Console.WriteLine("Turn for Player2....");
                Fire(Player1);
                Console.WriteLine("Want to continue? (Y/N)..");
                res = Console.ReadLine();
                if (res.ToUpper() == "Y")
                {
                    bcontinue = true;
                }
                else if (res.ToUpper() == "N")
                {
                    bcontinue = false;
                }
                else
                {
                    bcontinue = true;
                }
            }
        }
    }
}
