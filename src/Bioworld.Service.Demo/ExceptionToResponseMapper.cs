namespace Bioworld.Service.Demo
{
    using System;
    using System.Net;
    using WebApi.Exceptions;

    public class ExceptionToResponseMapper : IExceptionToResponseMapper
    {
        public ExceptionResponse Map(Exception exception)
            => new ExceptionResponse(new { code = "error", message = exception.Message }, HttpStatusCode.BadRequest);
    }
}