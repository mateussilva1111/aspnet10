using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Hypermedia
{
    public class HypermediaFilter(HypermediaFilterOpttions hypermediaFiltersOptions) : ResultFilterAttribute
    {
        private readonly HypermediaFilterOpttions _hypermediaFilters = hypermediaFiltersOptions;

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            TryEnrichExecuting(context);
            base.OnResultExecuting(context);
        }

        private void TryEnrichExecuting(ResultExecutingContext context)
        {
            if (context.Result is OkObjectResult objectResult) 
            {
                var enricher = _hypermediaFilters.ContentResponseEnricherList.
                    FirstOrDefault(e => e.CanEnrich(context));
                enricher?.Enrich(context).Wait();
            }
        }
    }
}
