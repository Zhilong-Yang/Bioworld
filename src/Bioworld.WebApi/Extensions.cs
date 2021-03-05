namespace Bioworld.WebApi
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Net;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;
    using Types;
    using WebApi.Exceptions;
    using WebApi.Formatters;
    using WebApi.Requests;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.HttpOverrides;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.AspNetCore.Server.Kestrel.Core;
    using Microsoft.Extensions.DependencyInjection;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using Newtonsoft.Json.Serialization;
    using Open.Serialization.Json;


    public static class Extensions
    {
        private static readonly byte[] InvalidJsonRequestBytes = Encoding.UTF8.GetBytes("An invalid JSON was sent.");

        private const string SectionName = "webApi";

        private const string RegisterName = "webApi";

        private const string EmptyJsonObject = "{}";

        private const string LocationHeader = "Location";

        private const string JsonContentType = "application/json";

        private static bool _bindRequestFromRoute;


        public static Task Ok(this HttpResponse response, object data = null)
        {
            response.StatusCode = 200;
            return data is null ? Task.CompletedTask : response.WriteAsJsonAsync(data);
        }

        public static Task Create(this HttpResponse response, string location = null, object data = null)
        {
            response.StatusCode = 201;
            if (string.IsNullOrWhiteSpace(location))
            {
                return Task.CompletedTask;
            }

            if (!response.Headers.ContainsKey(LocationHeader))
            {
                response.Headers.Add(LocationHeader, location);
            }

            return data is null ? Task.CompletedTask : response.WriteAsJsonAsync(data);
        }

        public static Task Accept(this HttpResponse response)
        {
            response.StatusCode = (int) HttpStatusCode.Accepted;
            return Task.CompletedTask;
        }

        public static Task NoContent(this HttpResponse response)
        {
            response.StatusCode = (int) HttpStatusCode.NoContent;
            return Task.CompletedTask;
        }

        public static Task MovedPermanently(this HttpResponse response, string url)
        {
            response.StatusCode = (int) HttpStatusCode.MovedPermanently;
            if (!response.Headers.ContainsKey(LocationHeader))
            {
                response.Headers.Add(LocationHeader, url);
            }

            return Task.CompletedTask;
        }

        public static Task Redirect(this HttpResponse response, string url)
        {
            response.StatusCode = (int) HttpStatusCode.PermanentRedirect;
            if (!response.Headers.ContainsKey(LocationHeader))
            {
                response.Headers.Add(LocationHeader, url);
            }

            return Task.CompletedTask;
        }

        public static Task BadRequest(this HttpResponse response)
        {
            response.StatusCode = (int) HttpStatusCode.BadRequest;
            return Task.CompletedTask;
        }

        public static Task Unauthorized(this HttpResponse response)
        {
            response.StatusCode = (int) HttpStatusCode.Unauthorized;
            return Task.CompletedTask;
        }

        public static Task Forbidden(this HttpResponse response)
        {
            response.StatusCode = (int) HttpStatusCode.Forbidden;
            return Task.CompletedTask;
        }

        public static Task NotFound(this HttpResponse response)
        {
            response.StatusCode = (int) HttpStatusCode.NotFound;
            return Task.CompletedTask;
        }

        public static Task InternalServerError(this HttpResponse response)
        {
            response.StatusCode = (int) HttpStatusCode.InternalServerError;
            return Task.CompletedTask;
        }

        public static async Task WriteJsonAsync<T>(this HttpResponse response, T value)
        {
            response.ContentType = JsonContentType;
            var serializer = response.HttpContext.RequestServices.GetRequiredService<IJsonSerializer>();
            await serializer.SerializeAsync(response.Body, value);
        }
        
        public static async Task<T> ReadJsonAsync<T>(this HttpContext httpContext)
        {
            if (httpContext.Request.Body is null)
            {
                httpContext.Response.StatusCode = 400;
                await httpContext.Response.Body.WriteAsync(InvalidJsonRequestBytes, 0, InvalidJsonRequestBytes.Length);

                return default;
            }

            try
            {
                var request = httpContext.Request;
                var payload = await httpContext.RequestServices.GetRequiredService<IJsonSerializer>().DeserializeAsync<T>(request.Body);
                if (_bindRequestFromRoute && HasRouteData(request))
                {
                    var values = request.HttpContext.GetRouteData().Values;
                    foreach (var (key, value) in values)
                    {
                        var field = payload.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
                            .SingleOrDefault(f => f.Name.ToLowerInvariant().StartsWith($"<{key}>",
                                StringComparison.InvariantCultureIgnoreCase));

                        if (field is null)
                        {
                            continue;
                        }

                        var fieldValue = TypeDescriptor.GetConverter(field.FieldType)
                            .ConvertFromInvariantString(value.ToString());
                        field.SetValue(payload, fieldValue);
                    }
                }

                var results = new List<ValidationResult>();
                if (Validator.TryValidateObject(payload, new ValidationContext(payload), results))
                {
                    return payload;
                }

                httpContext.Response.StatusCode = 400;
                await httpContext.Response.WriteJsonAsync(results);

                return default;
            }
            catch
            {
                httpContext.Response.StatusCode = 400;
                await httpContext.Response.Body.WriteAsync(InvalidJsonRequestBytes, 0, InvalidJsonRequestBytes.Length);

                return default;
            }
        }

        public static T ReadQuery<T>(this HttpContext context) where T : class
        {
            var request = context.Request;
            RouteValueDictionary values = null;

            if (HasRouteData(request))
            {
                values = request.HttpContext.GetRouteData().Values;
            }

            if (HasQueryString(request))
            {
                var queryString = HttpUtility.ParseQueryString(request.HttpContext.Request.QueryString.Value);
                values ??= new RouteValueDictionary();
                foreach (var key in queryString.AllKeys)
                {
                    values.TryAdd(key, queryString[key]);
                }
            }

            var serializer = context.RequestServices.GetRequiredService<IJsonSerializer>();
            if (values is null)
            {
                return serializer.Deserialize<T>(EmptyJsonObject);
            }
            var serialized = serializer.Serialize(values.ToDictionary(k => k.Key, k => k.Value))
                ?.Replace("\\\"", "\"")
                .Replace("\"{", "{")
                .Replace("}\"", "}")
                .Replace("\"[", "[")
                .Replace("]\"", "]");

            return serializer.Deserialize<T>(serialized);
        }

        private static bool HasQueryString(this HttpRequest request)
            => request.Query.Any();

        private static bool HasRouteData(this HttpRequest request)
            => request.HttpContext.GetRouteData().Values.Any();

        public static string Args(this HttpContext context, string key)
            => context.Args<string>(key);

        public static T Args<T>(this HttpContext context, string key)
        {
            if (!context.GetRouteData().Values.TryGetValue(key, out var value))
            {
                return default;
            }

            if (typeof(T) == typeof(string) && value is string)
            {
                return (T) value;
            }

            var data = value?.ToString();
            if (string.IsNullOrWhiteSpace(data))
            {
                return default;
            }

            return (T) TypeDescriptor.GetConverter(typeof(T)).ConvertFromInvariantString(data);
        }

        private class EmptyExceptionToResponseMapper : IExceptionToResponseMapper
        {
            public ExceptionResponse Map(Exception exception) => null;
        }
    }
}