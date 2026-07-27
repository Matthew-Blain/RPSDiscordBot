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
        _config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
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
        try
        {
            await _interactions.RegisterCommandsGloballyAsync(true);
            Console.WriteLine("Commands registered successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    private async Task InteractionCreated(SocketInteraction interaction)
    {
        try
        {
            var context = new SocketInteractionContext(_client, interaction);

            var result = await _interactions.ExecuteCommandAsync(context, null);

            Console.WriteLine($"Success: {result.IsSuccess}");

            if (!result.IsSuccess)
            {
                Console.WriteLine(result.Error);
                Console.WriteLine(result.ErrorReason);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}