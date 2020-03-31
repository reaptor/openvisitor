using System;
using System.Collections.Concurrent;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;

namespace OpenVisitor.Client
{
    public class BrowserConsoleLogger<T> : BrowserConsoleLogger, ILogger<T>
    {
        public BrowserConsoleLogger(IJSRuntime runtime)
            : base(runtime)
        {

        }
    }

    public class BrowserConsoleLogger : ILogger
    {
        private readonly IJSRuntime _runtime;

        public BrowserConsoleLogger(IJSRuntime runtime)
        {
            _runtime = runtime;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }
            _runtime.InvokeVoidAsync("console.log", $"{formatter(state, exception)}\n{exception}");
        }

        public bool IsEnabled(LogLevel logLevel) => true;

        public IDisposable BeginScope<TState>(TState state) => null!;
    }

    public class BrowserConsoleLoggerProvider : ILoggerProvider
    {
        private readonly Func<string, LogLevel, bool> _filter;
        private readonly IJSRuntime _runtime;
        private ConcurrentDictionary<string,BrowserConsoleLogger> _loggers = new ConcurrentDictionary<string, BrowserConsoleLogger>();

        public BrowserConsoleLoggerProvider(IJSRuntime runtime, Func<string, LogLevel, bool> filter)
        {
            _runtime = runtime;
            _filter = filter;
        }

        public ILogger CreateLogger(string categoryName)
        {
            if (string.IsNullOrWhiteSpace(categoryName))
            {
                throw new ArgumentNullException(nameof(categoryName));
            }
            return _loggers.GetOrAdd(categoryName, CreateLoggerImplementation);
        }

        public void Dispose() => _loggers.Clear();

        private BrowserConsoleLogger CreateLoggerImplementation(string name) => new BrowserConsoleLogger(_runtime);

    }

    public static class BrowserConsoleLoggerFactoryExtensions
    {
        public static ILoggingBuilder AddBrowserConsole(this ILoggingBuilder builder)
        {
            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, BrowserConsoleLoggerProvider>());

            // HACK: Override the hardcoded ILogger<> injected by Blazor
            builder.Services.Add(ServiceDescriptor.Singleton(typeof(ILogger<>), typeof(BrowserConsoleLogger<>)));

            return builder;
        }
    }
}