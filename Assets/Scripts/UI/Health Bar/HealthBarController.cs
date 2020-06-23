using UnityEngine;
using Utility.UI.Progress_Bar.Abstract;
using Utility.UI.Progress_Bar.Data;

namespace UI.Health_Bar
{
    public class HealthBarController : ProgressBarController
    {
        [SerializeField] private string id;

        // ReSharper disable once ParameterHidesMember
        public void Construct(string id)
        {
            this.id = id;
        }
        
        protected override void ProgressBarDataDataAction(ProgressBarData barData)
        {
            if (barData.Id == id)
            {
                fillerController.SetFill(barData.CurrentAmount / barData.TotalAmount);
            }
        }
    }
}
