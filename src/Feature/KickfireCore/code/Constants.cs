using Sitecore.Data;

namespace Bonfire.Feature.KickfireCore
{
    public static class Constants
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

        public struct Profile
        {
            public static string IndustryName = "Industry";
        }
    }
}