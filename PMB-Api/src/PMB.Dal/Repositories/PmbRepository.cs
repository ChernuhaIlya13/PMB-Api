using System;
using System.Data;
using System.Threading.Tasks;
using Npgsql;

namespace PMB.Dal.Repositories
{
    public abstract class PmbRepository : IDisposable
    {
        private readonly string _connectionString;
        private bool _disposed;
        
        public NpgsqlConnection Connection { get; private set; }
        
        public PmbRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<NpgsqlConnection> GetConnAsync()
        {
            if (Connection != null)
                return Connection;

            Connection = new NpgsqlConnection(_connectionString);
            await Connection.OpenAsync();
            
            Connection.Disposed += (_, _) => { Connection = null; };
            Connection.StateChange += (_, e) =>
            {
                if (e.CurrentState == ConnectionState.Closed)
                    Connection = null;
            };

            return Connection;
        }

        public async Task<IDbTransaction> GetTransactionAsync() => await (await GetConnAsync()).BeginTransactionAsync();
        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
            }

            Connection?.Dispose();
            Connection = null;
            
            _disposed = true;
        }
        
        ~PmbRepository()
        {
            Dispose(false);
        }
    }
}