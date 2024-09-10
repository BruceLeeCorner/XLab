using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using static System.Net.Mime.MediaTypeNames;
using Action = System.Action;

namespace Caliburn.Micro.MyPatcher.Lab
{
    public class MainWindowViewModel : BindableBase
    {
        public MainWindowViewModel()
        {
            PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(LastName))
                {
                    CanSaveProperty = CanSave(_firstNameSaveParam);
                }
            };
        }

        private string _lastName;
        private string _firstNameSaveParam = string.Empty;
        private bool _canSaveProperty;
        private bool _canDeleteProperty;

        public string LastName
        {
            get => _lastName;
            set => SetProperty(ref _lastName, value);
        }

        public IObservableCollection<Person> Persons { get; } = new BindableCollection<Person>()
        {
            new Person(), new Person(), new Person(), new Person(), new Person()
        };

        #region App Task
        /// <summary>
        /// Action执行状态
        /// </summary>
        public ActionStatusNotifier SaveStatusNotifier { get; set; } = new ActionStatusNotifier();

        /// <summary>
        /// When action parameter value changed, update and notify CanSavePlus
        /// </summary>
        /// <param name="firstName"></param>
        /// <returns></returns>
        public void SaveParams(string firstName)
        {
            _firstNameSaveParam = firstName;
            CanSaveProperty = CanSave(firstName);
        }

        /// <summary>
        /// Direct CanSaveProperty
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="factor"></param>
        /// <returns></returns>
        private bool CanSave(string firstName)
        {
            return !string.IsNullOrWhiteSpace(firstName) && !string.IsNullOrWhiteSpace(LastName);
        }

        /// <summary>
        /// 综合ViewModel的因子和ActionParameter因子
        /// </summary>
        public bool CanSaveProperty
        {
            get => _canSaveProperty;
            set => SetProperty(ref _canSaveProperty, value);
        }

        public async void Save(string firstName)
        {
            if (!CanSave(_firstNameSaveParam))
            {
                MessageBox.Show("录入数据有误，保存失败！");
                return;
            }


            if (SaveStatusNotifier.ActionStatus != ActionStatus.Todo)
            {
                MessageBox.Show("上次录入的数据仍在存储中...请稍后再请求保存当前录入！");
                return;
            }

            try
            {
                SaveStatusNotifier.SetDoing();
                await Task.Delay(3000);
                SaveStatusNotifier.SetDone(1000);
            }
            catch (Exception e)
            {
                SaveStatusNotifier.SetFaulted(1000);
            }
        }

        #endregion

        #region Delete

        public bool CanDeleteProperty
        {
            get => _canDeleteProperty;
            set => SetProperty(ref _canDeleteProperty, value);
        }

        public void DeleteParams(bool g)
        {
            CanDeleteProperty = g;
        }

        public async void Delete(bool gender)
        {
            ;
        }
        #endregion
    }

    public class Person:BindableBase
    {
        private bool _gender;

        public bool Gender
        {
            get => _gender;
            set => SetProperty(ref _gender, value);
        }
    }
}