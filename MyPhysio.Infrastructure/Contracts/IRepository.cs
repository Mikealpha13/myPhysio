using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPhysio.Infrastructure.Contracts
{
    public interface IRepository<T> where T:class
    {
       Task <IEnumerable<T>> ExecuteProcedure(string connectionKey,DynamicParameters parameters,string procedureName);
        Task<IEnumerable<T>> ExecuteQuery(string connectionKey, string query);
    }
}
