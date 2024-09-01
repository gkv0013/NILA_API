using Hydrogen.Common;
using System.Net;
using System.Runtime.Serialization;

namespace H2O.Common
{
    public class ApiResponse
    {

        [DataMember]
        public int StatusCode { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string ErrorMessage { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int ValidationMessageType { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public object Result { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Message { get; set; }
        public ApiResponse() { }
        public ApiResponse(HttpStatusCode statusCode, object result,
                    int validationMessageType, object objRequestMessage, HttpResponseMessage response)
        {
            StatusCode = (int)statusCode;
            ValidationMessageType = validationMessageType;

            switch (statusCode)
            {
                case HttpStatusCode.OK: // 200
                    Result = result;
                    HttpRequestMessage requestMessage = objRequestMessage as HttpRequestMessage;
                    if (requestMessage != null)
                    {
                        if (requestMessage.Method == HttpMethod.Post)
                        {
                            Message = MessageLib.Save;
                        }
                        else if (requestMessage.Method == HttpMethod.Put)
                        {
                            Message = MessageLib.Update;
                        }
                        else if (requestMessage.Method == HttpMethod.Delete)
                        {
                            Message = MessageLib.Delete;
                        }
                    }
                    else
                    {
                        string strRequestmsg = objRequestMessage as string;
                        if (strRequestmsg != null)
                        {
                            if (strRequestmsg.Equals("POST", StringComparison.OrdinalIgnoreCase))
                            {
                                Message = MessageLib.Save;
                            }
                            else if (strRequestmsg.Equals("PUT", StringComparison.OrdinalIgnoreCase))
                            {
                                Message = MessageLib.Update;
                            }
                            else if (strRequestmsg.Equals("DELETE", StringComparison.OrdinalIgnoreCase))
                            {
                                Message = MessageLib.Delete;
                            }
                        }
                    }
                    break;
                case HttpStatusCode.Accepted: //202
                    Result = "";
                    Message = result.ToString();
                    break;
                case HttpStatusCode.Unauthorized: //401
                    ValidationMessageType = 2;
                    Result = "";
                    Message = "Authorization has been denied for this request. Reopen the app and continue";
                    break;
                default:
                    break;
            }
        }

    }
}
