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
        public Product NewProduct { get; private set; }
        public Window2()
        {
            InitializeComponent();
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

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string enteredName = Name.Text;
            string enteredCategory = Category.Text;
            string enteredSize = Size.Text;
            string enteredPrice = Price.Text;
            string enteredStockQuantity = StockQuantity.Text;
            string enteredSeason = Season.Text;
            bool is_valid = true;

            // Валидация
            if (string.IsNullOrWhiteSpace(enteredName))
            {
                VseDannie.Text = "Ошибка. Вы не ввели имя";
                is_valid = false;
            }
            if (string.IsNullOrWhiteSpace(enteredCategory))
            {
                VseDannie.Text = "Ошибка. Вы не выбрали категорию";
                is_valid = false;
            }
            if (string.IsNullOrWhiteSpace(enteredSize))
            {
                VseDannie.Text = "Ошибка. Вы не выбрали размер";
                is_valid = false;
            }
            int numberPrice;
            if (Int32.TryParse(enteredPrice, out numberPrice))
            {
                if (numberPrice < 100 || numberPrice > 100000)
                {
                    VseDannie.Text = "Ошибка. Введите цену от 100 до 100000";
                    is_valid = false;
                }
            }
            else
            {
                VseDannie.Text = "Ошибка. Введите числовое значение цены";
                is_valid = false;
            }
            int numberStockQuantity;
            if (Int32.TryParse(enteredStockQuantity, out numberStockQuantity))
            {
                if (numberStockQuantity < 100 || numberStockQuantity > 10000)
                {
                    VseDannie.Text = "Ошибка. Введите количество товара на складе от 100 до 10000";
                    is_valid = false;
                }
            }
            else
            {
                VseDannie.Text = "Ошибка. Введите числовое значение количества товара на складе";
                is_valid = false;
            }
            if (string.IsNullOrWhiteSpace(enteredSeason))
            {
                VseDannie.Text = "Ошибка. Вы не выбрали сезон";
                is_valid = false;
            }
            if (is_valid == true)
            {
                NewProduct = new Window1.Product
                {
                    Name = enteredName,
                    Category = enteredCategory,
                    Size = enteredSize,
                    Price = numberPrice,
                    StockQuantity = numberStockQuantity,
                    Season = enteredSeason,
                };

                DialogResult = true;
                this.Close();
            }     
        }
    }
}
