using CommunityToolkit.Mvvm.ComponentModel;
using System.IO;

namespace PrismCommandLab
{
    public class ViewModel : ObservableObject
    {
        private bool _isAdmin = true;
        private CancellationTokenSource _cancellationTokenSource;

        public ViewModel()
        {
            SaveCommand = new AsyncDelegateCommand<string>(Save, CanSave)
                .ObservesProperty(() => IsAdmin)
                .Catch<TaskCanceledException>(ex => { })
                .Catch(ex => { })
                //.CancellationTokenSourceFactory(() =>
                //{
                //    _cancellationTokenSource = new CancellationTokenSource();
                //    return _cancellationTokenSource.Token;
                //})
                .CancelAfter(TimeSpan.FromSeconds(5));
            //.EnableParallelExecution();

            //PropertyChanged += (sender, args) =>
            //{
            //    if (args.PropertyName == nameof(IsAdmin))
            //    {
            //        SaveCommand.RaiseCanExecuteChanged();
            //    }
            //};
        }

        private bool CanSave(string parameter)
        {
            return true;
        }

        private async Task Save(string parameter)
        {
            var a = SaveCommand.IsExecuting;
            if (SaveCommand.CanExecute(parameter))
            {
                ;
            }

            await File.WriteAllTextAsync("D:\\info.txt", parameter);
        }

        public AsyncDelegateCommand<string> SaveCommand { get; }

        public bool IsAdmin
        {
            get => _isAdmin;
            set => SetProperty(ref _isAdmin, value);
        }
    }
}