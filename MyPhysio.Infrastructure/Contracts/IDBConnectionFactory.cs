using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPhysio.Infrastructure.Contracts
{
    public interface IDBConnectionFactory
    {
        IDbConnection GetConnection(string connectionKey);
    }
}
