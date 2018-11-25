using Sitecore.Data;

namespace Bonfire.Feature.KickfireCore
{
    public class Templates
    {
        public struct SicCodeOverride
        {
            public struct Fields
            {
                public static ID Code = new ID("{415E8711-4FE5-45BC-A218-057634681385}");
                public static ID Description = new ID("{506A9E72-3132-4070-88EC-B02F00F9915E}");
                public static ID Grouping = new ID("{C8A6C8A5-8124-4728-A4C4-811AA2B3CBFE}");
            }
        }

        public struct SiceCode
        {
            public struct Fields
            {
                public static ID Group = new ID("{F9E6C052-9189-43DA-B814-E9019C793E99}");
                public static ID Profile = new ID("{851F239D-F387-4724-B756-62179C7F0356}");
            }
        }

        public struct Configuration
        {
            public struct Fields
            {
                public static ID KireFireKey = new ID("{5A3E6C40-392E-437A-A72A-7C2D9D9CADE4}");
                public static ID SkipIsp = new ID("{66A4FE61-1260-452F-9465-54199854F9F3}");
                public static ID SkipNonUsa = new ID("{99CA527F-E76D-4ACF-A121-152D360EBB07}");
            }
        }
    }
}