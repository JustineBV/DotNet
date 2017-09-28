using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WSConvertisseur.Models
{
    public class Devise
    {
        public int Id { get; set; }

        public string NomDevise { get; set; }

        public double Taux { get; set; }


        public Devise()
        {

        }

        public Devise(int Id, string Nom, double Taux)
        {
            this.Id = Id;
            this.NomDevise = Nom;
            this.Taux = Taux;
        }





    }
}