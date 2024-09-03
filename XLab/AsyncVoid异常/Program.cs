// See https://aka.ms/new-console-template for more information

try
{
    Thread.Sleep(1);
    Thread.Yield();
    await T1();
    Thread.Yield();
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
    Console.WriteLine("线程的ID是：" + Thread.CurrentThread.ManagedThreadId.ToString());
}

static async Task T1()
{
    await Task.Factory.StartNew(() =>
    {
        try
        {
            File.ReadAllBytes("C:dd");
            File.ReadAllBytes("C:dd");
            File.ReadAllBytes("C:dd");
            File.ReadAllBytes("C:dd");
            File.ReadAllBytes("C:dd");
            File.ReadAllBytes("C:dd");
        }
        catch (Exception e)
        {
            Thread.Sleep(10);

            throw new AbandonedMutexException("异步线程的ID是：" +
                                              Thread.CurrentThread.ManagedThreadId.ToString());
        }
    }, TaskCreationOptions.PreferFairness);
}