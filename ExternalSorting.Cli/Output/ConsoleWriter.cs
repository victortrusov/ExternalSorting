using System;

namespace ExternalSorting.Cli.Output
{
    public class ConsoleWriter : IConsoleWriter
    {
        public void WriteLine(string? str) =>
            Console.WriteLine(str);
    }
}
