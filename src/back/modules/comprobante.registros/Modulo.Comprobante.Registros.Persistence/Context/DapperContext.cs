using Npgsql;
using System.Data;

namespace Modulo.Comprobante.Registros.Persistence.Context;

internal sealed class DapperContext : IDisposable
{
    public NpgsqlConnection Connection { get; }

    public DapperContext(string connectionString)
    {
        this.Connection = new NpgsqlConnection(connectionString);
    }

    public void Dispose()
    {
        if (this.Connection is not null)
            this.Connection.Dispose();
    }
}