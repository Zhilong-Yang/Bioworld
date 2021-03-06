namespace Bioworld.WebApi.Exceptions
{
    using System;

    public interface IExceptionToResponseMapper
    {
        ExceptionResponse Map(Exception exception);
    }
}