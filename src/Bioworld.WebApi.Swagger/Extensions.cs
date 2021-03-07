namespace Bioworld.WebApi.Swagger
{
    using System;
    using Docs.Swagger;
    using WebApi.Swagger;
    using Microsoft.Extensions.DependencyInjection;

    public static class Extensions
    {
        private const string SectionName = "swagger";

        public static IBioWorldBuilder AddWebApiSwaggerDocs(this IBioWorldBuilder builder,
            string sectionName = SectionName)
        {
            if (string.IsNullOrWhiteSpace(sectionName))
            {
                sectionName = SectionName;
            }

            return builder.AddWebApiSwaggerDocs(b => b.AddSwaggerDocs(sectionName));
        }

        public static IBioWorldBuilder AddWebApiSwaggerDocs(this IBioWorldBuilder builder,
            Func<ISwaggerOptionsBuilder, ISwaggerOptionsBuilder> buildOptions)
            => builder.AddWebApiSwaggerDocs(b => b.AddSwaggerDocs(buildOptions));

        public static IBioWorldBuilder AddWebApiSwaggerDocs(this IBioWorldBuilder builder,
            SwaggerOptions options)
            => builder.AddWebApiSwaggerDocs(b => b.AddSwaggerDocs(options));

        private static IBioWorldBuilder AddWebApiSwaggerDocs(this IBioWorldBuilder builder,
            Action<IBioWorldBuilder> registerSwagger)
        {
            registerSwagger(builder);
            builder.Services.AddSwaggerGen(c => c.DocumentFilter<WebApiDocumentFilter>());
            return builder;
        }
    }
}