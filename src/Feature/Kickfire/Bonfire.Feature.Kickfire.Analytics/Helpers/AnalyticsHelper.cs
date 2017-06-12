using System.Collections.Generic;
using System.Web.Mvc;
using Bonfire.Foundation.Kickfire.Library.Model;
using Bonfire.Foundation.Kickfire.Library.Repositories;
using Sitecore.Analytics.Model.Framework;
using Sitecore.Diagnostics;

namespace Bonfire.Feature.Kickfire.Analytics.Helpers
{
    public static class AnalyticsHelper
    {
        internal static void DeepCopyFacet(IFacet source, IFacet destination)
        {
            Assert.ArgumentNotNull((object)source, "source");
            Assert.ArgumentNotNull((object)destination, "destination");
            CopyElement((IElement)source, (IElement)destination);
        }

        internal static void CopyElement(IElement source, IElement destination)
        {
            foreach (var modelMember in (IEnumerable<IModelMember>)source.Members)
            {
                var modelAttributeMember = modelMember as IModelAttributeMember;
                if (modelAttributeMember != null)
                {
                    ((IModelAttributeMember)destination.Members[modelMember.Name]).Value = modelAttributeMember.Value;
                }
                else
                {
                    var dictionaryMember1 = modelMember as IModelDictionaryMember;
                    if (dictionaryMember1 != null)
                    {
                        var dictionaryMember2 = (IModelDictionaryMember)destination.Members[modelMember.Name];

                        foreach (var key in (IEnumerable<string>)dictionaryMember1.Elements.Keys)
                        {
                            var destination1 = dictionaryMember2.Elements.Create(key);
                            CopyElement(dictionaryMember1.Elements[key], destination1);
                        }
                    }
                    else
                    {
                        var collectionMember1 = modelMember as IModelCollectionMember;
                        if (collectionMember1 != null)
                        {
                            var collectionMember2 = (IModelCollectionMember)destination.Members[modelMember.Name];

                            for (var index = 0; index < collectionMember1.Elements.Count; ++index)
                            {
                                var destination1 = collectionMember2.Elements.Create();
                                CopyElement(collectionMember1.Elements[index], destination1);
                            }
                        }
                        else
                        {
                            var modelElementMember1 = modelMember as IModelElementMember;
                            if (modelElementMember1 != null)
                            {
                                var modelElementMember2 = (IModelElementMember)destination.Members[modelElementMember1.Name];
                                CopyElement(modelElementMember1.Element, modelElementMember2.Element);
                            }
                        }
                    }
                }
            }
        }

        internal static SicCodeModel GetKickfireSicCodeModel(string sicCodeValue)
        {
            var sicCode = new SicCodeModel();

            if (!string.IsNullOrWhiteSpace(sicCodeValue))
            {
                Log.Info("KickFire: Calling SicCode API for " + sicCodeValue, "KickFire");

                var sicCodeRepository = DependencyResolver.Current.GetService<ISicCodeRepository>();

                int sic;

                if (int.TryParse(sicCodeValue, out sic))
                    sicCode = sicCodeRepository.GetSicCodeById(sic);
            }
            else
                Log.Info("KickFire: CicCodeValue was null/empty. We should have a SicCode to look up in the database.", "KickFire");

            return sicCode;

        }
    }
}
