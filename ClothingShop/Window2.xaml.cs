using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ClothingShop
{
    /// <summary>
    /// Логика взаимодействия для Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        public Window2()
        {
            InitializeComponent();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string entaredName = Name.Text;
            string entaredCategory = Category.Text;
            string entaredSize = Size.Text;
            string entaredPrice = Price.Text;
            string entaredStockQuantity = StockQuantity.Text;
            string entaredSeason = Season.Text;
            bool is_valid = true;
            if (string.IsNullOrWhiteSpace(entaredName))
            {
                VseDannie.Text = "Ошибка. Вы не ввели имя";
                is_valid = false;
            }
            if (string.IsNullOrWhiteSpace(entaredCategory))
            {
                VseDannie.Text = "Ошибка. Вы не ввели категорию";
                is_valid = false;
            }
            if (string.IsNullOrWhiteSpace(entaredSize))
            {
                VseDannie.Text = "Ошибка. Вы не ввели размер";
                is_valid = false;
            }
            int numberPrice;
            if (Int32.TryParse(entaredPrice, out numberPrice))
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
            if (Int32.TryParse(entaredStockQuantity, out numberStockQuantity))
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
            if (string.IsNullOrWhiteSpace(entaredSeason))
            {
                VseDannie.Text = "Ошибка. Вы не ввели сезон";
                is_valid = false;
            }
        }
    }
}
