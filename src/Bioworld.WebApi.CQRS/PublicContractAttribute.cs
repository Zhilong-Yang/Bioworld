namespace Bioworld.WebApi.CQRS
{
    using System;

    [AttributeUsage(AttributeTargets.Class)]
    public class PublicContractAttribute : Attribute
    {
    }
}