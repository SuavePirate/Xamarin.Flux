using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xamarin.Flux.Models
{
    /// <summary>
    /// Data model for a todo item
    /// </summary>
    public class Todo
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public bool IsComplete { get; set; }
    }
}
