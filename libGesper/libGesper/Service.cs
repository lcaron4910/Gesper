using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace libGesper
{
     public class Service
    {
        List<Employe> lesEmployeDuService = new List<Employe>();
        decimal budget;
        int capacite;
        int dernierId;
        string designation;
        int id;
        string produit;
        char type;
        

        public Service(int id, string designation, char type, string produit, int capacite, decimal budget)
        {
            this.id = id;
            this.designation = designation;
            this.type = type;
            this.produit = produit;
            this.capacite = capacite;
            this.budget = budget;
        }
        public Service(int id, string designation, char type,decimal budget)
        {
            this.id = id;
            this.designation = designation;
            this.type = type;
            this.budget = budget;
        }
        public Service(int id, string designation, char type, string produit, int capacite)
        {
            this.id = id;
            this.designation = designation;
            this.type = type;
            this.produit = produit;
            this.capacite = capacite;
        }
        public string ToString()
        {
            return string.Format("Id : {0} , designation : {1},type : {2} ,produit : {3}, capacite  : {4}, budget : {5} dernier id :{6}", this.id, this.designation, this.type, this.produit, this.capacite, this.budget, this.dernierId);
        }
        public int Id
        {
            get
            {
                return this.id;
            }
        }
        public string Designation
        {
            get
            {
                return this.designation;
            }
        }
        public char Type
        {
            get
            {
                return this.type;
            }
        }
        public string Produit
        {
            get
            {
                return this.produit;
            }
        }
        public int Capacite
        {
            get
            {
                return this.capacite;
            }
        }
        public decimal Budget
        {
            get
            {
                return this.budget;
            }
        }
        public List<Employe> LesEmployeDuService
        {
            get
            {
                return this.lesEmployeDuService;
            }
        }

        public Service()
        {
        }
    }
}
