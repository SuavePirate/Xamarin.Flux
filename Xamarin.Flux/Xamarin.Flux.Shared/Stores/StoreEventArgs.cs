using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xamarin.Flux.Stores
{
    /// <summary>
    /// Event args for an emitted event from a store
    /// </summary>
    public class StoreEventArgs : EventArgs
    {
        public string EventType { get; set; }
        public StoreEventArgs(string eventType)
        {
            EventType = eventType;
        }
    }
}
