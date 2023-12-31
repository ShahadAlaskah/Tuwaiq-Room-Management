

// ReSharper disable TemplateIsNotCompileTimeConstantProblem

namespace Shared.Logging;

public interface ILoggerAdapter<T>
{
    void LogInformation(string message);
    void LogInformation<T0>(string message, T0 args0);
    void LogInformation<T0, T1>(string message, T0 args0, T1 args1);
    void LogInformation<T0, T1, T2>(string message, T0 args0, T1 args1, T2 args2);
    void LogDebug(string message);
    void LogDebug<T0>(string message, T0 args0);
    void LogDebug<T0, T1>(string message, T0 args0, T1 args1);
    void LogDebug<T0, T1, T2>(string message, T0 args0, T1 args1, T2 args2);
    void LogWarning(string message);
    void LogWarning<T0>(string message, T0 args0);
    void LogWarning<T0, T1>(string message, T0 args0, T1 args1);
    void LogWarning<T0, T1, T2>(string message, T0 args0, T1 args1, T2 args2);
    void LogError(Exception ex, string message);
    void LogError<T0>(Exception ex, string message, T0 args0);
    void LogError<T0, T1>(Exception ex, string message, T0 args0, T1 args1);
    void LogError<T0, T1, T2>(Exception ex, string message, T0 args0, T1 args1, T2 args2);
}