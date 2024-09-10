using CommunityToolkit.Mvvm.ComponentModel;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media;

namespace PrismCommandLab
{
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
            var value2 = (ActionStatus)value;
            return value2 switch
            {
                ActionStatus.WaitingToRun => Brushes.DarkGray,
                ActionStatus.Faulted => Brushes.Red,
                ActionStatus.Canceled => Brushes.Orange,
                ActionStatus.RanToCompletion => Brushes.LimeGreen,
                _ => throw new ArgumentException()
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
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