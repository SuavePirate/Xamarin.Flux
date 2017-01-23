using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Flux.Startup;

namespace Xamarin.Flux.ViewModels
{
    /// <summary>
    /// Helper class for auto binding view models and kicking off the IoC container
    /// </summary>
    public class ViewModelLocator
    {
        public TodoListPageViewModel TodoListPage
        {
            get
            {
                return ServiceLocator.Current.GetInstance<TodoListPageViewModel>();
            }
        }
        public ViewModelLocator()
        {
            var iocConfig = new IoCConfig();
            iocConfig.RegisterStores();
            iocConfig.RegisterActions();
            iocConfig.RegisterViewModels();
        }
    }
}
