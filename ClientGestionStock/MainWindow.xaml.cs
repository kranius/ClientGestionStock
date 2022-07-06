using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using WSGestionStock;

namespace ClientGestionStock
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<object> ArticleResult { get; set; } = new ObservableCollection<object>();
        private bool articleVisibility = false;
        public bool ArticleVisibility
        {
            get { return this.articleVisibility; }
            set
            {
                this.articleVisibility = value;
                NotifyPropertyChanged();
            }
        }

        public ObservableCollection<Categorie> CategorieResult { get; set; } = new ObservableCollection<Categorie>();
        private bool categorieVisibility = false;
        public bool CategorieVisibility
        {
            get { return this.categorieVisibility; }
            set
            {
                this.categorieVisibility = value;
                NotifyPropertyChanged();
            }
        }

        private GestionStockClient wsArticleClient = new GestionStockClient();
        private GestionCategorieClient wsCategorieClient = new GestionCategorieClient();

        private void ListAllArticles(object sender, RoutedEventArgs e)
        {
            var articles = wsArticleClient.GetArticles();
            var categories = wsCategorieClient.GetCategories();

            var result = (from a in articles
                          join c in categories
                          on a.Categorie.Id equals c.Id
                          select new { a.Id, a.Prix, a.QteMini, a.Designation, Categorie = c.Nom })
                          .ToList();

            ArticleResult.Clear();
            ArticleResult.Concat(result);
            ShowArticles();
        }

        private void ListAllCategories(object sender, RoutedEventArgs e)
        {
            var categories = wsCategorieClient.GetCategories();

            CategorieResult.Clear();
            CategorieResult.Concat(categories);
            ShowCategories();
        }

        private void ClearQueryResult(object sender, RoutedEventArgs e)
        {
            HideAll();
        }

        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            /*
            var row = ItemsControl.ContainerFromElement((DataGrid)sender,
                                      e.OriginalSource as DependencyObject) as DataGridRow;

            if (row == null) return;
            */

            if (sender is DataGridRow dataGridRow)
            {
                var dbg = MessageBox.Show(dataGridRow.Item.ToString());
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void ShowArticles()
        {
            CategorieVisibility = false;
            ArticleVisibility = true;
        }

        private void ShowCategories()
        {
            ArticleVisibility = false;
            CategorieVisibility = true;
        }

        private void HideAll()
        {
            ArticleVisibility = false;
            CategorieVisibility = false;
        }
    }
}
