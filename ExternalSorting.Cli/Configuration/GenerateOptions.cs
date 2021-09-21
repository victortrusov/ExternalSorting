using CommandLine;

namespace ExternalSorting.Cli.Configuration
{
    [Verb("generate", HelpText = "Generate example file")]
    public record GenerateOptions
    {
        private const string DefaultFilePath = "Example.txt";
        private const int DefaultFileSize = 10;


        [Option('f', "file", Default = DefaultFilePath, HelpText = "Path to the file")]
        public string FilePath { get; init; } = DefaultFilePath;


        [Option('s', "size", Default = DefaultFileSize, HelpText = "Size of the creating file in MB")]
        public int FileSize { get; init; } = DefaultFileSize;

    }
}
