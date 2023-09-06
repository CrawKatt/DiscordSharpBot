using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

namespace DiscordSharpBot.commands;

public class TestCommands: BaseCommandModule
{
    [Command("ping")]
    public async Task Ping(CommandContext ctx)
    {
        await ctx.Channel.SendMessageAsync("Pong!");
    }
    
    [Command("variables")]
    public async Task Variables(CommandContext ctx)
    {
        await ctx.Channel.SendMessageAsync("variables");
    }
}