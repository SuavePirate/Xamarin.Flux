using Acr.UserDialogs;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Flux.Actions;
using Xamarin.Flux.ActionTypes;
using Xamarin.Flux.Models;
using Xamarin.Flux.Stores;
using Xamarin.Forms;

namespace Xamarin.Flux.ViewModels
{
    public class TodoListPageViewModel : BasePageViewModel
    {
        private readonly TodoStore _todoStore;
        private readonly TodoActions _todoActions;
        private ObservableCollection<Todo> _items;
        private ICommand _createCommand;
        private ICommand _toggleCommand;
        private ICommand _toggleAllCommand;
        private ICommand _deleteCommand;
        private ICommand _deleteCompletedCommand;
        private ICommand _editCommand;
        private ICommand _populateCommand;

        public ICommand CreateCommand
        {
            get
            {
                return _createCommand ??
                    (_createCommand = new RelayCommand(async () =>
                    {
                        var result = await UserDialogs.Instance.PromptAsync(string.Empty, "New", "Done", "Cancel", "Todo...");
                        if (result.Ok)
                        {
                            _todoActions.AddTodo(result.Text);
                        }
                    }));
            }
        }

        public ICommand EditCommand
        {
            get
            {
                return _editCommand ??
                    (_editCommand = new RelayCommand<Todo>(async (t) =>
                    {
                        var result = await UserDialogs.Instance.PromptAsync(new PromptConfig()
                            .SetText(t.Text)
                            .SetTitle("Edit")
                            .SetOkText("Done")
                            .SetCancelText("Cancel")
                            .SetPlaceholder("Todo..."));
                        if (result.Ok)
                        {
                            _todoActions.EditTodo(t.Id, result.Text);
                        }
                    }));
            }
        }

        public ICommand ToggleCommand
        {
            get
            {
                return _toggleCommand ??
                    (_toggleCommand = new RelayCommand<Todo>((t) =>
                    {
                        _todoActions.ToggleTodo(t.Id);
                    }));
            }
        }

        public ICommand ToggleAllCommand
        {
            get
            {
                return _toggleAllCommand ??
                    (_toggleAllCommand = new RelayCommand(() =>
                    {
                        _todoActions.ToggleAllTodos();
                    }));
            }
        }

        public ICommand DeleteCommand
        {
            get
            {
                return _deleteCommand ??
                    (_deleteCommand = new RelayCommand<Todo>((t) =>
                    {
                        _todoActions.DeleteTodo(t.Id);
                    }));
            }
        }

        public ICommand DeleteCompletedCommand
        {
            get
            {
                return _deleteCompletedCommand ??
                    (_deleteCompletedCommand = new RelayCommand(() =>
                    {
                        _todoActions.DeleteCompletedTodos();
                    }));
            }
        }

        public ICommand PopulateCommand
        {
            get
            {
                return _populateCommand ??
                    (_populateCommand = new RelayCommand(() =>
                    {
                        for(var i = 1; i < 20; i++)
                        {
                            _todoActions.AddTodo($"New Item {i}");
                            Task.Delay(200);
                        }
                    }));
            }
        }

        public ObservableCollection<Todo> Items
        {
            get
            {
                return _todoStore.Data;
            }
        }

        public TodoListPageViewModel(TodoStore todoStore, TodoActions todoActions)
        {
            _todoStore = todoStore;
            _todoActions = todoActions;
            _todoStore.OnEmitted += TodoStore_OnEmitted;
        }

        /// <summary>
        /// Processes events from the todo store and updates any UI that isn't handled automatically
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TodoStore_OnEmitted(object sender, StoreEventArgs e)
        {
            switch (e.EventType)
            {
                case TodoActionTypes.ADD_TODO:
                    if(_todoStore.Error == null)
                    {
                        UserDialogs.Instance.Toast("Item added");
                    }
                    break;
                case TodoActionTypes.DELETE_COMPLETED_TODOS:
                    if (_todoStore.Error == null)
                    {
                        UserDialogs.Instance.Toast("Items deleted");
                    }
                    break;
                case TodoActionTypes.DELETE_TODO:
                    if (_todoStore.Error == null)
                    {
                        UserDialogs.Instance.Toast("Item deleted");
                    }
                    break;
                case TodoActionTypes.TOGGLE_ALL_TODOS:
                    if (_todoStore.Error == null)
                    {
                        UserDialogs.Instance.Toast("Items toggled");
                    }
                    break;
                case TodoActionTypes.TOGGLE_TODO:
                    if (_todoStore.Error == null)
                    {
                        UserDialogs.Instance.Toast("Item toggled");
                    }
                    break;
            }
            if(_todoStore.Error != null)
            {
                UserDialogs.Instance.Alert(_todoStore.Error);
            }
        }
    }
}
