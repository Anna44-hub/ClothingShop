using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static ClothingShop.Window1;

namespace ClothingShop
{
    /// <summary>
    /// Логика взаимодействия для Window2.xaml
    /// </summary>
    public partial class Window3 : Window
    {
        Product1 _productToEdit;
        bool isEditMode;

        // Конструктор для добавления нового товара
        public Window3()
        {
            InitializeComponent();
            isEditMode = false;
        }

        // Конструктор для редактирования (передаём товар, который нужно изменить)
        public Window3(Product1 existingProduct)
        {
            InitializeComponent();
            _productToEdit = existingProduct;
            isEditMode = true;
            LoadProductData(existingProduct); // заполняем поля данными
        }


        private void LoadProductData(Product1 product)
        {
            Provider.Text = product.Provider;
            DateOfDelivery.Text = product.DateOfDelivery;
            Name.Text = product.Name;
            Quantity.Text = product.Quantity.ToString();
            CostOfDelivery.Text = product.CostOfDelivery.ToString();
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Validate())
                return;

            if (isEditMode)
                SaveEdit();
            else
                SaveAdd();

            DialogResult = true;
            Close();
        }

        // Валидация
        bool Validate()
        {
            if (string.IsNullOrWhiteSpace(Provider.Text))
            {
                VseDannie.Text = "Ошибка. Вы не ввели поставщика";
                return false;
            }
            if (string.IsNullOrWhiteSpace(DateOfDelivery.Text))
            {
                VseDannie.Text = "Ошибка. Вы не ввели дату";
                return false;
            }
            DateTime parsedDate;
            bool isDateValid = DateTime.TryParseExact(
                DateOfDelivery.Text,
                "dd.MM.yyyy",
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None,
                out parsedDate);
            if (!isDateValid)
            {
                VseDannie.Text = "Ошибка. Введите дату в формате ДД.ММ.ГГГГ (напр. 22.05.2007)";
                return false;
            }
            if (string.IsNullOrWhiteSpace(Name.Text))
            {
                VseDannie.Text = "Ошибка. Вы не ввели название товара";
                return false;
            }
            bool hasLetter = false;
            foreach (char c in Name.Text)
            {
                if (char.IsLetter(c))
                {
                    hasLetter = true;
                    break;
                }
            }
            if (!hasLetter)
            {
                VseDannie.Text = "Ошибка. Товар должен содержать хотя бы одну букву";
                return false;
            }

            double cost;
            if (!double.TryParse(CostOfDelivery.Text, out cost))
            {
                VseDannie.Text = "Ошибка. Введите числовое значение стоимости";
                return false;
            }
            if (cost < 100 || cost > 1000000)
            {
                VseDannie.Text = "Ошибка. Введите цену от 100 до 1000000";
                return false;
            }

            int stock;
            if (!int.TryParse(Quantity.Text, out stock))
            {
                VseDannie.Text = "Ошибка. Введите числовое значение количества";
                return false;
            }
            if (stock < 0 || stock > 10000)
            {
                VseDannie.Text = "Ошибка. Введите количество от 0 до 10000";
                return false;
            }
            return true;
        }

        void SaveAdd()
        {
            // Создаём новый объект Product1
            NewProduct1 = new Product1
            {
                Provider = Provider.Text,
                DateOfDelivery = DateOfDelivery.Text,
                Name = Name.Text,
                Quantity = int.Parse(Quantity.Text),
                CostOfDelivery = double.Parse(CostOfDelivery.Text),
            };
        }

        private void SaveEdit()
        {
            // Меняем свойства у объекта, который нам передали в конструкторе
            _productToEdit.Provider = Provider.Text;
            _productToEdit.DateOfDelivery = DateOfDelivery.Text;
            _productToEdit.Name = Name.Text;
            _productToEdit.Quantity = int.Parse(Quantity.Text);
            _productToEdit.CostOfDelivery = double.Parse(CostOfDelivery.Text);
        }
        //Этот механизм используется для возврата созданного товара из диалогового окна Window2 в главное окно Window1.
        public Product1 NewProduct1 { get; private set; }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }
    }
}
