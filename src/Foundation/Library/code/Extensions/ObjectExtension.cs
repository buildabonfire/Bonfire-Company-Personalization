namespace Bonfire.Foundation.Kickfire.Library.Extensions
{
    using Newtonsoft.Json;

    public static class ObjectExtension
    {
        public static string Jsonize<T>(this T toSerialize)
        {
            return JsonConvert.SerializeObject(toSerialize);
        }
    }
}
