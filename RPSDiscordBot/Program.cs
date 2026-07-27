using RPSDiscordBot;

internal class Program
{
    static async Task Main(string[] args)
    {
        try
        {
            await new DiscordService().StartAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            Console.ReadKey();
        }
    }
}