using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
        ObservableCollection<Product1> _products1 = new ObservableCollection<Product1>();
        public Window1()
        {
            InitializeComponent();
            ProductsDataGrid.ItemsSource = _products;
            PostavkiDataGrid.ItemsSource = _products1;
        }
        // INotifyPropertyChanged - контракт (заставляет класс иметь возможность давать сигнал)
        /// <summary>
        /// Класс товара (одежда) для главного окна магазина
        /// </summary>
        public class Product : System.ComponentModel.INotifyPropertyChanged
        {
            private int _id;
            private string _name;
            private string _category;
            private string _size;
            private double _price;
            private int _stockQuantity;
            private string _season;

            // Идентификатор товара
            public int ID
            {
                get => _id;
                set { _id = value; OnPropertyChanged("ID"); }
            }
            // Наименование товара
            public string Name
            {
                get => _name;
                set { _name = value; OnPropertyChanged("Name"); }
            }
            // Категория товара (Футболки, Джинсы, Куртки, Обувь)
            public string Category
            {
                get => _category;
                set { _category = value; OnPropertyChanged("Category"); }
            }
            // Размер товара (XS, S, M, L, XL, XXL)
            public string Size
            {
                get => _size;
                set { _size = value; OnPropertyChanged("Size"); }
            }
            // Цена товара в рублях
            public double Price
            {
                get => _price;
                set { _price = value; OnPropertyChanged("Price"); }
            }
            // Количество товара на складе
            public int StockQuantity
            {
                get => _stockQuantity;
                set { _stockQuantity = value; OnPropertyChanged("StockQuantity"); }
            }
            // Сезон (Лето, Зима, Демисезон)
            public string Season
            {
                get => _season;
                set { _season = value; OnPropertyChanged("Season"); }
            }
            //PropertyChanged - сам сигнал, который перехватывает таблица и заменяет свои данные
            // Событие уведомления об изменении свойства (для обновления DataGrid)
            public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
            // Вызов события изменения свойства
            protected void OnPropertyChanged(string propName)
            {
                if (PropertyChanged != null)
                    PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propName));
            }
        }
        // Добавление нового товара
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Window2 addWindow = new Window2();
            if (addWindow.ShowDialog() == true)
            {
                // А этим механизмом получаем то, что передавали с того окна
                Product newProduct = addWindow.NewProduct;
                // Сначала проверяется есть ли записи в таблице. Если нет, то id - 1, а если есть то мы находим максимальное значение (Max) из списка всех id, полученных с помощью лямбда-функции(p => p.id).
                int newId = _products.Count > 0 ? _products.Max(p => p.ID) + 1 : 1;
                newProduct.ID = newId;
                _products.Add(newProduct);
            }
        }
        // Редактирование выбранного товара
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (ProductsDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Выберите товар для редактирования.");
                return;
            }
            Product selectedProduct = (Product)ProductsDataGrid.SelectedItem;
            Window2 editWindow = new Window2(selectedProduct);
            if (editWindow.ShowDialog() == true)
            {
                // Так как я редактировала напрямую объект selectedProduct,
                // а он лежит в _products, то таблица обновится сама (благодаря INotifyPropertyChanged)
                // Ничего дополнительно делать не нужно.
            }
        }
        // Обработка двойного клика по товару (открытие редактирования)
        void Double_click(object sender, RoutedEventArgs e)
        {
            if (ProductsDataGrid.SelectedItem is Product selectedProduct)
            {
                Window2 editWindow = new Window2(selectedProduct);
                editWindow.ShowDialog();
            }
        }
        // Удаление выбранного товара
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (ProductsDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Выберите товар для удаления");
                return;
            }
            // получаем объект класса product из выбранной строчки
            Product selectedProduct = (Product)ProductsDataGrid.SelectedItem;
            MessageBoxResult result = MessageBox.Show($"Вы действительно хотите удалить товар \"{selectedProduct.Name}\"?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                _products.Remove(selectedProduct);
            }
        }
        // Класс поставки товаров
        public class Product1 : System.ComponentModel.INotifyPropertyChanged
        {
            private int _id;
            private string _provider;
            private string _dateofdelivery;
            private string _name;
            private int _quantity;
            private double _costofdelivery;
            // Идентификатор поставки
            public int ID
            {
                get => _id;
                set { _id = value; OnPropertyChanged("ID"); }
            }
            // Поставщик товара
            public string Provider
            {
                get => _provider;
                set { _provider = value; OnPropertyChanged("Provider"); }
            }
            // Дата поставки (в формате ДД.ММ.ГГГГ)
            public string DateOfDelivery
            {
                get => _dateofdelivery;
                set { _dateofdelivery = value; OnPropertyChanged("DateOfDelivery"); }
            }
            // Наименование поставленного товара
            public string Name
            {
                get => _name;
                set { _name = value; OnPropertyChanged("Name"); }
            }
            // Количество поставленного товара
            public int Quantity
            {
                get => _quantity;
                set { _quantity = value; OnPropertyChanged("Quantity"); }
            }
            // Стоимость поставки (в рублях)
            public double CostOfDelivery
            {
                get => _costofdelivery;
                set { _costofdelivery = value; OnPropertyChanged("CostOfDelivery"); }
            }

            //PropertyChanged - сам сигнал, который перехватывает таблица и заменяет свои данные
            // Событие уведомления об изменении свойства
            public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
            // Вызов события изменения свойства
            protected void OnPropertyChanged(string propName)
            {
                if (PropertyChanged != null)
                    PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propName));
            }
        }
        private void Add1Button_Click(object sender, RoutedEventArgs e)
        {
            Window3 addWindow = new Window3();
            if (addWindow.ShowDialog() == true)
            {
                // А этим механизмом получаем то, что передавали с того окна
                Product1 newProduct = addWindow.NewProduct1;
                // Сначала проверяется есть ли записи в таблице. Если нет, то id - 1, а если есть то мы находим максимальное значение (Max) из списка всех id, полученных с помощью лямбда-функции(p => p.id).
                int newId = _products1.Count > 0 ? _products1.Max(p => p.ID) + 1 : 1;
                newProduct.ID = newId;
                _products1.Add(newProduct);
            }
        }
        private void Edit1Button_Click(object sender, RoutedEventArgs e)
        {
            if (PostavkiDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Выберите товар для редактирования.");
                return;
            }
            Product1 selectedProduct = (Product1)PostavkiDataGrid.SelectedItem;
            Window3 editWindow = new Window3(selectedProduct);
            if (editWindow.ShowDialog() == true)
            {
                // Так как мы редактировали напрямую объект selectedProduct,
                // а он лежит в _products, то таблица обновится сама (благодаря INotifyPropertyChanged)
                // Ничего дополнительно делать не нужно.
            }
        }
        void Double1_click(object sender, RoutedEventArgs e)
        {
            if (PostavkiDataGrid.SelectedItem is Product1 selectedProduct)
            {
                Window3 editWindow = new Window3(selectedProduct);
                editWindow.ShowDialog();
            }
        }

        private void Delete1Button_Click(object sender, RoutedEventArgs e)
        {
            if (PostavkiDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Выберите товар для удаления");
                return;
            }
            // получаем объект класса product из выбранной строчки
            Product1 selectedProduct = (Product1)PostavkiDataGrid.SelectedItem;
            MessageBoxResult result = MessageBox.Show($"Вы действительно хотите удалить товар \"{selectedProduct.Name}\"?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                _products1.Remove(selectedProduct);
            }
        }
    }
}
