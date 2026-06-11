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

namespace ClothingShop
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        // Обработчик кнопки "Вход" - проверка логина и пароля
        private void ShowPasswor_Click(object sender, RoutedEventArgs e)
        {
            {
                string enteredLogin = LoginInpup.Text;
                string enteredPassword = PasswordInput.Password;
                // Проверка авторизации (жёстко заданные логин/пароль)
                if (enteredLogin == "seller" && enteredPassword == "fashion2026")
                {
                    // Успешный вход - открываем главное окно магазина
                    var window1 = new Window1();
                    window1.Show();
                    this.Close();
                }
                else
                {
                    // Ошибка авторизации - очищаем поля и показываем сообщение
                    LoginInpup.Text = string.Empty;
                    PasswordInput.Password = string.Empty;
                    MessageBox.Show("Ошибка авторизации. Проверьте логин и пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }


            }
        }
        // Обработчик кнопки "Выход" - закрытие приложения
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}