using System;
using System.Collections.Generic;
using System.Text;

namespace Prayug.Infrastructure.ResponseFormat
{
    public interface ISingleModelResponse<TModel> : IResponse, IDisposable
    {
        TModel objResponse { get; set; }
    }
}
