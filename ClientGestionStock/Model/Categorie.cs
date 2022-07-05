using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientGestionStock.Model
{
    public class Categorie
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Info { get; set; }

        public Categorie(int id, string name, string info)
        {
            Id = id;
            Name = name;
            Info = info;
        }
    }
}
