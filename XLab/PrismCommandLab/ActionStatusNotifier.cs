using System.ComponentModel;

namespace PrismCommandLab;

public sealed class ActionStatusNotifier : INotifyPropertyChanged
{
    private ActionStatus _actionStatus;

    public ActionStatusNotifier()
    {
        ActionStatus = ActionStatus.WaitingToRun;
    }

    public event Action<ActionStatus>? CommandExecutionResultChanged;

    public event PropertyChangedEventHandler? PropertyChanged;

    public ActionStatus ActionStatus
    {
        get => _actionStatus;
        private set
        {
            if (!EqualityComparer<ActionStatus>.Default.Equals(_actionStatus, value))
            {
                _actionStatus = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ActionStatus)));
                CommandExecutionResultChanged?.Invoke(value);
            }
        }
    }

    public void Reset()
    {
        ActionStatus = ActionStatus.WaitingToRun;
    }

    public void SetRunning()
    {
        ActionStatus = ActionStatus.Running;
    }

    public void SetTimeout(int resetTimeout = Timeout.Infinite)
    {
        ActionStatus = ActionStatus.Timeout;
        DelayReset(resetTimeout);
    }

    public void SetCanceled(int resetTimeout = Timeout.Infinite)
    {
        ActionStatus = ActionStatus.Canceled;
        DelayReset(resetTimeout);
    }

    public void SetFaulted(int resetTimeout = Timeout.Infinite)
    {
        ActionStatus = ActionStatus.Faulted;
        DelayReset(resetTimeout);
    }

    public void SetRanToCompletion(int resetTimeout = Timeout.Infinite)
    {
        ActionStatus = ActionStatus.RanToCompletion;
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