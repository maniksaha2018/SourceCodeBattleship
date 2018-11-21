using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleShip.Domain;
using BattleShip.Service;

namespace BattleShip
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Strating Battleship.....");
            BattleShip.Service.BattleShipGame Game = new BattleShipGame(new BattleShip.Service.GameSetup());
            Console.WriteLine("Doing Battleship.....");
            Game.DoSetup();
            Game.PlayGame();
            Game.ShowStatus();
            Console.ReadKey();
        }
    }
}
