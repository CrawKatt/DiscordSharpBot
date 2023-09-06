using DiscordSharpBot.commands;
using DiscordSharpBot.commands.SlashCommands;
using DiscordSharpBot.config;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.SlashCommands;

namespace DiscordSharpBot
{
    internal class Program
    {
        private static DiscordClient? Client { get; set; }
        private static CommandsNextExtension? Commands { get; set; }
        static async Task Main(string[] args)
        {
            var jsonReader = new JsonReader();
            await jsonReader.ReadJson();

            var discordConfig = new DiscordConfiguration()
            {
                Intents = DiscordIntents.All,
                Token = jsonReader.Token,
                TokenType = TokenType.Bot,
                AutoReconnect = true,
            };

            Client = new DiscordClient(discordConfig);
            
            Client.Ready += Client_Ready;
            
            var slashCommandsConfig = Client.UseSlashCommands();

            var prefix = jsonReader.Prefix ?? "defaultPrefix";
            
            var commandsConfig = new CommandsNextConfiguration
            {
                StringPrefixes = new [] { prefix },
                EnableMentionPrefix = true,
                EnableDms = false,
                EnableDefaultHelp = false,
            };

            Commands = Client.UseCommandsNext(commandsConfig);

            // Prefix Commands
            Commands.RegisterCommands<TestCommands>();
            
            // Slash Commands
            slashCommandsConfig.RegisterCommands<TestSl>();

            await Client.ConnectAsync();
            await Task.Delay(-1);
        }

        private static Task Client_Ready(DiscordClient sender, DSharpPlus.EventArgs.ReadyEventArgs args)
        {
            return Task.CompletedTask;
        }
    }
}