using System;
using System.Collections.Generic;
using System.Text;

namespace Prayug.Infrastructure.ResponseFormat
{
    public interface ISingleListResponse<TModel> : IListResponse, IDisposable
    {
        TModel objResponse { get; set; }
    }
}
