using System.Threading.Tasks;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.SearchA.Model;
using OrchardCore.SearchA.ViewModels;

namespace OrchardCore.SearchA.Drivers
{
    public class SearchAPartDisplay : ContentPartDisplayDriver<SearchAPart>
    {
        public override IDisplayResult Display(SearchAPart searchAPart)
        {
            return Initialize<SearchAPartViewModel>("SearchAPart", model =>
            {
                //model.Title = titlePart.ContentItem.DisplayText;
                model.SearchAPart = searchAPart;
            })
            .Location("Detail", "Header:5")
            .Location("Summary", "Header:5");
        }

        public override IDisplayResult Edit(SearchAPart searchAPart)
        {
            return Initialize<SearchAPartViewModel>("SearchAPart_Edit", model =>
            {
                //model.Title = titlePart.ContentItem.DisplayText;
                model.SearchAPart = searchAPart;

                return Task.CompletedTask;
            });
        }

        public override async Task<IDisplayResult> UpdateAsync(SearchAPart model, IUpdateModel updater)
        {
            await updater.TryUpdateModelAsync(model, Prefix, t => t.SearchValue);

            //model.ContentItem.DisplayText = model.Title;

            return Edit(model);
        }
    }
}
