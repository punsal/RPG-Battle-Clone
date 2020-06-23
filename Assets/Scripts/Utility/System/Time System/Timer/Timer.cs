using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.System.Time_System.Data;
using Utility.System.Time_System.Type;

namespace Utility.System.Time_System.Timer
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] private TimeData timeData;
        private TimerActionData[] timerActions;

        public void Initialize(TimeData data, TimerActionData[] actions)
        {
            timeData.SetTime(data);
            timerActions = actions;
            StartCoroutine(TimeCounter());
        }

        public float GetTime() => timeData.GetTime();

        private IEnumerator TimeCounter()
        {
            var waiter = new WaitForSeconds(Time.deltaTime);
            
            foreach (var action in timerActions)
            {
                if (action.ActionType == TimerActionType.OnStart)
                {
                    action.TimerAction.Invoke();
                }
            }
            
            while (timeData.GetTime() > 0f)
            {
                timeData.Tick(Time.deltaTime);

                foreach (var action in timerActions)
                {
                    if (action.ActionType == TimerActionType.OnTick)
                    {
                        action.TimerAction.Invoke();
                    }
                }
                
                yield return waiter;
            }
            
            foreach (var action in timerActions)
            {
                if (action.ActionType == TimerActionType.OnEnd)
                {
                    action.TimerAction.Invoke();
                }
            }
            
            Destroy(gameObject);
        }
    }
}