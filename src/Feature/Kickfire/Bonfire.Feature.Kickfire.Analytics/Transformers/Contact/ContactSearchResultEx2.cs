using Newtonsoft.Json;
using Sitecore.Cintel.Client.Transformers.Contact;
using Sitecore.Cintel.Search;

namespace Bonfire.Feature.Kickfire.Analytics.Transformers.Contact
{
    public class ContactSearchResultEx2 : ContactSearchResultEx
    {
        [JsonProperty("company")]
        public string Company { get; set; }

        public ContactSearchResultEx2(IContactSearchResult result)
            : base(result)
        {
        }
    }
}
