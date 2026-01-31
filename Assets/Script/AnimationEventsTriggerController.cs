using System;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;
using UnityEngine.Events;

namespace GameJam
{
    public enum EventsAnimation
    {
        Event1,
        Event2,
        Event3,
        Event4,
        Event5,
        Event6,
        Event7,
        Event8,
        Event9,
        Event10
    }

    public class AnimationEventsTriggerController : MonoBehaviour
    {
        public List<AnimationClass> animationsEvent = new List<AnimationClass>();


        public void ExecuteEvent(EventsAnimation eventsAnimation)
        {
            foreach (var eventAnim in animationsEvent)
            {
                if (eventAnim.EventsAnimation == eventsAnimation)
                {
                    eventAnim._UnityEvent?.Invoke();
                    return;
                }
            }
        }
    }

    [Serializable]
    public class AnimationClass
    {
        public EventsAnimation EventsAnimation;
        public UnityEvent _UnityEvent;
    }
}
