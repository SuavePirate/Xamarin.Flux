using GalaSoft.MvvmLight;
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
    public class Todo : ViewModelBase
    {
        private string _id;
        private string _text;
        private bool _isComplete;

        public string Id
        {
            get
            {
                return _id;
            }
            set
            {
                Set(() => Id, ref _id, value);
            }
        }
        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                Set(() => Text, ref _text, value);
            }
        }
        public bool IsComplete
        {
            get
            {
                return _isComplete;
            }
            set
            {
                Set(() => IsComplete, ref _isComplete, value);
            }
        }
    }
}
