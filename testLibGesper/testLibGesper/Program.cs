using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using libGesper;


namespace testLibGesper
{
    class Program
    {
        public static void Main(string[] args)
        {
           
            Donnees lesDonnees = new Donnees();

// il faut commencer par charger les services avant de charger les employés !!!!!!!

            lesDonnees.chargerServices();
            lesDonnees.chargerEmployes();
            lesDonnees.AfficherEmployes();
            lesDonnees.chargerDiplomes();
            lesDonnees.AfficherDiplomes();
         //   lesDonnees.chargerServices();
            lesDonnees.AfficherServices();
            lesDonnees.ToutSauvegarder();
            Console.ReadLine();
        }
    }
}
