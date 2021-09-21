using CommandLine;

namespace ExternalSorting.Cli.Configuration
{
    [Verb("sort", HelpText = "Sort file")]
    public record SortOptions
    {

        [Option('f', "file", HelpText = "Path to the file", Required = true)]
        public string FilePath { get; init; } = "";

    }
}
