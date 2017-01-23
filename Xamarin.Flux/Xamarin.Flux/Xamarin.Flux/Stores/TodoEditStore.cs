using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Flux.ActionTypes;

namespace Xamarin.Flux.Stores
{
    /// <summary>
    /// Store for data being edited for a todo item
    /// </summary>
    public class TodoEditStore : BaseStore<string>
    {
        /// <summary>
        /// Creates a new store and subscribes to events from the dispatcher
        /// </summary>
        public TodoEditStore()
        {
            Subscribe<string>(TodoActionTypes.START_EDITING_TODO);
            Subscribe(TodoActionTypes.STOP_EDITING_TODO);
        }

        /// <summary>
        /// Handles an event received from the dispatcher before emitting.
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="eventType"></param>
        /// <param name="data"></param>
        protected override void ReceiveEvent<TData>(string eventType, TData data)
        {
            try
            {
                Error = null;
                switch (eventType)
                {
                    case TodoActionTypes.START_EDITING_TODO:
                        Data = data as string;
                        break;
                    case TodoActionTypes.STOP_EDITING_TODO:
                        Data = string.Empty;
                        break;
                }
            }
            catch(Exception ex)
            {
                Error = ex.Message;
            }

            base.ReceiveEvent(eventType, data);
        }
    }
}
