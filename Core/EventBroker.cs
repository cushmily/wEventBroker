using System;
using System.Collections.Generic;
using UnityEngine;

namespace wLib
{
    /// <summary>
    /// IEventBroker implementation.
    /// </summary>
    public sealed class EventBroker : IEventBroker
    {
        private const int MaxCallDepth = 5;

        private readonly Dictionary<Type, Delegate> _events = new Dictionary<Type, Delegate>(32);
        private readonly Dictionary<Type, object> _cachedEvents = new Dictionary<Type, object>();

        private int _eventsInCall;

        public void Subscribe<T>(EvenetAction<T> eventAction)
        {
            if (eventAction != null)
            {
                var eventType = typeof(T);
                Delegate rawList;
                _events.TryGetValue(eventType, out rawList);
                _events[eventType] = (rawList as EvenetAction<T>) + eventAction;
            }
        }

        public void Unsubscribe<T>(EvenetAction<T> eventAction, bool keepEvent = false)
        {
            if (eventAction != null)
            {
                var eventType = typeof(T);
                Delegate rawList;
                if (_events.TryGetValue(eventType, out rawList))
                {
                    if (rawList != null)
                    {
                        var list = (EvenetAction<T>) rawList - eventAction;
                        if (list == null && !keepEvent) { _events.Remove(eventType); }
                        else { _events[eventType] = list; }
                    }
                }
            }
        }

        public void UnsubscribeAll<T>(bool keepEvent = false)
        {
            var eventType = typeof(T);
            Delegate rawList;
            if (_events.TryGetValue(eventType, out rawList))
            {
                if (keepEvent) { _events[eventType] = null; }
                else { _events.Remove(eventType); }
            }
        }

        public void UnsubscribeAndClearAllEvents()
        {
            _events.Clear();
        }

        public void Publish<T>(T eventMessage)
        {
            if (_eventsInCall >= MaxCallDepth)
            {
#if UNITY_EDITOR
                Debug.LogError("Max call depth reached");
#endif
                return;
            }

            var eventType = typeof(T);
            Delegate rawList;
            _events.TryGetValue(eventType, out rawList);
            var list = rawList as EvenetAction<T>;
            if (list != null)
            {
                _eventsInCall++;
                try { list(eventMessage); }
                catch (Exception ex) { Debug.LogError(ex); }

                _eventsInCall--;
            }
        }

        public void Publish<T>() where T : new()
        {
            var type = typeof(T);
            if (!_cachedEvents.ContainsKey(type)) { _cachedEvents.Add(type, new T()); }

            var evt = (T) _cachedEvents[type];
            Publish(evt);
        }
    }
}