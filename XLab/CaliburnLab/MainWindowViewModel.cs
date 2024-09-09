using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;

namespace CaliburnLab
{
    internal class MainWindowViewModel
    {
        //public bool CanSave(string text)
        //{
        //    return !string.IsNullOrWhiteSpace("text");
        //}

        public bool CanSave()
        {
            return false;
            return !string.IsNullOrWhiteSpace("text");
        }

        
        public void Save(string text)
        {
            MessageBox.Show(text);
        }
    }
}
