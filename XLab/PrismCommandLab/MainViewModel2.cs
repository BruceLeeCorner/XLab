using System.Net.Http;

namespace PrismCommandLab;

public class MainViewModel2 : BindableBase
{
    private string _data;

    public MainViewModel2()
    {
        AssignCommands();

        //DownloadActionStatusNotifier.CommandExecutionResultChanged += DownloadExecutionResultNotifierOnExecutionResultChanged;
    }

    public string Data
    {
        get => _data;
        set
        {
            if (value == _data) return;
            _data = value;
            RaisePropertyChanged();
        }
    }

    public AsyncDelegateCommand DownloadCommand { get; private set; }

    public ActionStatusNotifier DownloadActionStatusNotifier { get; private set; }

    private void AssignCommands()
    {

        

        DownloadCommand.CanExecuteChanged += (sender, args) =>
        {

        };


        DownloadActionStatusNotifier = new ActionStatusNotifier();
        DownloadCommand = new AsyncDelegateCommand(DownloadExecute, DownloadCanExecute)
            .CancelAfter(TimeSpan.FromSeconds(5))
            .Catch<TaskCanceledException>(ex =>
                DownloadActionStatusNotifier.SetCanceled(2000)
            )
            .Catch(ex =>
                DownloadActionStatusNotifier.SetFaulted(2000));
    }

    private bool DownloadCanExecute()
    {
        return DownloadActionStatusNotifier.ActionStatus == ActionStatus.WaitingToRun;
    }

    //private void DownloadExecutionResultNotifierOnExecutionResultChanged(ActionStatus obj)
    //{
    //    if (obj == ActionStatus.RanToCompletion || obj == ActionStatus.Faulted)
    //    {
    //        EventAggregator eventAggregator = new EventAggregator();
    //        eventAggregator.GetEvent<MyClass>().Publish();
    //    }
    //}

    private async Task DownloadExecute(CancellationToken cancellationToken)
    {
        HttpClient client = new HttpClient();
        var number = Random.Shared.Next(0, 10) % 3;
        if (number == 0)
        {
            Data = await client.GetStringAsync("https://medium.com/@cilliemalan/how-to-await-a-cancellation-token-in-c-cbfc88f28fa2", cancellationToken).ConfigureAwait(false);
        }
        else if (number == 1)
        {
            await Task.Delay(9000, cancellationToken);
            Data = "";
        }
        else
        {
            await Task.Delay(1000, cancellationToken);
            Data = "";
            throw new ArgumentOutOfRangeException();
        }

        DownloadActionStatusNotifier.SetRanToCompletion();
    }
}