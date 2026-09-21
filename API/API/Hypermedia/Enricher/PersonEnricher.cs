using API.Data.Dto;
using API.Hypermedia.Cosntants;
using Microsoft.AspNetCore.Mvc;

namespace API.Hypermedia.Enricher
{
    public class PersonEnricher : ContentResponseEnricher<PersonDTO>
    {
        protected override Task EnrichModel(PersonDTO content, IUrlHelper urlHelper)
        {
            var request = urlHelper.ActionContext.HttpContext.Request;
            var baseUrl = $"{request.Scheme}://{request.Host.ToUriComponent()}{request.PathBase.ToUriComponent()}/api/person";

            // Adiciona todos os links gerados pelo método auxiliar, incluindo o 'self'
            content.Links.AddRange(GenerateLinks(content, baseUrl));

            return Task.CompletedTask;
        }

        private IEnumerable<HypermediaLink> GenerateLinks(PersonDTO content, string baseUrl)
        {
            return new List<HypermediaLink>()
            {
                new HypermediaLink
                {
                    Rel = "self",
                    Href = $"{baseUrl}/{content.Id}",
                    Type = ResponseTypeFormat.DefaultGet,
                    Action = HttpActionVerb.GET
                },
                new HypermediaLink
                {
                    Rel = RelationType.COLLECTION,
                    Href = $"{baseUrl}",
                    Type = ResponseTypeFormat.DefaultGet,
                    Action = HttpActionVerb.GET,
                },
                new HypermediaLink
                {
                    Rel = RelationType.CREATE,
                    Href = $"{baseUrl}",
                    Type = ResponseTypeFormat.DefaultPost,
                    Action = HttpActionVerb.POST,
                },
                new HypermediaLink
                {
                    Rel = RelationType.UPDATE,
                    Href = $"{baseUrl}",
                    Type = ResponseTypeFormat.DefaultPut,
                    Action = HttpActionVerb.PUT,
                },
                new HypermediaLink
                {
                    Rel = RelationType.DELETE,
                    Href = $"{baseUrl}/{content.Id}",
                    Type = ResponseTypeFormat.DefaultDelete,
                    Action = HttpActionVerb.DELETE,
                },
                new HypermediaLink
                {
                    Rel = RelationType.GET,
                    Href = $"{baseUrl}/{content.Id}",
                    Type = ResponseTypeFormat.DefaultGet,
                    Action = HttpActionVerb.GET,
                },
                new HypermediaLink
                {
                    Rel = RelationType.PATCH,
                    Href = $"{baseUrl}/{content.Id}",
                    Type = ResponseTypeFormat.DefaultPatch,
                    Action = HttpActionVerb.PATCH,
                }
            };
        }
    }
}