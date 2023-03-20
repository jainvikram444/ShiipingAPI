using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Xml.Linq;
using Microsoft.VisualBasic;
using NuGet.Protocol.Plugins;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace ShiipingAPI.RespnseModels
{
    public class Response<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public IEnumerable<T> ResponseData { get; set; }

        public Response(bool isSuccess, string message, IEnumerable<T> data)
        {
            IsSuccess = isSuccess;
            Message = message;
            ResponseData = data;
        }
    }


    public class SwaggerErrorResponse
    {
        [DefaultValue(false)]
        public bool IsSuccess { get; set; } = false;
        [DefaultValue("Error message")]
        public string Message { get; set; } = "Error message";
        [DefaultValue(null)]
        public Array ResponseData { get; set; }
    }


    public class SwaggerBoolResponse
    {
        [DefaultValue(true)]
        public bool IsSuccess { get; set; } = true;
        [DefaultValue("Success message")]
        public string Message { get; set; } = "Success message";
        [DefaultValue(null)]
        public Array ResponseData { get; set; }
    }

}




