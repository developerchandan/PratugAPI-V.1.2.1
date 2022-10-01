using Prayug.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prayug.Infrastructure.ResponseFormat
{
    public interface IResponse
    {
        long timestamp { get; set; }
        String Message { get; set; }
        String ResultValue { get; set; }
        ResponseMessageEnum Status { get; set; }
    }
}
