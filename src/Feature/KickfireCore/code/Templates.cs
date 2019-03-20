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

        public struct Employees
        {
            public static ID Id = new ID("{46E6AB99-6CEF-4A90-ADF6-173B79958D59}");

            public struct Fields
            {
                public static ID Range = new ID("{F4B02179-62C8-4D4F-8EC6-4B77D3411D30}");
               
            }
        }

        public struct Revenue
        {
            public static ID Id = new ID("{8E453E67-C2F6-4E80-83CD-CB63839F44E3}");

            public struct Fields
            {
                public static ID Range = new ID("{1A01C3B3-C865-4D5D-B70A-F0EC03431F56}");
               
            }
        }

        public struct Region
        {
            public static ID Id = new ID("{A6DA2DB8-7ABC-4C30-AFEF-920A7831F706}");

            public struct Fields
            {
                public static ID Code = new ID("{47308B97-FD4D-47C7-8091-EF7B1A944251}");
                public static ID Description = new ID("{1A01C3B3-C865-4D5D-B70A-F0EC03431F56}");

            }
        }

        public struct FieldProperties
        {
            public static ID Id = new ID("{DED9F9C4-1D06-4C91-B9DB-EC0CA82634AD}");

            public struct Fields
            {
                public static ID FieldName = new ID("{E083FC9B-848C-4CC6-B8AF-A72B213534F2}");

            }
        }
    }
}