using System;

namespace Utility.System.Time_System.Data
{
    [Serializable]
    public struct TimeData
    {
        public int minutes;
        public float seconds;

        public void SetTime(float duration)
        {
            minutes = (int) (duration / 60f);
            seconds = duration % 60f;
        }

        public void SetTime(TimeData data)
        {
            minutes = data.minutes;
            seconds = data.seconds;
        }

        public float GetTime()
        {
            return minutes * 60f + seconds;
        }

        public void Tick(float duration)
        {
            var currentTime = GetTime();
            SetTime(currentTime - duration);
        }

        public override string ToString()
        {
            return $"{minutes} : {((int)seconds):D2}";
        }
    }
}