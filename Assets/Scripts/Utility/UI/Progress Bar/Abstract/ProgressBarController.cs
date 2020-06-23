using UnityEngine;
using Utility.System.Publisher_Subscriber_System;
using Utility.UI.Progress_Bar.Data;
using Utility.UI.Progress_Bar.Filler;
using Utility.UI.Progress_Bar.Type;

namespace Utility.UI.Progress_Bar.Abstract
{
    public abstract class ProgressBarController : MonoBehaviour
    {
        [Header("Type")]
        [Tooltip("Type is mandatory to identify events.")]
        [SerializeField] private ProgressBarType barType = ProgressBarType.LevelProgression;

        [Header("Filler")]
        [SerializeField]
        protected FillerController fillerController;

        private Subscription<ProgressBarData> progressBarSubscription;
        
        private void OnValidate()
        {
            if (fillerController == null)
            {
                fillerController = GetComponentInChildren<FillerController>();
            }
        }

        private void OnEnable()
        {
            progressBarSubscription = PublisherSubscriber.Subscribe<ProgressBarData>(ProgressBarDataHandler);
        }

        private void OnDisable()
        {
            PublisherSubscriber.Unsubscribe(progressBarSubscription);
        }

        private void ProgressBarDataHandler(ProgressBarData barData)
        {
            if (barData.BarType != barType) return;
            ProgressBarDataDataAction(barData);
        }

        protected abstract void ProgressBarDataDataAction(ProgressBarData barData);
    }
}
