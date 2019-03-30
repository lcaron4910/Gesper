using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace libGesper
{
    public class Diplome
    {
        List<Employe> lesEmployes = new List<Employe>();
        int id;
        string libelle;

        public Diplome(int id, string libelle)
        {
            this.id = id;
            this.libelle = libelle;
        }
        public string ToString()
        {
            return string.Format("Id : {0} , libelle : {1}", this.id, this.libelle);
        }
        public int Id
        {
            get
            {
                return this.id;
            }
        }
        public string Libelle
        {
            get
            {
                return this.libelle;
            }
        }
        public List<Employe> LesEmployes
        {
            get
            {
                return this.lesEmployes;
            }
        }
    }
}
