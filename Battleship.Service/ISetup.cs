using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleShip.Domain;

namespace BattleShip.Service
{
    public interface ISetup
    {
        PlayerInfo CreatePlayer();
        Board CreateBoard();
        bool AddBattleShip(List<BattleShip.Domain.BattleShip> fleet);
    }
}
