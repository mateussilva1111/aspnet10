using Microsoft.Net.Http.Headers;

namespace API.Configuration
{
    public static class ContentNegotiationConfig
    {
        public static IMvcBuilder AddContentNegotiationConfig(this IMvcBuilder mvcBuilder)
        {
            return mvcBuilder.AddMvcOptions(static options =>
            {
                options.RespectBrowserAcceptHeader = true;
                options.ReturnHttpNotAcceptable = false;
                options.FormatterMappings.SetMediaTypeMappingForFormat("json", MediaTypeHeaderValue.Parse("application/json"));
                options.FormatterMappings.SetMediaTypeMappingForFormat("xml", MediaTypeHeaderValue.Parse("application/xml"));
            }).AddXmlSerializerFormatters();
        }
    }
}
