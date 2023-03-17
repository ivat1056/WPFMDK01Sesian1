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
        string finalString;
        public MainWindow()
        {
            InitializeComponent();
            ClassBase.ep = new EP1();
            Password.IsEnabled = false;
            Key.IsEnabled = false;
            Number.IsEnabled = true;
            Number.Focus();
        }


        private void btn_Ex_Click(object sender, RoutedEventArgs e)
        {
            Number.Text = "";
            Password.Text = "";
            Key.Text = "";
        }

       
        private void PasswordBox_KeyDown(object sender, KeyEventArgs e)
        { 
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                Check();
            }
        }


        public void Check()
        {
            
            Emploe em1 = ClassBase.ep.Emploe.FirstOrDefault(z => z.Number == Number.Text );
            if (em1 == null)
            {
                MessageBox.Show("Такого сотрудника нет");
            }
            else
            {
                Password.IsEnabled = true;
                Password.Focus();
                Emploe em2 = ClassBase.ep.Emploe.FirstOrDefault(z => z.Password == Password.Text);
                if ((em2 == null) && (Password.Text != ""))
                {
                    MessageBox.Show("Неверный пароль");
                }
                
                else
                {
                    if ((Number.Text != "") && (Password.Text != ""))
                    {
                        if (Key.Text == finalString)
                        {
                            Emploe emploe = ClassBase.ep.Emploe.FirstOrDefault(x => x.Number == Number.Text);
                            MessageBox.Show($"Ваша роль {emploe.Rols.Name}");
                        }
                        else if((Key.Text != finalString) && (Key.Text != ""))
                        {
                            MessageBox.Show("Обновите код");
                            Key.IsEnabled = false;
                            Refac.Visibility = Visibility.Visible;
                        }
                        else
                        {
                            Auto();
                        }    
                    }
                }
            }
        }

        public void Auto()
        {
            finalString = GenRandom();
            MessageBox.Show($"Введите код {finalString}  в течение 10 секунд");

            Key.IsEnabled = true;
            Key.Focus();

            _time = TimeSpan.FromSeconds(10);  // Таймер

            _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                tbTime.Text = _time.ToString("c");
                if (_time == TimeSpan.Zero)
                {
                   
                    MessageBox.Show("Обновите код");
                    Key.IsEnabled = false;
                    Refac.Visibility = Visibility.Visible;
                   
                    _timer.Stop();
                }
                else
                {
                    _time = _time.Add(TimeSpan.FromSeconds(-1));
                  
                }

                
            }, Application.Current.Dispatcher);

            _timer.Start();        
        }
        public string GenRandom()
        {
            var chars1 = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var chars2 = "abcdefghijklmnopqrstuvwxyz"; // Генерация рандомного числа  
            var chars3 = "0123456789";
            var chars4 = "@#$%&!*№";

            var stringChars = new char[8];
            var random = new Random();

            for (int i = 0; i <= 1; i++)
            {
                stringChars[i] = chars1[random.Next(chars1.Length)];
            }

            for (int i = 2; i <= 3; i++)
            {
                stringChars[i] = chars2[random.Next(chars2.Length)];
            }
            for (int i = 4; i <= 5; i++)
            {
                stringChars[i] = chars3[random.Next(chars3.Length)];
            }
            for (int i = 6; i <= 7; i++)
            {
                stringChars[i] = chars4[random.Next(chars4.Length)];
            }


            finalString = new String(stringChars);
            Clipboard.SetText(finalString);
            return finalString;
        }
        

        private void btn_In_Click(object sender, RoutedEventArgs e)
        {
            Check();

        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Auto();
        }

        private void NumberKey(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                Check();
            }
        }

        private void KeyDown1(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                Check();
            }
        }
    }
}
