using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace CaliburnLab
{
    public class MainViewModel:PropertyChangedBase
    {

        private decimal _salary;
        private string _phone;
        private string _mail;

        public decimal Salary
        {
            get => _salary;
            // 缺省时，CallerMemberName决定通知Salary
            set => Set(ref _salary, value);
        }

        public string Mail
        {
            get => _mail;
            // 显示指定通知Mail
            set => Set(ref _mail, value, nameof(Mail));
        }

        public string Phone
        {
            get => _phone;
            // 显示指定通知MainViewModel的所有属性
            set => Set(ref _phone, value, string.Empty);
        }
    }
}
