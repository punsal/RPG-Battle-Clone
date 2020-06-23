using Utility.UI.Progress_Bar.Abstract;
using Utility.UI.Progress_Bar.Data;

namespace Utility.UI.Progress_Bar.Simple_Progress_Bar
{
    public class SimpleProgressBarController : ProgressBarController
    {
        protected override void ProgressBarDataDataAction(ProgressBarData barData)
        {
            fillerController.SetFill(barData.CurrentAmount / barData.TotalAmount);
        }
    }
}