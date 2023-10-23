using System.Data.Common;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infrastructure.Persistence.Ef.Interceptors;

public class AuditReadInterceptor : DbCommandInterceptor
{
    public override InterceptionResult<DbDataReader> ReaderExecuting(
        DbCommand command,
        CommandEventData eventData,
        InterceptionResult<DbDataReader> result)
    {
        ModifyCommand(command);

        return result;
    }

    public override ValueTask<InterceptionResult<DbDataReader>> ReaderExecutingAsync(
        DbCommand command,
        CommandEventData eventData,
        InterceptionResult<DbDataReader> result,
        CancellationToken cancellationToken = default)
    {
        ModifyCommand(command);

        return new ValueTask<InterceptionResult<DbDataReader>>(result);
    }

    private static void ModifyCommand(DbCommand command)
    {
        Console.WriteLine(command.CommandText);
        foreach (DbParameter parameter in command.Parameters)
        {
            Console.WriteLine();
            Console.WriteLine(parameter.Value);
        }
        // if (command.CommandText.StartsWith("-- Apply OrderBy DESC", StringComparison.Ordinal))
        // {
        //     Console.WriteLine("DemoDbCommandInterceptor: Applying OrderBy DESC");
        //     // command.CommandText += " Order By 1 DESC";
        // }
    }
}