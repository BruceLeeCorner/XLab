// See https://aka.ms/new-console-template for more information

using System.Reflection;

ICommand1 c = new MyClass();



var hhh = c.GetType().GetMethod("ICommand1.Execute",BindingFlags.Instance | BindingFlags.NonPublic);

Console.WriteLine(hhh.Name);
var method = typeof(ICommand1).GetMethod("Execute");

method.Invoke(c, null);

public interface ICommand1
{
    void Execute();
   
}

public interface ICommand2
{
    void Execute();
}

internal class MyClass : ICommand1, ICommand2
{
    void ICommand1.Execute()
    {
        Console.WriteLine(nameof(ICommand1));
        ((ICommand2)this).Execute();
    }
    void ICommand2.Execute()
    {
        Console.WriteLine(nameof(ICommand2));
    }

    public virtual void Execute()
    {

    }
}