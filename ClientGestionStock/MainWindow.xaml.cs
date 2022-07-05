using ClientGestionStock.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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

        // localhostService.WebService ws = new localhostService.WebService();

        private void PopulateArticles()
        {
            var categories = new List<Categorie>();
            var articles = new List<Article>();

            Categorie meubles = new Categorie(1, "meubles", "pour l'interieur ou l'exterieur");
            Categorie tech = new Categorie(2, "tech", "appareils high-tech");
            categories.Add(meubles);
            categories.Add(tech);

            articles.Add(new Article(1, "Chaise", 2, 15, 1));
            articles.Add(new Article(2, "Table", 1, 80, 1));
            articles.Add(new Article(3, "Telephone", 1, 300, 2));
            articles.Add(new Article(4, "Casque sans fil", 1, 120, 2));

            var Join = (from a in articles
                        join c in categories
                        on a.CategorieId equals c.Id
                        select new { a.Id, a.Prix, a.QuantiteMinimale, a.Description, c.Name }).ToList();
                        //select new {Article=a, Categorie=c }).ToList();

            ArticleResult.Clear();
            foreach (var article in Join)
            {
                ArticleResult.Add(article);
            }
            ShowArticles();
        }

        private void PopulateCategories()
        {
            var categories = new List<Categorie>();
            Categorie meubles = new Categorie(1, "meubles", "pour l'interieur ou l'exterieur");
            Categorie tech = new Categorie(2, "tech", "appareils high-tech");
            categories.Add(meubles);
            categories.Add(tech);

            CategorieResult.Clear();
            foreach (var categorie in categories)
            {
                CategorieResult.Add(categorie);
            }
            ShowCategories();
        }

        private void ListAllArticles(object sender, RoutedEventArgs e)
        {
            PopulateArticles();
            //ReplaceQueryResultWith(ws.ListerArticles());
        }

        private void ListAllCategories(object sender, RoutedEventArgs e)
        {
            PopulateCategories();
            //ReplaceQueryResultWith(ws.ListerCategories());
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
