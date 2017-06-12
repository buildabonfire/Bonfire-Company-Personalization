using Sitecore.Data;

namespace Bonfire.Feature.Kickfire.Analytics.Constants
{
    public static class IDs
    {
        public static ID SicParentId = new ID("{6F25CFF3-970F-477B-A9EB-B56465B34892}");
        public static ID GroupParentId = new ID("{8F6AC979-C5EB-4300-820B-E7E264526588}");
        
        public static class Events
        {
            public static ID CompanyEvent = new ID("{F434F709-C599-4F6C-8B78-33C228C17EDE}");
        }

        public static class Fields 
        {
            public static class SicParent
            {
                public static ID Code = new ID("{415E8711-4FE5-45BC-A218-057634681385}");
                public static ID Description = new ID("{506A9E72-3132-4070-88EC-B02F00F9915E}");
                public static ID Grouping = new ID("{C8A6C8A5-8124-4728-A4C4-811AA2B3CBFE}");
            }

            public static class GroupParent
            {
                public static ID Group = new ID("{F9E6C052-9189-43DA-B814-E9019C793E99}");
                public static ID Profile = new ID("{851F239D-F387-4724-B756-62179C7F0356}");
            }

            public static class Configuration
            {
                public static ID KireFireKey = new ID("{5A3E6C40-392E-437A-A72A-7C2D9D9CADE4}");
                public static ID SkipIsp = new ID("{66A4FE61-1260-452F-9465-54199854F9F3}");
                public static ID SkipNonUsa = new ID("{99CA527F-E76D-4ACF-A121-152D360EBB07}");
            }
        }

        public static class Profiles
        {
            public static ID Industry = new ID("{8C78D1EC-E978-4EF7-A27F-F70038548230}");
            
            public static class Keys
            {
                public static class Industry
                {
                    public static ID Agriculture = new ID("{9B414501-1EDE-493D-A110-6271CF3D186A}");
                }
            }
        }

        public static class EngagementPlans
        {
            public static ID KickFire = new ID("{3CAD5891-6109-4E77-883F-345DAD698C7D}");

            public static class State
            {
                public static ID BeginingState = new ID("{485D7B77-2029-4E94-6610-D52AC447EBDA}");
            }
        }
    }
}

