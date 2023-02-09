using MyPhysio.Domain.ServiceModels.Request;
using MyPhysio.v1.Contracts;
using MyPhysioAPI.v1.ViewModels.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPhysioAPI.v1.Processor.Request
{
    /// <summary>
    /// 
    /// </summary>
    public class PartialUpdateRequestProcessor : IRequestProcessor<PartialUpdateRequestServiceModel, PartialUpdateRequestViewModel>
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public PartialUpdateRequestServiceModel ToExternalModel(PartialUpdateRequestViewModel request)
        {
            try
            {
                return new PartialUpdateRequestServiceModel()
                {
                    date = request.date,
                    status = request.status,
                    instructions = request.Instruction
                };
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
