using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media;

namespace PrismCommandLab
{
    [Flags]
    public enum ExecutionResult
    {
        WaitingToRun = 1,
        RanToCompletion = 2,
        Faulted = 4,
        Canceled = 8
    }

    public class Fanzhuan : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !System.Convert.ToBoolean(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class CommandExecutionResult2Brush : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var value2 = (ExecutionResult)value;
            return value2 switch
            {
                ExecutionResult.WaitingToRun => Brushes.DarkGray,
                ExecutionResult.Faulted => Brushes.Red,
                ExecutionResult.Canceled => Brushes.Orange,
                ExecutionResult.RanToCompletion => Brushes.LimeGreen,
                _ => throw new ArgumentException()
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public sealed class ExecutionResultNotifier : INotifyPropertyChanged
    {
        private ExecutionResult _executionResult;

        public ExecutionResultNotifier()
        {
            ExecutionResult = ExecutionResult.WaitingToRun;
        }

        public event Action<ExecutionResult>? CommandExecutionResultChanged;

        public event PropertyChangedEventHandler? PropertyChanged;

        public ExecutionResult ExecutionResult
        {
            get => _executionResult;
            private set
            {
                if (!EqualityComparer<ExecutionResult>.Default.Equals(_executionResult, value))
                {
                    _executionResult = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ExecutionResult)));
                    CommandExecutionResultChanged?.Invoke(value);
                }
            }
        }

        public void Reset()
        {
            ExecutionResult = ExecutionResult.WaitingToRun;
        }

        public void SetCanceled(int resetTimeout = Timeout.Infinite)
        {
            ExecutionResult = ExecutionResult.Canceled;
            DelayReset(resetTimeout);
        }

        public void SetFaulted(int resetTimeout = Timeout.Infinite)
        {
            ExecutionResult = ExecutionResult.Faulted;
            DelayReset(resetTimeout);
        }

        public void SetRanToCompletion(int resetTimeout = Timeout.Infinite)
        {
            ExecutionResult = ExecutionResult.RanToCompletion;
            DelayReset(resetTimeout);
        }

        private async void DelayReset(int resetTimeout)
        {
            if (resetTimeout != Timeout.Infinite)
            {
                await Task.Delay(resetTimeout).ConfigureAwait(false);
                try
                {
                    Reset();
                }
                catch
                {
                    // ignored
                }
            }
        }
    }

    public class Person : ObservableObject
    {
        public string Name { get; }
    }

    public class TestViewModel : ObservableObject
    {
        public TestViewModel()
        {
            SaveCommand = new DelegateCommand(() => { })
                .ObservesProperty(() => Person)
                .ObservesProperty(() => Person.Name);
            this.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(Person))
                {
                    SaveCommand.RaiseCanExecuteChanged();
                }
            };

            this.Person.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(Person.Name))
                {
                    SaveCommand.RaiseCanExecuteChanged();
                }
            };
        }

        public Person Person { get; set; }
        public DelegateCommand SaveCommand { get; private set; }
    }

    internal class MyClass : PubSubEvent
    {
    }
}

public class MainWindowViewModel : ObservableObject
{
    private string _info;

    public MainWindowViewModel()
    {
        AssignCommands();

        this.PropertyChanged += (sender, args) =>
        {
            if (args.PropertyName == nameof(Info))
            {
                SaveCommand!.RaiseCanExecuteChanged();
            }
        };
    }

    public string Info
    {
        get => _info;
        set => SetProperty(ref _info, value);
    }

    public DelegateCommand SaveCommand { get; private set; }

    private void AssignCommands()
    {
        SaveCommand = new DelegateCommand(SaveExecute, SaveCanExecute).ObservesProperty(() => Info);
    }

    private bool SaveCanExecute() => Convert.ToString(Info) != null;

    private void SaveExecute()
    {
        try
        {
            File.WriteAllText("D:\\info.txt", Convert.ToString(Info));
        }
        catch (Exception e)
        {
        }
    }
}