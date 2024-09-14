// See https://aka.ms/new-console-template for more information

using System.Reflection;


//Nullable<int> a = 1;

//Console.WriteLine(a.GetType().Name);

//;

//var g = new G<G<string>>();

//var gg = new G<string>();
//Console.WriteLine(g.GetType().FullName);
//Console.WriteLine(gg.GetType().FullName);


DisplayTypeInfo(typeof(Nullable<int>));
 static void DisplayTypeInfo(Type t)
{
    Console.WriteLine("\r\n{0}", t);
    Console.WriteLine("\tIs this a generic type definition? {0}",
        t.IsGenericTypeDefinition);
    Console.WriteLine("\tIs it a generic type? {0}",
        t.IsGenericType);
    Type[] typeArguments = t.GetGenericArguments();
    Console.WriteLine("\tList type arguments ({0}):", typeArguments.Length);
    foreach (Type tParam in typeArguments)
    {
        Console.WriteLine("\t\t{0}", tParam);
    }
}


;





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

class G<T>
{

}