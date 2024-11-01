using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 闪屏
{
    public class MainViewModel : INotifyPropertyChanged
    {

        public MainViewModel()
        {
            Task.Run(() =>
            {
                Thread.Sleep(3000);
                OutOfSpace = true;
            });  
        }

        private bool outOfSpace;

        public bool OutOfSpace
        {
            get => outOfSpace; set
            {
                outOfSpace = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("OutOfSpace"));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
