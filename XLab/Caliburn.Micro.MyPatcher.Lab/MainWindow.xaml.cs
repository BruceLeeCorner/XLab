
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Caliburn.Micro.MyPatcher.Lab
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainWindowViewModel();
        }
    }

    public class ActionStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var value2 = (ActionStatus)value;
            return value2 == ActionStatus.Todo ? "Save" : value2.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ActionStatus2BrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var value2 = (ActionStatus)value;
            switch (value2)
            {
                case ActionStatus.Todo:
                    return Brushes.Azure;
                case ActionStatus.Doing:
                    return Brushes.CornflowerBlue;
                case ActionStatus.Done:
                    return Brushes.LimeGreen;
                case ActionStatus.Faulted:
                case ActionStatus.Timeout:
                    return Brushes.Red;
                case ActionStatus.Canceled:
                    return Brushes.Orange;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}