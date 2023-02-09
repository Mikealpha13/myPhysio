using MyPhysio.Infrastructure.Contracts;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPhysio.Infrastructure.Repositories
{

    /// <summary>
    /// Generic Repository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Repository<T> : IRepository<T> where T : class
    {

        private readonly IDBConnectionFactory _dbConnectionFactory;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbConnectionFactory"></param>
        public Repository(IDBConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory ?? throw new ArgumentNullException(nameof(dbConnectionFactory));
        }

        /// <summary>
        /// This method executes the procedure.
        /// </summary>
        /// <param name="connectionKey"></param>
        /// <param name="parameters"></param>
        /// <param name="procedureName"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> ExecuteProcedure(string connectionKey, DynamicParameters parameters, string procedureName)
        {
            try
            {
                using(var activeConnection = _dbConnectionFactory.GetConnection(connectionKey))
                {
                    var result =  await SqlMapper.QueryAsync<T>(activeConnection,
                                           procedureName, param: parameters,
                                           commandType: System.Data.CommandType.StoredProcedure
                                             );
                    return result;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        /// <summary>
        /// This method executes the inline query
        /// </summary>
        /// <param name="connectionKey"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> ExecuteQuery(string connectionKey, string query)
        {
            try
            {
                using (var activeConnection = _dbConnectionFactory.GetConnection(connectionKey))
                {
                    var result = await SqlMapper.QueryAsync<T>(activeConnection,
                                                               sql:query,
                                                               commandType: System.Data.CommandType.Text
                                                               );
                    return result;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
