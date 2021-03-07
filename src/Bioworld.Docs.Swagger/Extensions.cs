namespace Bioworld.Docs.Swagger
{
    using System;
    using Docs.Swagger.Builders;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.OpenApi.Models;

    public static class Extensions
    {
        private const string SectionName = "swagger";
        private const string RegisterName = "doc.swagger";

        public static IBioWorldBuilder AddSwaggerDocs(this IBioWorldBuilder builder, string sectionName = SectionName)
        {
            if (string.IsNullOrWhiteSpace(sectionName))
            {
                sectionName = SectionName;
            }

            var options = builder.GetOptions<SwaggerOptions>(sectionName);
            return builder.AddSwaggerDocs(options);
        }

        public static IBioWorldBuilder AddSwaggerDocs(this IBioWorldBuilder builder,
            Func<ISwaggerOptionsBuilder, ISwaggerOptionsBuilder> buildOptions)
        {
            var options = buildOptions(new SwaggerOptionsBuilder()).Build();
            return builder.AddSwaggerDocs(options);
        }

        public static IBioWorldBuilder AddSwaggerDocs(this IBioWorldBuilder builder, SwaggerOptions options)
        {
            if (!options.Enabled || !builder.TryRegister(RegisterName))
            {
                return builder;
            }

            builder.Services.AddSingleton(options);
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(options.Name, new OpenApiInfo() {Title = options.Title, Version = options.Version});
                if (options.IncludeSecurity)
                {
                    c.AddSecurityDefinition("Bear", new OpenApiSecurityScheme()
                    {
                        Description =
                            "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey
                    });
                }
            });

            return builder;
        }

        public static IApplicationBuilder UseSwaggerDocs(this IApplicationBuilder builder)
        {
            var options = builder.ApplicationServices.GetService<SwaggerOptions>();
            if ((bool) !options?.Enabled)
            {
                return builder;
            }

            var routePrefix = string.IsNullOrWhiteSpace(options.RoutePrefix)
                ? string.Empty
                : options.RoutePrefix;

            builder.UseStaticFiles().UseSwagger(c =>
            {
                c.RouteTemplate = string.Concat(routePrefix, "/{documentName}/{swagger.json}");
                c.SerializeAsV2 = options.SerializeAsOpenApiV2;
            });

            return options.ReDocEnabled
                ? builder.UseReDoc(c =>
                {
                    c.RoutePrefix = routePrefix;
                    c.SpecUrl = $"{options.Name}/swagger.json";
                })
                : builder.UseSwaggerUI(c =>
                {
                    c.RoutePrefix = routePrefix;
                    c.SwaggerEndpoint($"/{routePrefix}/{options.Name}/swagger.json".FormatEmptyRoutePrefix(),
                        options.Title);
                });
        }

        private static string FormatEmptyRoutePrefix(this string route)
            => route.Replace("//", "/");
    }
}