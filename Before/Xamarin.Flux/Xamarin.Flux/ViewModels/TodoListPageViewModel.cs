using Acr.UserDialogs;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Flux.Models;
using Xamarin.Forms;

namespace Xamarin.Flux.ViewModels
{
    public class TodoListPageViewModel : BasePageViewModel
    {
        private ObservableCollection<Todo> _items = new ObservableCollection<Todo>();
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
                            Items.Add(new Todo
                            {
                                Id = Guid.NewGuid().ToString(),
                                Text = result.Text
                            });
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
                            var item = Items.FirstOrDefault(i => i.Id == t.Id);
                            item.Text = result.Text;
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
                        var item = Items.FirstOrDefault(i => i.Id == t.Id);
                        item.IsComplete = !item.IsComplete;
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
                        foreach(var item in Items)
                        {
                            item.IsComplete = true;
                        }
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
                        var item = Items.FirstOrDefault(i => i.Id == t.Id);
                        Items.Remove(item);
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
                        foreach(var todo in Items.ToList())
                        {
                            if (todo.IsComplete)
                                Items.Remove(todo);
                        }
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
                            Items.Add(new Todo { Text = $"New Item {i}", Id = Guid.NewGuid().ToString() });
                            Task.Delay(200);
                        }
                    }));
            }
        }

        public ObservableCollection<Todo> Items
        {
            get
            {
                return _items;
            }
            set
            {
                Set(() => Items, ref _items, value);
            }
        }
    }
}
