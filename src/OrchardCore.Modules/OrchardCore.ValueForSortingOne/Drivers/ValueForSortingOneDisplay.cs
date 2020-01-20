using System.Threading.Tasks;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.ValueForSortingOne.Models;
using OrchardCore.ValueForSortingOne.ViewModels;

namespace OrchardCore.ValueForSortingOne.Drivers
{
    public class ValueForSortingOneDisplay : ContentPartDisplayDriver<ValueForSortingOnePart>
    {
        public override IDisplayResult Display(ValueForSortingOnePart valueForSortingOnePart)
        {
            return Initialize<ValueForSortingOnePartViewModel>("ValueForSortingOnePart", model =>
            {
                model.ValueForSortingOne = valueForSortingOnePart.ContentItem.ValueForSortingOne;
                model.ValueForSortingOnePart = valueForSortingOnePart;
            })
            .Location("Detail", "Header:5")
            .Location("Summary", "Header:5");
        }

        public override IDisplayResult Edit(ValueForSortingOnePart valueForSortingOnePart)
        {
            return Initialize<ValueForSortingOnePartViewModel>("ValueForSortingOnePart_Edit", model =>
            {
                model.ValueForSortingOne = valueForSortingOnePart.ContentItem.ValueForSortingOne;
                model.ValueForSortingOnePart = valueForSortingOnePart;

                return Task.CompletedTask;
            });
        }

        public override async Task<IDisplayResult> UpdateAsync(ValueForSortingOnePart model, IUpdateModel updater)
        {
            await updater.TryUpdateModelAsync(model, Prefix, t => t.ValueForSortingOne);

            model.ContentItem.ValueForSortingOne = model.ValueForSortingOne;

            return Edit(model);
        }
    }
}
