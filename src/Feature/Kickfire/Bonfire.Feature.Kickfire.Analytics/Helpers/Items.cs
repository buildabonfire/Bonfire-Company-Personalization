using System.Linq;
using Bonfire.Feature.Kickfire.Analytics.Constants;
using Sitecore.Data.Items;

namespace Bonfire.Feature.Kickfire.Analytics.Helpers
{
    public static class Items
    {
        public static Item GetSicGroup(string group)
        {
            Item item = null;
            if (!string.IsNullOrWhiteSpace(group))
                item = Constants.Items.GroupParent.Children.FirstOrDefault(x => x.Fields[IDs.Fields.GroupParent.Group].Value == group);

            if (item != null)
                return item;

            item = Constants.Items.GroupParent.Children.FirstOrDefault(x => x.Fields[IDs.Fields.GroupParent.Group].Value.ToLower() == "default");
            return item;
        }

        public static Item GetProfileItemBySicCode(string sicCode)
        {
            var sicCodeItem = Constants.Items.SicParent.Children.FirstOrDefault(
                x => x.Fields[Constants.IDs.Fields.SicParent.Code].Value == sicCode.ToString());


            if (sicCodeItem != null)
            {
                var groupitem =
                    ((Sitecore.Data.Fields.LookupField)sicCodeItem.Fields[Constants.IDs.Fields.SicParent.Grouping])
                        .TargetItem;

                var profileItem = ((Sitecore.Data.Fields.LookupField)groupitem.Fields[Constants.IDs.Fields.GroupParent.Profile]).TargetItem;

                return profileItem;
            }

            return null;

        }
    }
}
