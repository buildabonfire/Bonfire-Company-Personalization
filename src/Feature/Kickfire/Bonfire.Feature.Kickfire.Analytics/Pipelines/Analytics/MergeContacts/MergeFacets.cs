using System;
using Bonfire.Feature.Kickfire.Analytics.Constants;
using Bonfire.Feature.Kickfire.Analytics.Helpers;
using Sitecore.Analytics.Model.Framework;
using Sitecore.Analytics.Pipelines.MergeContacts;
using Sitecore.Diagnostics;

namespace Bonfire.Feature.Kickfire.Analytics.Pipelines.Analytics.MergeContacts
{
    class MergeFacets : MergeContactProcessor
    {
        public override void Process(MergeContactArgs args)
        {
            Log.Info("KickFire: ==== MERGING =====", "KickFire");
            
            Assert.ArgumentNotNull((object)args, "args");
            foreach (var name in args.DyingContact.Facets.Keys)
            {
                var source = args.DyingContact.Facets[name];
                var destination = (IFacet)null;
                try
                {
                    destination = args.SurvivingContact.GetFacet<IFacet>(name);
                }
                catch (FacetNotAvailableException ex)
                {
                }

                // Check if the name of the Facet is the name of our custom Facet
                if (name.Equals(Strings.Analytics.CompanyData,
                    StringComparison.InvariantCultureIgnoreCase) && destination != null && !source.IsEmpty)
                {
                    AnalyticsHelper.DeepCopyFacet(source, destination);
                }
                else if (destination != null && destination.IsEmpty && !source.IsEmpty)
                {
                    ModelUtilities.DeepCopyFacet(source, destination);
                }
            }
        }
    }
}
