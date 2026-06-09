using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        ObservableCollection<Product> _products = new ObservableCollection<Product>();
        public Window1()
        {
            InitializeComponent();
            ProductsDataGrid.ItemsSource = _products;
        }
        public class Product
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public string Category { get; set; }
            public string Size { get; set; }
            public int Price { get; set; }
            public int StockQuantity { get; set; }
            public string Season { get; set; }
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {   
            var window2 = new Window2();
            if (window2.ShowDialog() == true) 
            {
                var newProduct = window2.NewProduct;
                // Сначала проверяется есть ли записи в таблице. Если нет, то id - 1, а если есть то мы находим максимальное значение (Max) из списка всех id, полученных с помощью лямбда-функции(p => p.id).
                int newId = _products.Count > 0 ? _products.Max(p => p.ID) + 1 : 1;
                newProduct.ID = newId;
                _products.Add(newProduct);
            }
        }


    }
}
