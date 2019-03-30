using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace libGesper
{
   public  class Employe
    {
        List<Diplome> lesDiplomes = new List<Diplome>();
        byte cadre;
        int id;
        string nom;
        string prenom;
        decimal salaire;
        string sexe;
        Service leService;

        public Employe(int id, string nom, string prenom, string sexe, byte cadre, decimal salaire,Service leService)
        {
            this.id = id;
            this.nom = nom;
            this.cadre = cadre;
            this.prenom = prenom;
            this.salaire = salaire;
            this.sexe = sexe;
            this.leService = leService;
        }
        public string ToString()
        {
            return string.Format("Id : {0} , nom : {1},prenom : {2} ,sexe : {3}, cadre  : {4}, salaire : {5}", this.id, this.nom, this.prenom, this.sexe, this.cadre, this.salaire);
        }
        public int Id
        {
            get
            {
                return this.id;
            }
        }
        public string Nom
        {
            get
            {
                return this.nom;
            }
        }
        public string Prenom
        {
            get
            {
                return this.prenom;
            }
        }
        public string Sexe
        {
            get
            {
                return this.sexe;
            }
        }
        public byte Cadre
        {
            get
            {
                return this.cadre;
            }
        }
        public decimal Salaire
        {
            get
            {
                return this.salaire;
            }
        }
        public Service Service
        {
            get
            {
                return this.leService;
            }
        }
        public List<Diplome> LesDiplomes
        {
            get
            {
                return this.lesDiplomes;
            }
        }
    }
}
