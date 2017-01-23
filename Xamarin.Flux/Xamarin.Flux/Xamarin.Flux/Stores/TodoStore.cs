using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Flux.ActionTypes;
using Xamarin.Flux.Models;

namespace Xamarin.Flux.Stores
{
    /// <summary>
    /// Event store for holding and managing todo items
    /// </summary>
    public class TodoStore : BaseStore<List<Todo>>
    {
        /// <summary>
        /// Creates a new store and handles subscriptions to the dispatcher
        /// </summary>
        public TodoStore()
        {
            Subscribe<Todo>(TodoActionTypes.ADD_TODO);
            Subscribe(TodoActionTypes.DELETE_COMPLETED_TODOS);
            Subscribe<string>(TodoActionTypes.DELETE_TODO);
            Subscribe<Todo>(TodoActionTypes.EDIT_TODO);
            Subscribe(TodoActionTypes.TOGGLE_ALL_TODOS);
            Subscribe<string>(TodoActionTypes.TOGGLE_TODO);

            Data = new List<Todo>();
        }

        /// <summary>
        /// Processes an event from the dispatcher before emitting it.
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
                    case TodoActionTypes.ADD_TODO:
                        Data.Add(new Todo
                        {
                            Id = Guid.NewGuid().ToString(),
                            Text = data as string,
                            IsComplete = false
                        });
                        break;
                    case TodoActionTypes.DELETE_COMPLETED_TODOS:
                        Data.RemoveAll(t => t.IsComplete);
                        break;
                    case TodoActionTypes.DELETE_TODO:
                        var itemToRemove = Data.FirstOrDefault(t => t.Id == data as string);
                        if (itemToRemove != null)
                            Data.Remove(itemToRemove);
                        break;
                    case TodoActionTypes.EDIT_TODO:
                        var itemToEdit = Data.FirstOrDefault(t => t.Id == (data as Todo).Id);
                        if (itemToEdit != null)
                            itemToEdit.Text = (data as Todo).Text;
                        break;
                    case TodoActionTypes.TOGGLE_ALL_TODOS:
                        var areIncomplete = Data.Any(t => !t.IsComplete);
                        foreach(var todo in Data)
                        {
                            todo.IsComplete = !areIncomplete;
                        }
                        break;
                    case TodoActionTypes.TOGGLE_TODO:
                        var itemToToggle = Data.First(t => t.Id == (data as string));
                        if (itemToToggle != null)
                            itemToToggle.IsComplete = !itemToToggle.IsComplete;
                        break;

                }
            }
            catch (Exception ex)
            {
                // if something goes wrong, set the error before emitting
                Error = ex.Message;
            }
           
            base.ReceiveEvent(eventType, data);
        }
    }
}
