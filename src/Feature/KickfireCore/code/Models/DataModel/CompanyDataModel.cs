using Bonfire.Feature.KickfireCore.Models.Facets;

namespace Bonfire.Feature.KickfireCore.Models.DataModel
{
    using Sitecore.XConnect;
    using Sitecore.XConnect.Schema;

    public class CompanyDataModel
    {
        public static XdbModel Model { get; } = BuildModel();

        private static XdbModel BuildModel()
        {
            var modelBuilder = new XdbModelBuilder("CompanyDataModel", new XdbModelVersion(1, 0));

            modelBuilder.ReferenceModel(Sitecore.XConnect.Collection.Model.CollectionModel.Model);

            modelBuilder.DefineFacet<Contact, CompanyFacet>(CompanyFacet.DefaultFacetKey);

            return modelBuilder.BuildModel();

        }
    }
}