namespace Caliburn.Micro.MyPatcher.Lab;

[Flags]
public enum ActionStatus
{
    Todo = 1,
    Doing = 2,
    Done = 4,
    Faulted = 8,
    Canceled = 16,
    Timeout = 32
}