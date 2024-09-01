using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Npgsql;

namespace H2O.Common
{
    public class CustomResponseMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomResponseMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var originalBodyStream = context.Response.Body;
            try
            {
                // Intercept and buffer the response
                
                using (var responseBody = new MemoryStream())
                {
                    context.Response.Body = responseBody;

                    await _next(context); // Continue processing (e.g., MVC action)

                    context.Response.Body.Seek(0, SeekOrigin.Begin);
                    var text = await new StreamReader(context.Response.Body).ReadToEndAsync();
                    context.Response.Body.Seek(0, SeekOrigin.Begin);

                    // Construct the custom response object
                    if (context.Response.ContentType != null && context.Response.ContentType.Contains("application/json"))
                    {
                        var customResponse = new ApiResponse
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = GetMessage(context.Response.StatusCode),
                            Result = JsonConvert.DeserializeObject(text) // Deserialize only if content is JSON
                        };

                        var json = JsonConvert.SerializeObject(customResponse);

                        // Reset the body to write the modified response
                        context.Response.Body = originalBodyStream;
                        context.Response.ContentType = "application/json"; // Ensure ContentType is set to JSON

                        // Ensure Content-Length header is not set or reflects the new content size
                        context.Response.ContentLength = null; // Clear Content-Length first
                        await context.Response.WriteAsync(json);
                    }
                    else
                    {
                        // If not JSON, handle it as plain text or another content type
                        var customResponse = new ApiResponse
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = GetMessage(context.Response.StatusCode),
                            Result = text // Keep the original content
                        };

                        var json = JsonConvert.SerializeObject(customResponse);

                        context.Response.Body = originalBodyStream;
                        context.Response.ContentType = "application/json";
                        context.Response.ContentLength = null;
                        await context.Response.WriteAsync(json);
                    }

                }
            }
            catch (NpgsqlException ex)
            {
                // Handle NpgsqlException
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var apiResponse = new ApiResponse
                {
                    StatusCode = context.Response.StatusCode,
                    ErrorMessage = $"PostgreSQL error: {ex.Message}"
                };

                var jsonResponse = JsonConvert.SerializeObject(apiResponse);
                context.Response.Body = originalBodyStream;
                context.Response.ContentType = "application/json";
                context.Response.ContentLength = null;
                await context.Response.WriteAsync(jsonResponse);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var apiResponse = new ApiResponse
                {
                    StatusCode = context.Response.StatusCode,
                    ErrorMessage = $"An error occurred: {ex.Message}"
                };

                var jsonResponse = JsonConvert.SerializeObject(apiResponse);
                context.Response.Body = originalBodyStream;
                context.Response.ContentType = "application/json";
                context.Response.ContentLength = null;
                await context.Response.WriteAsync(jsonResponse);
            }
        }

        private string GetMessage(int statusCode)
        {
            // Simplified example, expand according to your needs
            return statusCode switch
            {
                200 => "Success",
                400 => "Bad Request",
                404 => "Not Found",
                _ => "Error"
            };
        }
    }
}
