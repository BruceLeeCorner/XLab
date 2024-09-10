namespace PrismCommandLab;

[Flags]
public enum ActionStatus
{
    WaitingToRun = 1,
    Running = 2,
    RanToCompletion = 4,
    Faulted = 8,
    Canceled = 16,
    Timeout = 32
}