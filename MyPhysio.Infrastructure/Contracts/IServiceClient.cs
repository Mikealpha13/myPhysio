using MyPhysio.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPhysio.Infrastructure.Contracts
{
    public interface IServiceClient<T,U> where T :class
    {
         Task<U> Post(T requestParameters, string endpoint,HTTPClients clients);

         Task<U> Get(string endpoint, HTTPClients clients);

        Task<U> Patch(T requestParameters, string endpoint, HTTPClients clients);

        Task<U> Put(T requestParameters, string endpoint, HTTPClients clients);

        Task<IEnumerable<U>> PostCollection(T requestParameters, string endpoint, HTTPClients clients);

        Task<IEnumerable<U>> GetCollection(string endpoint, HTTPClients clients);
    }
}
