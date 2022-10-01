using Prayug.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Prayug.Infrastructure.ResponseFormat
{
    public interface IListResponse
    {
        long timestamp { get; set; }
        [JsonIgnore]
        Int32 pageSize { get; set; }
        Int64 TotalRecord { get; set; }
        Int32 NumberOfPages { get; }
        String Message { get; set; }
        ResponseMessageEnum Status { get; set; }
    }
}
