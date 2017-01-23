using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Flux.ActionTypes;
using Xamarin.Flux.Models;
using Xamarin.Forms;

namespace Xamarin.Flux.Actions
{
    /// <summary>
    /// Actions to be taken against Todo items
    /// </summary>
    public class TodoActions : IActions
    {
        public void AddTodo(string text)
        {
            MessagingCenter.Send(this, TodoActionTypes.ADD_TODO, text);
        }

        public void DeleteCompletedTodos()
        {
            MessagingCenter.Send(this, TodoActionTypes.DELETE_COMPLETED_TODOS);
        }

        public void DeleteTodo(string id)
        {
            MessagingCenter.Send(this, TodoActionTypes.DELETE_TODO, id);
        }

        public void EditTodo(string id, string text)
        {
            MessagingCenter.Send(this, TodoActionTypes.EDIT_TODO, new Todo
            {
                Id = id,
                Text = text
            });
        }

        public void StartEditingTodo(string id)
        {
            MessagingCenter.Send(this, TodoActionTypes.START_EDITING_TODO, id);
        }

        public void StopEditingTodo()
        {
            MessagingCenter.Send(this, TodoActionTypes.STOP_EDITING_TODO);
        }

        public void ToggleAllTodos()
        {
            MessagingCenter.Send(this, TodoActionTypes.TOGGLE_ALL_TODOS);
        }

        public void ToggleTodo(string id)
        {
            MessagingCenter.Send(this, TodoActionTypes.TOGGLE_TODO);
        }

        public void UpdateDraft(string text)
        {
            MessagingCenter.Send(this, TodoActionTypes.UPDATE_DRAFT, text);
        }
    }
}
