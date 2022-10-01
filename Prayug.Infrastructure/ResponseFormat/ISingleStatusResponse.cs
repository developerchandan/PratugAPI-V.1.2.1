using System;
using System.Collections.Generic;
using System.Text;

namespace Prayug.Infrastructure.ResponseFormat
{
    public interface ISingleStatusResponse<TModel> : IResponse, IDisposable
    {
    }
}
