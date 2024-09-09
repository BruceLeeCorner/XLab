using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prism.Commands
{
    public sealed class DelegateCommandEx : DelegateCommand
    {
        public bool CanExecuteOnView => CanExecute();

        public DelegateCommandEx(Action executeMethod) : base(executeMethod)
        {
            this.CanExecuteChanged += (sender, args) =>
            {
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(CanExecuteOnView)));
            };
        }

        public DelegateCommandEx(Action executeMethod, Func<bool> canExecuteMethod) : base(executeMethod, canExecuteMethod)
        {
            this.CanExecuteChanged += (sender, args) =>
            {
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(CanExecuteOnView)));
            };
        }

        protected override bool CanExecute(object? parameter)
        {
            return base.CanExecute(parameter);
        }
    }

    public sealed class DelegateCommandEx<T> : DelegateCommand<T>
    {
        public bool CanExecuteOnView => CanExecute(_value);

        private T _value;

        public DelegateCommandEx(Action<T> executeMethod) : base(executeMethod)
        {
            this.CanExecuteChanged += (sender, args) =>
            {
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(CanExecuteOnView)));
            };
        }

        public DelegateCommandEx(Action<T> executeMethod, Func<T, bool> canExecuteMethod) : base(executeMethod, canExecuteMethod)
        {

        }

        protected override bool CanExecute(object? parameter)
        {
            _value = (T)parameter;
            
            return base.CanExecute(parameter);
        }
    }
}