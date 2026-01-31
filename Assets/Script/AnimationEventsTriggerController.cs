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
        Event10,
    }

    public class AnimationEventsTriggerController : MonoBehaviour
    {
        public SerializedDictionary<EventsAnimation, UnityEvent> animationsEvent;
    }
}
