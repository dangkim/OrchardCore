using System.Threading.Tasks;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.ValueForSortingTwo.Model;
using OrchardCore.ValueForSortingTwo.ViewModels;

namespace OrchardCore.ValueForSortingTwo.Drivers
{
    public class ValueForSortingTwoPartDisplay : ContentPartDisplayDriver<ValueForSortingTwoPart>
    {
        public override IDisplayResult Display(ValueForSortingTwoPart valueForSortingTwoPart)
        {
            return Initialize<ValueForSortingTwoPartViewModel>("ValueForSortingTwoPart", model =>
            {
                model.ValueForSortingTwo = valueForSortingTwoPart.ContentItem.ValueForSortingTwo;
                model.ValueForSortingTwoPart = valueForSortingTwoPart;
            })
            .Location("Detail", "Header:5")
            .Location("Summary", "Header:5");
        }

        public override IDisplayResult Edit(ValueForSortingTwoPart valueForSortingTwoPart)
        {
            return Initialize<ValueForSortingTwoPartViewModel>("ValueForSortingTwoPart_Edit", model =>
            {
                model.ValueForSortingTwo = valueForSortingTwoPart.ContentItem.ValueForSortingTwo;
                model.ValueForSortingTwoPart = valueForSortingTwoPart;

                return Task.CompletedTask;
            });
        }

        public override async Task<IDisplayResult> UpdateAsync(ValueForSortingTwoPart model, IUpdateModel updater)
        {
            await updater.TryUpdateModelAsync(model, Prefix, t => t.ValueForSortingTwo);

            model.ContentItem.ValueForSortingTwo = model.ValueForSortingTwo;

            return Edit(model);
        }
    }
}
