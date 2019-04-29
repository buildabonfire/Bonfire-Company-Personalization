using Bonfire.Feature.KickfireCore.Models.Goals;
using Sitecore.XConnect.Schema;

namespace Bonfire.Feature.KickfireCore.Models.DataModel
{
    public class IdentifiedGoalDataModel
    {
        public static XdbModel Model { get; } = IdentifiedGoalDataModel.BuildModel();

        private static XdbModel BuildModel()
        {
            XdbModelBuilder modelBuilder = new XdbModelBuilder("IdentifiedGoalDataModel", new XdbModelVersion(0, 1));

            modelBuilder.ReferenceModel(Sitecore.XConnect.Collection.Model.CollectionModel.Model);
            modelBuilder.DefineEventType<IdentifiedGoal>(false);

            return modelBuilder.BuildModel();
        }
    }
}