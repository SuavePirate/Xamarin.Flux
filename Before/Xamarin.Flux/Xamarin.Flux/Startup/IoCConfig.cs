using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Flux.Actions;
using Xamarin.Flux.Stores;
using Xamarin.Flux.ViewModels;

namespace Xamarin.Flux.Startup
{
    /// <summary>
    /// Wire up some basic IoC for dependency injection
    /// </summary>
    public class IoCConfig
    {

        public void RegisterActions()
        {
            SimpleIoc.Default.Register<TodoActions>();
        }

        public void RegisterStores()
        {
            SimpleIoc.Default.Register<TodoStore>();
        }

        public void RegisterViewModels()
        {
            SimpleIoc.Default.Register<TodoListPageViewModel>();
        }
    }
}
