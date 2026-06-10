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
    public partial class Window2 : Window
    {
        Product _productToEdit;
        bool isEditMode;

        // Конструктор для добавления нового товара
        public Window2()
        {
            InitializeComponent();
            FillComboBoxes();
            isEditMode = false;
        }

        // Конструктор для редактирования (передаём товар, который нужно изменить)
        public Window2(Product existingProduct)
        {
            InitializeComponent();
            FillComboBoxes();
            _productToEdit = existingProduct;
            isEditMode = true;
            LoadProductData(existingProduct); // заполняем поля данными
        }

        private void FillComboBoxes()
        {
            Category.Items.Add("Футболки");
            Category.Items.Add("Джинсы");
            Category.Items.Add("Куртки");
            Category.Items.Add("Обувь");
            Size.Items.Add("XS");
            Size.Items.Add("S");
            Size.Items.Add("M");
            Size.Items.Add("L");
            Size.Items.Add("XL");
            Size.Items.Add("XXL");
            Season.Items.Add("Лето");
            Season.Items.Add("Зима");
            Season.Items.Add("Демисезон");
        }
        private void LoadProductData(Product product)
        {
            Name.Text = product.Name;
            Category.Text = product.Category;
            Size.Text = product.Size;
            Price.Text = product.Price.ToString();
            StockQuantity.Text = product.StockQuantity.ToString();
            Season.Text = product.Season;
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
            if (string.IsNullOrWhiteSpace(Name.Text))
            {
                VseDannie.Text = "Ошибка. Вы не ввели наименование";
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
                VseDannie.Text = "Ошибка. Наименование должно содержать хотя бы одну букву";
                return false;
            }
            if (string.IsNullOrWhiteSpace(Category.Text))
            {
                VseDannie.Text = "Ошибка. Вы не выбрали категорию";
                return false;
            }
            if (string.IsNullOrWhiteSpace(Size.Text))
            {
                VseDannie.Text = "Ошибка. Вы не выбрали размер";
                return false;
            }

            double price;
            if (!double.TryParse(Price.Text, out price))
            {
                VseDannie.Text = "Ошибка. Введите числовое значение цены";
                return false;
            }
            if (price < 100 || price > 100000)
            {
                VseDannie.Text = "Ошибка. Введите цену от 100 до 100000";
                return false;
            }

            int stock;
            if (!int.TryParse(StockQuantity.Text, out stock))
            {
                VseDannie.Text = "Ошибка. Введите числовое значение количества на складе";
                return false;
            }
            if (stock < 0 || stock > 10000)
            {
                VseDannie.Text = "Ошибка. Введите количество от 0 до 10000";
                return false;
            }

            if (string.IsNullOrWhiteSpace(Season.Text))
            {
                VseDannie.Text = "Ошибка. Вы не выбрали сезон";
                return false;
            }
            return true;
        }

        void SaveAdd()
        {
            // Создаём новый объект Product
            NewProduct = new Product
            {
                Name = Name.Text,
                Category = Category.Text,
                Size = Size.Text,
                Price = double.Parse(Price.Text),
                StockQuantity = int.Parse(StockQuantity.Text),
                Season = Season.Text
            };
        }

        private void SaveEdit()
        {
            // Меняем свойства у объекта, который нам передали в конструкторе
            _productToEdit.Name = Name.Text;
            _productToEdit.Category = Category.Text;
            _productToEdit.Size = Size.Text;
            _productToEdit.Price = double.Parse(Price.Text);
            _productToEdit.StockQuantity = int.Parse(StockQuantity.Text);
            _productToEdit.Season = Season.Text;
        }
        //Этот механизм используется для возврата созданного товара из диалогового окна Window2 в главное окно Window1.
        public Product NewProduct { get; private set; }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }

    }
}
