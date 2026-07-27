using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPSDiscordBot.Models
{
    public class RpsResult
    {
        public Move PlayerMove { get; set; }
        public Move OpponentMove { get; set; }
        public ulong WinnerId { get; set; }
    }
}
