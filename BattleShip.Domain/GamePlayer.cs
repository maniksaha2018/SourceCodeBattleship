using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.Domain
{
    public class GamePlayer
    {
        public PlayerInfo Player { get; set; }
        public Board GameBoard { get; set; }
        public List<BattleShip> Fleet { get; set; }

        public GamePlayer()
        {
            Fleet = new List<Domain.BattleShip>();
        }
    }
}
