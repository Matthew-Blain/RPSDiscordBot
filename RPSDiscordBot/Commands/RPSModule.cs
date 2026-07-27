using Discord;
using Discord.Interactions;
using RPSDiscordBot.Helpers;
using RPSDiscordBot.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPSDiscordBot.Commands
{
    public class RPSModule : InteractionModuleBase<SocketInteractionContext>
    {
        private readonly RpsService _rpsService = new RpsService();

        [EnabledInDm(true)]
        [SlashCommand("rps", "Play Rock Paper Scissors")]
        public async Task Rps(IUser opponent)
        {
            var result = _rpsService.Play(Context.User.Id, opponent.Id);

            var embed = new EmbedBuilder()
                        .WithTitle("🪨 Rock Paper Scissors")
                        .AddField(Context.User.Username,
                            $"{EmojiHelper.Get(result.PlayerMove)} {result.PlayerMove}", true)
                        .AddField(opponent.Username,
                            $"{EmojiHelper.Get(result.OpponentMove)} {result.OpponentMove}", true)
                        .WithDescription(result.WinnerId == Context.User.Id
                            ? $"🏆 {Context.User.Mention} wins!"
                            : $"🏆 {opponent.Mention} wins!")
                        .WithColor(Color.Blue)
                        .Build();

            await RespondAsync(embed: embed);
        }
    }
}
