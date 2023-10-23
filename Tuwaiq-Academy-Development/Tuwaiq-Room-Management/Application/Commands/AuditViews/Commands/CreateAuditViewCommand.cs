using Shared.Enums;
using Shared.Interfaces;

namespace Application.Commands.AuditViews.Commands;

public class CreateAuditViewCommand : ICommand
{
    public CreateAuditViewCommand(AuditViewType type,Guid? userid, string? description = null, string[]? tables = null,
        Dictionary<string, object>? args = null)
    {
        Type = type;
        Userid = userid;
        Description = description;
        Tables = tables;
        Args = args;
    }

    public AuditViewType Type { get; }
    public Guid? Userid { get; }
    public string? Description { get; }
    public Dictionary<string, object>? Args { get; }
    public string[]? Tables { get; }
}