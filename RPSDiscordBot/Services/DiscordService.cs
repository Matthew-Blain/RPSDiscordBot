using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace RPSDiscordBot;

public class DiscordService
{
    private readonly DiscordSocketClient _client;
    private readonly InteractionService _interactions;
    private readonly IConfiguration _config;

    public DiscordService()
    {
        _client = new DiscordSocketClient(new DiscordSocketConfig
        {
            GatewayIntents = GatewayIntents.Guilds
        });

        _interactions = new InteractionService(_client);

#if DEBUG
        _config = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.Local.json")
                    .Build();
#endif
        //_config = new ConfigurationBuilder()
        //    .AddJsonFile("appsettings.json")
        //    .Build();
    }

    public async Task StartAsync()
    {
        _client.Ready += ClientReady;
        _client.InteractionCreated += InteractionCreated;

        await _interactions.AddModulesAsync(Assembly.GetExecutingAssembly(), null);

        await _client.LoginAsync(TokenType.Bot, _config["Bot:Token"]);
        await _client.StartAsync();

        await Task.Delay(-1);
    }

    private async Task ClientReady()
    {
        await _interactions.RegisterCommandsGloballyAsync(true);
    }

    private async Task InteractionCreated(SocketInteraction interaction)
    {
        var context = new SocketInteractionContext(_client, interaction);

        var result = await _interactions.ExecuteCommandAsync(context, null);

        if (!result.IsSuccess && interaction.Type == InteractionType.ApplicationCommand)
        {
            await interaction.GetOriginalResponseAsync();
        }
    }
}