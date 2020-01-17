using System.Threading.Tasks;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.ValueForSortingThree.Model;
using OrchardCore.ValueForSortingThree.ViewModels;

namespace OrchardCore.ValueForSortingThree.Drivers
{
    public class ValueForSortingThreePartDisplay : ContentPartDisplayDriver<ValueForSortingThreePart>
    {
        public override IDisplayResult Display(ValueForSortingThreePart valueForSortingThreePart)
        {
            return Initialize<ValueForSortingThreePartViewModel>("ValueForSortingThreePart", model =>
            {
                model.ValueForSortingThree = valueForSortingThreePart.ContentItem.ValueForSortingThree;
                model.ValueForSortingThreePart = valueForSortingThreePart;
            })
            .Location("Detail", "Header:5")
            .Location("Summary", "Header:5");
        }

        public override IDisplayResult Edit(ValueForSortingThreePart valueForSortingThreePart)
        {
            return Initialize<ValueForSortingThreePartViewModel>("ValueForSortingThreePart_Edit", model =>
            {
                model.ValueForSortingThree = valueForSortingThreePart.ContentItem.ValueForSortingThree;
                model.ValueForSortingThreePart = valueForSortingThreePart;

                return Task.CompletedTask;
            });
        }

        public override async Task<IDisplayResult> UpdateAsync(ValueForSortingThreePart model, IUpdateModel updater)
        {
            await updater.TryUpdateModelAsync(model, Prefix, t => t.ValueForSortingThree);

            model.ContentItem.ValueForSortingThree = model.ValueForSortingThree;

            return Edit(model);
        }
    }
}
