namespace Domain.Domains;

public class AuditLog
{
    public long Id { get; set; }

    public string Event { get; set; } = null!;

    public string Source { get; set; }= null!;

    public string Category { get; set; }= null!;

    public string SubjectIdentifier { get; set; }= null!;

    public string SubjectName { get; set; }= null!;

    public string SubjectType { get; set; }= null!;

    public string SubjectAdditionalData { get; set; }= null!;

    public string Action { get; set; }= null!;

    public string Data { get; set; }= null!;

    public DateTime Created { get; set; } = DateTime.Now;
}