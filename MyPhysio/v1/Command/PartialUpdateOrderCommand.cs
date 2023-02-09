using MyPhysioAPI.v1.ViewModels.Request;
using MyPhysioAPI.v1.ViewModels.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPhysioAPI.v1.Command
{
    /// <summary>
    /// 
    /// </summary>
    public class PartialUpdateOrderCommand:IRequest<PartialUpdateResponseViewModel>
    {
        /// <summary>
        /// 
        /// </summary>
        public readonly PartialUpdateRequestViewModel _partialUpdateRequest;
        /// <summary>
        /// 
        /// </summary>
        public PartialUpdateOrderCommand(PartialUpdateRequestViewModel partialUpdateRequest)
        {
            _partialUpdateRequest = partialUpdateRequest ?? throw new ArgumentNullException(nameof(partialUpdateRequest));
        }
    }
}
