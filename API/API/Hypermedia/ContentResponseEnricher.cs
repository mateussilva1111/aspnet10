using API.Hypermedia.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Net.Mime;

namespace API.Hypermedia
{
    public abstract class ContentResponseEnricher<T> : IResponseEnricher where T : ISuportHypermedia
    {
        public virtual bool CanEnrich(Type contextType)
        {
            return contextType == typeof(T) || contextType == typeof(List<T>);
        }

        protected abstract Task EnrichModel(T content, IUrlHelper urlHelper);

        bool IResponseEnricher.CanEnrich(ResultExecutingContext response)
        {
            if (response.Result is OkObjectResult okObjectResult)
            {
                return CanEnrich(okObjectResult.Value.GetType());
            }

            return false;
        }

        public async Task Enrich(ResultExecutingContext context)
        {

            var urlHelper = new UrlHelperFactory().GetUrlHelper(context);

            if(context.Result is OkObjectResult okObjectResult)
            {
                if(okObjectResult.Value is T model)
                {
                    await EnrichModel(model, urlHelper);
                }
                if(context.Result is List<T> modelList)
                {
                    foreach (var item in modelList)
                    {
                        await EnrichModel(item, urlHelper);
                    }
                }
            }

            await Task.CompletedTask;
        }

    }
}
