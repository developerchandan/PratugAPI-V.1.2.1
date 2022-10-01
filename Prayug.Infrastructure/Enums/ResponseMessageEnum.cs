using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Prayug.Infrastructure.Enums
{
    public enum ResponseMessageEnum
    {
        [Description("Request successful.")]
        Success = 200,
        [Description("Duplicate.")]
        Duplicate = 201,
        [Description("Request responded with exceptions.")]
        Exception = 500,
        [Description("Request denied.")]
        UnAuthorized = 401,
        //[Description("Request responded with validation error(s).")]
        //ValidationError = 400,
        [Description("Unable to process the request.")]
        Failure = 400
        //Failure = 501
    }
}
