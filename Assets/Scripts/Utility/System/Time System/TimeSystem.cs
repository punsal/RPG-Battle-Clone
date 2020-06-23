using UnityEngine;
using Utility.System.Time_System.Data;

namespace Utility.System.Time_System
{
    public class TimeSystem : MonoBehaviour
    {
        public static Timer.Timer CreateTimer(string name, float duration, TimerActionData[] actions)
        {
            var timeData = new TimeData();
            timeData.SetTime(duration);
            
            var temp = new GameObject($"Timer - {name}");
            var timer = temp.AddComponent<Timer.Timer>();
            timer.Initialize(timeData, actions);

            return timer;
        }
    }
}
