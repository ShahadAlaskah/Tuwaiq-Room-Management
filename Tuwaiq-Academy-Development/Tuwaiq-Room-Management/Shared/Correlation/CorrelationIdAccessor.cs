namespace Shared.Correlation;

public class CorrelationIdAccessor : ICorrelationIdAccessor
{
    private static readonly AsyncLocal<string?> _correlationId = new AsyncLocal<string?>();

    public string? CorrelationId
    {
        get => _correlationId .Value;
        set => _correlationId .Value = value;
    }
}