using System.ComponentModel;

namespace Caliburn.Micro.MyPatcher.Lab;

public sealed class ActionStatusNotifier : INotifyPropertyChanged
{
    #region MyRegion
    private ActionStatus _actionStatus;
    private bool _canAction;

    public ActionStatusNotifier()
    {
        ActionStatus = ActionStatus.Todo;
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
        ActionStatus = ActionStatus.Todo;
    }

    public void SetDoing()
    {
        ActionStatus = ActionStatus.Doing;
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

    public void SetDone(int resetTimeout = Timeout.Infinite)
    {
        ActionStatus = ActionStatus.Done;
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
    #endregion
}