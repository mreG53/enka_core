using Microsoft.Extensions.Localization;
using stokEnka.Resources;
using System.Reflection;

namespace stokEnka.Services
{
    public class LocalizationService
    {
        private readonly IStringLocalizer _localizer;

        public LocalizationService(IStringLocalizerFactory factory)
        {
            var type = typeof(ApplicationResource);
            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
            _localizer = factory.Create("ApplicationResource", assemblyName.Name!);
        }

        public LocalizedString GetLocalizedHtmlString(string key)
        {
            var result = _localizer[key];
            return result;
        }
    }
}
