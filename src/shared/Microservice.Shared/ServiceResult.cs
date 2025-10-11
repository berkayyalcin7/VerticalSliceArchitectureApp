using Refit;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using ProblemDetails = Microsoft.AspNetCore.Mvc.ProblemDetails;

namespace Microservice.Shared
{
    public class ServiceResult
    {
        [JsonIgnore]
        public HttpStatusCode Status { get; set; }
        public ProblemDetails? Fail { get; set; }

        // => Sadece Get olan metotlar
        [JsonIgnore]
        public bool IsSuccess => Fail is null;
        [JsonIgnore]
        public bool IsFail => !IsSuccess;

        public static ServiceResult SuccessAsNoContent()
        {
            return new ServiceResult
            {
                Status = HttpStatusCode.NoContent
            };
        }

        public static ServiceResult ErrorAsNotFound()
        {
            return new ServiceResult
            {
                Status = HttpStatusCode.NotFound,
                Fail = new ProblemDetails
                {
                    Title = "Not Found",
                    Status = (int)HttpStatusCode.NotFound,
                    Detail = "The requested resource was not found."
                }
            };
        }

        public static ServiceResult ErrorFromProblemDetails(ApiException exception)
        {
            if (!string.IsNullOrEmpty(exception.Content))
            {
                return new ServiceResult
                {
                    Status = exception.StatusCode,
                    Fail = new Microsoft.AspNetCore.Mvc.ProblemDetails
                    {
                        Title = exception.Message,
                    }
                };
            }

            var problemDetails = JsonSerializer.Deserialize<Microsoft.AspNetCore.Mvc.ProblemDetails>(exception.Content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            });

            return new ServiceResult()
            {
                Fail = problemDetails,
                Status = exception.StatusCode
            };
        }

        public static ServiceResult Error(ProblemDetails problemDetails, HttpStatusCode httpStatusCode)
        {
            return new ServiceResult
            {
                Status = httpStatusCode,
                Fail = problemDetails
            };
        }

        public static ServiceResult ErrorFromValidation(string error)
        {
            var problemDetails = new ProblemDetails
            {
                Title = "Validation Error",
                Status = (int)HttpStatusCode.BadRequest,
                Detail = error,
                
            };
            return new ServiceResult
            {
                Status = HttpStatusCode.BadRequest,
                Fail = problemDetails
            };
        }
    }

    public class ServiceResult<T>:ServiceResult
    {
        public T? Data { get; set; } = default!;

        [JsonIgnore]
        public string? UrlAsCreated { get; set; } // Created response header'da dönecek URL

        // 200
        public static ServiceResult<T> SuccessAsOk(T data)
        {
            return new ServiceResult<T>
            {
                Status = HttpStatusCode.OK,
                Data = data
            };
        }


        // NEW anahtar kelimesiyle - Virtual ile farklıdır..

        // 201 => Created => responses body header 
        public static ServiceResult<T> SuccessAsCreated(T data,string url)
        {
            return new ServiceResult<T>
            {
                Status = HttpStatusCode.Created,
                Data = data,
                UrlAsCreated = url
            };
        }

        public new static ServiceResult<T> ErrorFromProblemDetails(ApiException exception)
        {
            if (!string.IsNullOrEmpty(exception.Content))
            {
                return new ServiceResult<T>
                {
                    Status = exception.StatusCode,
                    Fail = new ProblemDetails
                    {
                        Title = exception.Message,
                    }
                };
            }

            var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(exception.Content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            });

            return new ServiceResult<T>()
            {
                Fail = problemDetails,
                Status = exception.StatusCode
            };
        }

        public new static ServiceResult<T> Error(ProblemDetails problemDetails,HttpStatusCode httpStatusCode)
        {
            return new ServiceResult<T>
            {   
                Status = httpStatusCode,
                Fail = problemDetails
            };
        }

        public new static ServiceResult<T> ErrorFromValidation(string error)
        {
            var problemDetails = new ProblemDetails
            {
                Title = "Validation Error",
                Status = (int)HttpStatusCode.BadRequest,
                Detail = error,
            };
            return new ServiceResult<T>
            {
                Status = HttpStatusCode.BadRequest,
                Fail = problemDetails
            };
        }
    }
}
