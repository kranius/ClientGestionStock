using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientGestionStock.Model
{
    public class Article
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public int QuantiteMinimale { get; set; } 

        public int Prix { get; set; }

        public int CategorieId { get; set; }

        public Article(int id, string description, int quantiteMinimale, int prix, int categorieId)
        {
            Id = id;
            Description = description;
            QuantiteMinimale = quantiteMinimale;
            Prix = prix;
            CategorieId = categorieId;
        }
    }
}
