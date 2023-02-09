using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPhysio.v1.Contracts
{

    /// <summary>
    /// This class is used to process the service/enity response to view model
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="X"></typeparam>
    public interface IResponseProcessor<T,X> where T :class
    {
        /// <summary>
        /// Cast the external model to view model
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        T ToViewModel(X request);
    }
}
