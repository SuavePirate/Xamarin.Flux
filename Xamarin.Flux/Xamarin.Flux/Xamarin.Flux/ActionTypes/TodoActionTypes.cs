using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xamarin.Flux.ActionTypes
{
    /// <summary>
    /// Different types of actions that can be completed within the context of Todo items
    /// </summary>
    public class TodoActionTypes
    {
        public const string ADD_TODO = "add_todo";
        public const string DELETE_COMPLETED_TODOS = "delete_completed_todos";
        public const string DELETE_TODO = "delete_todo";
        public const string EDIT_TODO = "edit_todo";
        public const string START_EDITING_TODO = "start_editing_todo";
        public const string STOP_EDITING_TODO = "stop_editing_todo";
        public const string TOGGLE_ALL_TODOS = "toggle_all_todos";
        public const string TOGGLE_TODO = "toggle_todo";
        public const string UPDATE_DRAFT = "update_draft";
    }
}
