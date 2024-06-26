using Newtonsoft.Json;
using ProductDetails.Domain.ResponseInfoFormat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProductDetails.Domain.ResponseInfoDetails
{
    
        public struct ResponseInfo
        {
            internal const string DefaultMessage = "An unexpected error has occurred.";

            private string _statusCode;

            private string _message;

            private bool _initialized;

            [Newtonsoft.Json.JsonIgnore]
            [System.Text.Json.Serialization.JsonIgnore]
            public object ErrorData { get; set; }

            [Newtonsoft.Json.JsonIgnore]
            [System.Text.Json.Serialization.JsonIgnore]
            public bool IsSuccessful => StatusCode == "90000";

            [JsonProperty("statusCode")]
            [JsonPropertyName("statusCode")]
            public string StatusCode
            {
                get
                {
                    if (!_initialized)
                    {
                        return "10001";
                    }

                    return _statusCode;
                }
                set
                {
                    _statusCode = value;
                    _initialized = true;
                }
            }

            [JsonProperty("message")]
            [JsonPropertyName("message")]
            public string Message
            {
                get
                {
                    if (!_initialized)
                    {
                        return "An unexpected error has occurred.";
                    }

                    return _message;
                }
                set
                {
                    _message = value;
                    _initialized = true;
                }
            }

            [JsonProperty("data")]
            [JsonPropertyName("data")]
            public object Data { get; private set; }

            public T GetError<T>()
            {
                object errorData = ErrorData;
                if (errorData is T)
                {
                    return (T)errorData;
                }

                return default(T);
            }

            public static ResponseInfo SuccessResult(string message = null)
            {
                ResponseInfo responseInfo = default(ResponseInfo);
                return responseInfo.Success(message);
            }

            public static ResponseInfo<T> SuccessResult<T>(T result, string message = null)
            {
                ResponseInfo<T> responseInfo = default(ResponseInfo<T>);
                return responseInfo.Success(result, message);
            }

            public static ResponseInfo FailedResult(string message = "An unexpected error has occurred.", string responseCode = "10001", object errorData = null)
            {
                ResponseInfo responseInfo = default(ResponseInfo);
                return responseInfo.Fail(message, responseCode, errorData);
            }

            public static ResponseInfo<T> FailedResult<T>(string message = "An unexpected error has occurred.", string responseCode = "10001", object errorData = null, T result = default(T))
            {
                ResponseInfo<T> responseInfo = default(ResponseInfo<T>);
                return responseInfo.Fail(message, responseCode, errorData, result);
            }

            public ResponseInfo Success(string message = "")
            {
                StatusCode = "90000";
                Message = message;
                _initialized = true;
                return this;
            }

            public ResponseInfo Fail(string message = "An unexpected error has occurred.", string responseCode = "10001", object errorData = null)
            {
                Message = message;
                StatusCode = responseCode;
                ErrorData = errorData;
                _initialized = true;
                return this;
            }

            public ResponseInfo(string message = "An unexpected error has occurred.", string statusCode = "10001")
            {
                _statusCode = "10001";
                _message = "An unexpected error has occurred.";
                _message = message;
                _statusCode = statusCode;
                ErrorData = null;
                Data = null;
                _initialized = true;
            }
        }
    
}
