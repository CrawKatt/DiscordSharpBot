using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;

namespace DiscordSharpBot.commands.SlashCommands;

public class TestSl: ApplicationCommandModule
{
    
    [SlashCommand("test", "Test command")]
    public async Task TestSlashCommand
    (
        InteractionContext ctx,
        /*[Choice("Choice 1", "choice1")] [Option("string", "String option")]
        string text,*/
        [Option("Select", "MarkDown File", autocomplete: true /*required: true*/)]
        string markdownFileName
    )
    {
        await ctx.CreateResponseAsync
        (
            InteractionResponseType.ChannelMessageWithSource,
            new DiscordInteractionResponseBuilder()
                .WithContent("Starting Slash Command")
        );

        string folderPath = "./../../../docs/";
        string[] markdownFiles = Directory.GetFiles(folderPath, "*.md");
        
        if (markdownFiles.Length == 0 )
        {
            await ctx.Channel.SendMessageAsync($"No se encontró el archivo {markdownFileName}");
            return;
        }

        string? selectedFilePath = markdownFiles.FirstOrDefault
        (
            file =>
            string.Equals(Path.GetFileName(file), markdownFileName, StringComparison.OrdinalIgnoreCase)
        );

        if (selectedFilePath == null)
        {
            await ctx.Channel.SendMessageAsync($"No se encontró el archivo {selectedFilePath}");
            return;
        }
        
        string markdownContent;
        try
        {
            markdownContent = File.ReadAllText($"{selectedFilePath}.md");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al leer el archivo Markdown: {ex.Message}");
            //await ctx.Channel.SendMessageAsync($"Error al leer el archivo Markdown: {ex.Message}");
            return;
        }

        var embedMessage = new DiscordEmbedBuilder()
        {
            Title = markdownFileName,
            Description = markdownContent,
        };
        
        await ctx.Channel.SendMessageAsync(embed: embedMessage);
    }
}