using Microsoft.Extensions.Logging;

namespace Shared.Logging;

public class LoggerAdapter<T> : ILoggerAdapter<T>
{
    private readonly ILogger<T> _logger;

    public LoggerAdapter(ILogger<T> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public void LogInformation(string message)
    {
        if (_logger.IsEnabled(LogLevel.Information))
            if (!string.IsNullOrEmpty(message))
                _logger.LogInformation(message);
    }

    public void LogInformation<T0>(string message, T0 args0)
    {
        if (_logger.IsEnabled(LogLevel.Information))
            if (!string.IsNullOrEmpty(message))
                _logger.LogInformation(message, args0);
    }

    public void LogInformation<T0, T1>(string message, T0 args0, T1 args1)
    {
        if (_logger.IsEnabled(LogLevel.Information))
            if (!string.IsNullOrEmpty(message))
                _logger.LogInformation(message, args0, args1);
    }

    public void LogInformation<T0, T1, T2>(string message, T0 args0, T1 args1, T2 args2)
    {
        if (_logger.IsEnabled(LogLevel.Information))
            if (!string.IsNullOrEmpty(message))
                _logger.LogInformation(message, args0, args1, args2);
    }

    public void LogDebug(string message)
    {
        if (_logger.IsEnabled(LogLevel.Debug))
            if (!string.IsNullOrEmpty(message))
                _logger.LogDebug(message);
    }

    public void LogDebug<T0>(string message, T0 args0)
    {
        if (_logger.IsEnabled(LogLevel.Debug))
            if (!string.IsNullOrEmpty(message))
                _logger.LogDebug(message, args0);
    }

    public void LogDebug<T0, T1>(string message, T0 args0, T1 args1)
    {
        if (_logger.IsEnabled(LogLevel.Debug))
            if (!string.IsNullOrEmpty(message))
                _logger.LogDebug(message, args0, args1);
    }

    public void LogDebug<T0, T1, T2>(string message, T0 args0, T1 args1, T2 args2)
    {
        if (_logger.IsEnabled(LogLevel.Debug))
            if (!string.IsNullOrEmpty(message))
                _logger.LogDebug(message, args0, args1, args2);
    }

    public void LogWarning(string message)
    {
        if (_logger.IsEnabled(LogLevel.Warning))
            if (!string.IsNullOrEmpty(message))
                _logger.LogWarning(message);
    }

    public void LogWarning<T0>(string message, T0 args0)
    {
        if (_logger.IsEnabled(LogLevel.Warning))
            if (!string.IsNullOrEmpty(message))
                _logger.LogWarning(message, args0);
    }

    public void LogWarning<T0, T1>(string message, T0 args0, T1 args1)
    {
        if (_logger.IsEnabled(LogLevel.Warning))
            if (!string.IsNullOrEmpty(message))
                _logger.LogWarning(message, args0, args1);
    }

    public void LogWarning<T0, T1, T2>(string message, T0 args0, T1 args1, T2 args2)
    {
        if (_logger.IsEnabled(LogLevel.Warning))
            if (!string.IsNullOrEmpty(message))
                _logger.LogWarning(message, args0, args1, args2);
    }

    public void LogError(Exception ex, string message)
    {
        if (_logger.IsEnabled(LogLevel.Error))
            if (!string.IsNullOrEmpty(message))
                _logger.LogError(ex, message);
    }

    public void LogError<T0>(Exception ex, string message, T0 args0)
    {
        if (_logger.IsEnabled(LogLevel.Error))
            if (!string.IsNullOrEmpty(message))
                _logger.LogError(ex, message, args0);
    }

    public void LogError<T0, T1>(Exception ex, string message, T0 args0, T1 args1)
    {
        if (_logger.IsEnabled(LogLevel.Error))
            if (!string.IsNullOrEmpty(message))
                _logger.LogError(ex, message, args0, args1);
    }

    public void LogError<T0, T1, T2>(Exception ex, string message, T0 args0, T1 args1, T2 args2)
    {
        if (_logger.IsEnabled(LogLevel.Error))
            if (!string.IsNullOrEmpty(message))
                _logger.LogError(ex, message, args0, args1, args2);
    }
}