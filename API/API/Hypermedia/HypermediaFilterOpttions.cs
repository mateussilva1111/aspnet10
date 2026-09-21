
using API.Hypermedia.Abstract;

namespace API.Hypermedia
{
    public class HypermediaFilterOpttions
    {

        public List<IResponseEnricher> ContentResponseEnricherList { get; set; } = new List<IResponseEnricher>();
    }
}
