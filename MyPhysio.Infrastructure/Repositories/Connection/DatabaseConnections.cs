using MyPhysio.Domain.Configuration;
using MyPhysio.Infrastructure.Contracts;
using Microsoft.Extensions.Options;
using System;
using System.Data;
using Microsoft.AspNetCore.Hosting;
using System.Linq;
using System.Data.SqlClient;

namespace MyPhysio.Infrastructure.Repositories.Connection
{
    public class DatabaseConnections : IDBConnectionFactory
    {

        private readonly IOptions<ConnectionDataSource> _connectionStrings;
        private readonly IHostingEnvironment _hostingEnvironment;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionStrings"></param>
        /// <param name="hostingEnvironment"></param>
        public DatabaseConnections(IOptions<ConnectionDataSource> connectionStrings,IHostingEnvironment hostingEnvironment)
        {
            _connectionStrings = connectionStrings ?? throw new ArgumentNullException(nameof(connectionStrings));
            _hostingEnvironment = hostingEnvironment ?? throw new ArgumentNullException(nameof(hostingEnvironment));
        }

        /// <summary>
        /// Gets the connection for database
        /// </summary>
        /// <param name="connectionKey"></param>
        /// <returns></returns>
        public IDbConnection GetConnection(string connectionKey)
        {
            if (_hostingEnvironment.IsDevelopment())
                return GetConnectionFromConfig(connectionKey);
            else
                return GetConnectionFromKeyVault(connectionKey);
        }


        /// <summary>
        /// Gets the connection in Local/Development Enviornment
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private SqlConnection GetConnectionFromConfig(string key)
        {
            var connectionString = _connectionStrings.Value.ConnectionStrings
                                 .FirstOrDefault(z => z.Name == key).connectionString;

            var connection = new SqlConnection(connectionString);
            switch (connection.State)
            {
                case ConnectionState.Closed:
                case ConnectionState.Broken:
                case ConnectionState.Fetching:
                    connection.Open();
                    break;
                default:
                    return connection;
            }
            return connection;
        }

        /// <summary>
        /// Gets the connection from Key Vault
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private SqlConnection GetConnectionFromKeyVault(string key)
        {
            //TODO
            return null;
        }
    }
}
