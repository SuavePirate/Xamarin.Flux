using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Xamarin.Flux.ViewModels
{
    public class BasePageViewModel : ViewModelBase
    {
        private bool _isLoading;
        private bool _isEnabled;
        private bool _hasError;
        private string _error;

        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                Set(() => IsLoading, ref _isLoading, value);
            }
        }

        public bool IsEnabled
        {
            get
            {
                return _isEnabled;
            }
            set
            {
                Set(() => IsEnabled, ref _isEnabled, value);
            }
        }
        
        public bool HasError
        {
            get
            {
                return _hasError;
            }
            set
            {
                Set(() => HasError, ref _hasError, value);
            }
        }

        public string Error
        {
            get
            {
                return _error;
            }
            set
            {
                Set(() => Error, ref _error, value);
            }
        }

        public BasePageViewModel()
        {
            Error = null;
            IsLoading = false;
            IsEnabled = true;
            HasError = false;
        }

        protected async Task NavigateAsync(Page page)
        {
            await ((NavigationPage)App.Current.MainPage).Navigation.PushAsync(page);
        }

        protected async Task PopAsync()
        {
            await ((NavigationPage)App.Current.MainPage).Navigation.PopAsync();
        }
    }
}
