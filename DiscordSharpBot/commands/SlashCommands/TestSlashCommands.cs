using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using System;
using System.Threading.Tasks;

namespace DiscordSharpBot.commands.SlashCommands;

public class TestSlashCommands
{
    [SlashCommand("test", "Test command")]
    public async Task TestSlashCommand
    (
        InteractionContext ctx,
        [Choice("Choice 1", "choice1")] [Option("string", "String option")]
        string text
    )
    {
        await ctx.CreateResponseAsync
        (
            InteractionResponseType.ChannelMessageWithSource,
            new DiscordInteractionResponseBuilder()
                .WithContent("Starting Slash Command")
        );

        var embedMessage = new DiscordEmbedBuilder()
        {
            Title = text
        };
        
        await ctx.Channel.SendMessageAsync(embed: embedMessage);
    }
}