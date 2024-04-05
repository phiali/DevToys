#nullable enable

using System.Composition;
using DevToys.Shared.Api.Core;
using DevToys.Api.Tools;
using DevToys.Core.Threading;
using Windows.UI.Xaml.Controls;
using DevToys.Helpers.JsonYaml;

namespace DevToys.ViewModels.Tools.ChmodCalculator
{
    [Export(typeof(IToolProvider))]
    [Name("Chmod Calculator")]
    [Parent(ConvertersGroupToolProvider.InternalName)]
    [ProtocolName("chmodcalculator")]
    [Order(0)]
    [NotScrollable]
    internal sealed class ChmodCalculatorToolProvider : IToolProvider
    {
        private readonly IMefProvider _mefProvider;

        public string MenuDisplayName => LanguageManager.Instance.ChmodCalculator.MenuDisplayName;

        public string? SearchDisplayName => LanguageManager.Instance.ChmodCalculator.SearchDisplayName;

        public string? Description => LanguageManager.Instance.ChmodCalculator.Description;

        public string AccessibleName => LanguageManager.Instance.ChmodCalculator.AccessibleName;

        public string? SearchKeywords => LanguageManager.Instance.ChmodCalculator.SearchKeywords;

        public string IconGlyph => "\u0109";

        [ImportingConstructor]
        public ChmodCalculatorToolProvider(IMefProvider mefProvider)
        {
            _mefProvider = mefProvider;
        }

        public bool CanBeTreatedByTool(string data)
        {
            return JsonHelper.IsValid(data) || YamlHelper.IsValidYaml(data);
        }

        public IToolViewModel CreateTool()
        {
            return _mefProvider.Import<ChmodCalculatorToolViewModel>();
        }
    }
}
