namespace wLib
{
    /// <summary>
    /// Prototype for subscribers action.
    /// </summary>
    /// <param name="eventData">Event data.</param>
    public delegate void EvenetAction<in T>(T eventData);

    public interface IEventBroker
    {
        /// <summary>
        /// Subscribe callback to be raised on specific event.
        /// </summary>
        /// <param name="eventAction">Callback.</param>
        void Subscribe<T>(EvenetAction<T> eventAction);

        /// <summary>
        /// Unsubscribe callback.
        /// </summary>
        /// <param name="eventAction">Event action.</param>
        /// <param name="keepEvent">GC optimization - clear only callback list and keep event for future use.</param>
        void Unsubscribe<T>(EvenetAction<T> eventAction, bool keepEvent = false);

        /// <summary>
        /// Unsubscribe all callbacks from event.
        /// </summary>
        /// <param name="keepEvent">GC optimization - clear only callback list and keep event for future use.</param>
        void UnsubscribeAll<T>(bool keepEvent = false);

        /// <summary>
        /// Unsubscribe all listeneres and clear all events.
        /// </summary>
        void UnsubscribeAndClearAllEvents();

        /// <summary>
        /// Publish event.
        /// </summary>
        /// <param name="eventMessage">Event message.</param>
        void Publish<T>(T eventMessage);

        /// <summary>
        /// Publish event.
        /// </summary>
        void Publish<T>() where T : new();
    }
}