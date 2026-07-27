using RPSDiscordBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPSDiscordBot.Services
{
    public class RpsService
    {
        private const ulong NiallsUserID = 434852151475306496;
        private readonly Random _random = new();

        public RpsResult Play(ulong playerId, ulong opponentId)
        {
            RpsResult result = new RpsResult();

            //TODO: Add Logic Here ....

            return result;
        }

    }
}
