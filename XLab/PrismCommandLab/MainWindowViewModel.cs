using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;

namespace PrismCommandLab
{
    internal class MainWindowViewModel
    {
        public AsyncDelegateCommand DownloadCommand { get; }

        public MainWindowViewModel()
        {
            DownloadCommand = new AsyncDelegateCommand(Download, DownloadCanExecute);



            ICommand relayCommand = new AsyncRelayCommand();

            //Task.Run(() =>
            //{
            //    while (true)
            //    {
            //        Thread.Sleep(1000);
            //        DownloadCommand.RaiseCanExecuteChanged();
            //    }
            //});
        }

        public async Task Download()
        {
            HttpClient client = new HttpClient();

            var content = await client.GetStringAsync("www.MMMMMMMTpornhub.com");

        }

        public bool DownloadCanExecute() => true;

        public DelegateCommand WhichUICanExecuteOnComamnd { get; } = new DelegateCommand(() =>
        {
            MessageBox.Show("Hello");
        }, () =>
        {
            MessageBox.Show(Thread.CurrentThread.ManagedThreadId.ToString());
            return true;
        });
    }
}