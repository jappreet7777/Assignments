using Newtonsoft.Json;
using ProductDetails.Domain.ResponseInfoDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProductDetails.Domain.ResponseInfoFormat
{
    public struct ResponseInfo<T>
    {
        private ResponseInfo _responseInfo;

        [JsonProperty("statusCode")]
        [JsonPropertyName("statusCode")]
        public string StatusCode
        {
            get
            {
                return _responseInfo.StatusCode;
            }
            set
            {
                _responseInfo.StatusCode = value;
            }
        }

        [JsonProperty("message")]
        [JsonPropertyName("message")]
        public string Message
        {
            get
            {
                return _responseInfo.Message;
            }
            set
            {
                _responseInfo.Message = value;
            }
        }

        [JsonProperty("data")]
        [JsonPropertyName("data")]
        public T Data { get; private set; }

        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public bool IsSuccessful => _responseInfo.IsSuccessful;

        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public ResponseInfo InfoResult => _responseInfo;

        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public object ErrorData
        {
            get
            {
                return _responseInfo.ErrorData;
            }
            set
            {
                _responseInfo.ErrorData = value;
            }
        }

        public ResponseInfo(string message = "An unexpected error has occurred.", string statusCode = "10001", T result = default(T))
        {
            Data = result;
            _responseInfo = new ResponseInfo(message, statusCode);
        }

        public ResponseInfo<T> Success(T result, string message = null)
        {
            if (result == null)
            {
                throw new ArgumentNullException("result", "result cannot be null when calling ResponseInfo<>.Success(" + typeof(T).FullName + ", string)");
            }

            _responseInfo.Success(message);
            Data = result;
            return this;
        }

        public ResponseInfo<T> Fail(string message = "An unexpected error has occurred.", string responseCode = "10001", object errorData = null, T result = default(T))
        {
            Data = result;
            _responseInfo.Fail(message, responseCode, errorData);
            return this;
        }

        public ResponseInfo<T> Fail(ResponseInfo response, T result = default(T))
        {
            Data = result;
            _responseInfo = response;
            return this;
        }

        public TK GetError<TK>()
        {
            return _responseInfo.GetError<TK>();
        }

        public ResponseInfo<T> SetStatusCode(string statusCode)
        {
            _responseInfo.StatusCode = statusCode;
            return this;
        }
    }
}
