using System.IO;
using Sitecore.XConnect.Serialization;

namespace KickfireCore.Model.Builder
{
    class Program
    {
        static void Main(string[] args)
        {
            var json = XdbModelWriter.Serialize(Bonfire.Feature.KickfireCore.Models.DataModel.CompanyDataModel.Model);
            File.WriteAllText(Bonfire.Feature.KickfireCore.Models.DataModel.CompanyDataModel.Model + ".json", json);

        }
    }
}
