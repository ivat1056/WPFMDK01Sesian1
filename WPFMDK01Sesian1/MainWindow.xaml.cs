using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using WPFMDK01Sesian1.Class;

namespace WPFMDK01Sesian1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer _timer;
        TimeSpan _time;
        public MainWindow()
        {
            InitializeComponent();
            ClassBase.ep = new EP1();
        }

        private void btn_Ex_Click(object sender, RoutedEventArgs e)
        {
            Number.Text = "";
            Password.Text = "";
            Key.Text = "";
        }

        
        private void PasswordBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
           
        }
       
        private void PasswordBox_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == System.Windows.Input.Key.Enter)
            {
                Emploe em = ClassBase.ep.Emploe.FirstOrDefault(z => z.Number == Number.Text && z.Password == Password.Text);
                if (em == null)
                {
                    MessageBox.Show("Такого сотрудника нет");
                }
                else
                {
                    var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789@#$%&!*№";
                    var stringChars = new char[8];
                    var random = new Random();

                    for (int i = 0; i < stringChars.Length; i++)
                    {
                        stringChars[i] = chars[random.Next(chars.Length)];
                    }

                    var finalString = new String(stringChars);

                    MessageBox.Show($"Введите код {finalString}  в течение 10 секунд" ) ;

                    KeyV.Visibility = Visibility.Visible ;

                    _time = TimeSpan.FromSeconds(10);

                    _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
                    {
                        tbTime.Text = _time.ToString("c");
                        if (_time == TimeSpan.Zero)
                        {
                            _timer.Stop();
                            KeyV.Visibility = Visibility.Hidden;
                            MessageBox.Show("Обновите код");
                        }
                        else
                        {
                            _time = _time.Add(TimeSpan.FromSeconds(-1));
                        }
                    }, Application.Current.Dispatcher);

                    _timer.Start();
                   

                }
            }

        }

        private void btn_In_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
