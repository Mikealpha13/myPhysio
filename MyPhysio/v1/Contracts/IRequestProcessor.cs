using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPhysio.v1.Contracts
{

    /// <summary>
    /// This class is used to process the model and loosely couple the model dependency
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="X"></typeparam>
    public interface IRequestProcessor<T,X> where T:class
    {

        /// <summary>
        /// Use to map the request view model to entity model
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        T ToExternalModel(X request);

       
    }
}
