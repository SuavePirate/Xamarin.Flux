using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Flux.Actions;
using Xamarin.Flux.ActionTypes;
using Xamarin.Flux.Models;
using Xamarin.Forms;

namespace Xamarin.Flux.Stores
{
    /// <summary>
    /// Store for todo draft items
    /// </summary>
    public class TodoDraftStore : BaseStore<string>
    {

        /// <summary>
        /// Create new store and subscribe to the message center
        /// </summary>
        public TodoDraftStore()
        {
            Subscribe<Todo>(TodoActionTypes.ADD_TODO);
            Subscribe<string>(TodoActionTypes.UPDATE_DRAFT);
        }

        /// <summary>
        /// Handle processing events before emitting
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="eventType"></param>
        /// <param name="data"></param>
        protected override void ReceiveEvent<T>(string eventType, T data)
        {
            try
            {
                Error = null;
                switch (eventType)
                {
                    case TodoActionTypes.ADD_TODO:
                        Data = string.Empty;
                        break;
                    case TodoActionTypes.UPDATE_DRAFT:
                        Data = data as string;
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
