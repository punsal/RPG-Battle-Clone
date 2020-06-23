using TMPro;
using UnityEngine;
using Utility.System.Time_System.Data;

namespace Utility.UI.Progress_Bar.Timer_Progress_Bar
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class ShowTime : MonoBehaviour
    {
        private TextMeshProUGUI text;

        private void OnEnable()
        {
            text = GetComponent<TextMeshProUGUI>();
        }

        private void Start()
        {
            SetTime(0f);
        }

        public void SetTime(float time)
        {
            var timeData = new TimeData();
            timeData.SetTime(time);
            text.text = timeData.ToString();

        }
    }
}