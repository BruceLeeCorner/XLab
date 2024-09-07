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
    public class MainWindowViewModel
    {
        public ICommand SaveCmd { get; private set; }

        private bool SaveCanExecute() => true;

        private void SaveExecute()
        {
            Save();
        }

        private void Save()
        {
            
        }


        public AsyncDelegateCommand DownloadCommand { get; }


        public MainWindowViewModel()
        {

            AssignCmds();

            void AssignCmds()
            {
                SaveCmd = new RelayCommand(SaveExecute, SaveCanExecute);
            }


            DownloadCommand = new AsyncDelegateCommand(Download, DownloadCanExecute);

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