using System.Collections.Generic;

namespace Bonfire.Foundation.Kickfire.Library.Model
{
    public class KickFireModel
    {
        public class Datum
        {
            public string CID { get; set; }
            public string name { get; set; }
            public string website { get; set; }
            public string street { get; set; }
            public string city { get; set; }
            public string regionShort { get; set; }
            public string region { get; set; }
            public string postal { get; set; }
            public string countryShort { get; set; }
            public string country { get; set; }
            public string phone { get; set; }
            public string employees { get; set; }
            public string revenue { get; set; }
            public string category { get; set; }
            public string sicCode { get; set; }
            public string latitude { get; set; }
            public string longitude { get; set; }
            public string stockSymbol { get; set; }
            public string facebook { get; set; }
            public string twitter { get; set; }
            public string linkedIn { get; set; }
            public string linkedInID { get; set; }
            public int isISP { get; set; }
            public int confidence { get; set; }
        }

        public class RootObject
        {
            public string status { get; set; }
            public int results { get; set; }
            public List<Datum> data { get; set; }
            public bool IsError { get; set; }
            public string Message { get; set; }
        }
    }
}
