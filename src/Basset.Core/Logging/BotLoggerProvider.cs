using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace Basset.Logging
{
    public class BotLoggerProvider : ILoggerProvider
    {
        private readonly LoggingConfig _options;

        public BotLoggerProvider(LoggingConfig options)
        {
            _options = options;
        }

        public ILogger CreateLogger(string categoryName)
        {
            string outputDirectory = _options.UseRelativeOutput
                ? Path.Combine(AppContext.BaseDirectory, _options.OutputDirectory)
                : _options.OutputDirectory;

            return new BotLogger(categoryName, outputDirectory, _options.DateTimeFormat, _options.MaxFileSizeKb);
        }

        public void Dispose()
        {
            return;
        }
    }
}