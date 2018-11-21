using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleShip.Domain;

namespace BattleShip.Service
{
    public interface IPlayGame
    {
        void DoSetup();
        void Fire(GamePlayer target);
        void ShowStatus();
        void PlayGame();
    }
}
