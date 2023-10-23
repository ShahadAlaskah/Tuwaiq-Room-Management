using Microsoft.Extensions.Diagnostics.HealthChecks;
using MySql.Data.MySqlClient;

namespace API.HealthChecks;

public class SqlServerHealthCheck : IHealthCheck
{
    private readonly string _connectionString;

    public SqlServerHealthCheck(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection") ??
                            throw new ArgumentNullException(nameof(configuration));
    }

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        try
        {
            await using var connection = new MySqlConnection(_connectionString);
            await connection.OpenAsync(cancellationToken);

            await using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT 1";
                await command.ExecuteScalarAsync(cancellationToken);
            }

            return HealthCheckResult.Healthy();
        }
        catch (Exception ex)
        {
            return new HealthCheckResult(context.Registration.FailureStatus, exception: ex,
                description: "SQL Connection Error");
        }
    }
}