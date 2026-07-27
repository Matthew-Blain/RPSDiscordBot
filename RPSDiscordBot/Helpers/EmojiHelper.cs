using RPSDiscordBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPSDiscordBot.Helpers
{
    public static class EmojiHelper
    {
        public static string Get(Move move)
        {
            return move switch
            {
                Move.Rock => "🪨",
                Move.Paper => "📄",
                Move.Scissors => "✂️",
                _ => ""
            };
        }
    }
}
