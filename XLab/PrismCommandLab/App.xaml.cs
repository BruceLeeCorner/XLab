using System.Configuration;
using System.Data;
using System.Windows;

namespace PrismCommandLab
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Application.Current.Dispatcher.UnhandledException += (sender, args) =>
            {
                MessageBox.Show(args.Exception.ToString());
                args.Handled = true;
            };
        }
    }

}
