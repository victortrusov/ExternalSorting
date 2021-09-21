using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CommandLine;
using ExternalSorting.Cli.Configuration;
using ExternalSorting.Cli.Output;
using ExternalSorting.Core;

namespace ExternalSorting.Cli
{
    class Program
    {
        static async Task Main(string[] args) =>
            await Parser.Default
                .ParseArguments<GenerateOptions, SortOptions>(args)
                .MapResult(
                    (GenerateOptions opts) => Generate(opts),
                    (SortOptions opts) => Sort(opts),
                    errors => Task.CompletedTask
                );

        private static async Task Generate(GenerateOptions options)
        {
            IConsoleWriter console = new ConsoleWriter();
            console.WriteLine("Starting generation...");

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var generator = new Generator(options.FileSize * 1024 * 1024, options.FilePath);
            await generator.Generate();

            stopwatch.Stop();
            console.WriteLine($"Generation completed in {stopwatch.Elapsed.ToString("mm\\:ss\\.fff")}");
        }

        private static async Task Sort(SortOptions options)
        {
            await Task.CompletedTask;
        }
    }
}
