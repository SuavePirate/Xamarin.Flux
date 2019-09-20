using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Flux.Actions;
using Xamarin.Forms;

namespace Xamarin.Flux.Stores
{
    /// <summary>
    /// Base store to handle shared methods
    /// </summary>
    public class BaseStore<T>
    {
        /// <summary>
        /// Event fired when an event is received from the dispatcher and processed
        /// </summary>
        public event EventHandler<StoreEventArgs> OnEmitted;

        /// <summary>
        /// The primary data in stored
        /// </summary>
        public T Data { get; set; }
        public string Error { get; set; }

        /// <summary>
        /// Emits an event received from the dispatcher to anything subscribed to the handler
        /// </summary>
        /// <param name="eventType"></param>
        protected void Emit(string eventType)
        {
            OnEmitted?.Invoke(this, new StoreEventArgs(eventType));
        }

        /// <summary>
        /// Subscribes to an event from the dispatcher
        /// </summary>
        /// <typeparam name="TData">The type of data the dispatcher is sending</typeparam>
        /// <param name="eventType">The name of the event to subscribe to</param>
        protected virtual void Subscribe<TData>(string eventType)
        {
            MessagingCenter.Subscribe<IActions, TData>(this, eventType, (sender, data) => ReceiveEvent(eventType, data));
        }

        /// <summary>
        /// Subscribes to an event from the dispatcher with no data
        /// </summary>
        /// <param name="eventType">The name of the event to subscribe to</param>
        protected virtual void Subscribe(string eventType)
        {
            MessagingCenter.Subscribe<IActions>(this, eventType, (sender) => ReceiveEvent(eventType, (object)null));
        }

        /// <summary>
        /// Called when an event is received from the dispatcher.
        /// Override this to handle when an event is received from the dispatcher.
        /// </summary>
        /// <typeparam name="TData">The type of data from the dispatcher</typeparam>
        /// <param name="eventType">The name of the event being fied</param>
        /// <param name="data">The data sent from the dispatcher</param>
        protected virtual void ReceiveEvent<TData>(string eventType, TData data)
        {
            Emit(eventType);
        }
    }
}
