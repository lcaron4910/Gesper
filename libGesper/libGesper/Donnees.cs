using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;

namespace libGesper
{
    public class Donnees
    {
        private MySqlConnection cnx;
        List<Diplome> LesDiplomes = new List<Diplome>();
        List<Employe> LesEmployes = new List<Employe>();
        List<Service> LesServices = new List<Service>();

        public Donnees()
        {
            this.cnx = ConnectionSQL.GetConnection();
            LesDiplomes = new List<Diplome>();
            LesEmployes = new List<Employe>();
            LesServices = new List<Service>();
        }

        public void AfficherDiplomes()
        {
            for (int i = 0; i < LesDiplomes.Count; i = i + 1)
            {
                Console.WriteLine(LesDiplomes[i].ToString());
            }
        }
        public void AfficherEmployes()
        {
            for (int i = 0; i < LesEmployes.Count; i = i + 1)
            {
                Console.WriteLine(LesEmployes[i].ToString());
            }
        }
        public void AfficherServices()
        {
            for (int i = 0; i < LesServices.Count; i = i + 1)
            {
                Console.WriteLine(LesServices[i].ToString());
            }
        }
        public void chargerDiplomes()
        {
            MySqlCommand cmSql = new MySqlCommand();
            cmSql.Connection = cnx;
            cmSql.CommandText = "select * from diplome";
            cmSql.CommandType = CommandType.Text;
            this.cnx.Open();
            MySqlDataReader reader = cmSql.ExecuteReader();
            while (reader.Read())
            {
                Diplome dip = new Diplome(Convert.ToInt32(reader[0]), Convert.ToString(reader[1]));
                LesDiplomes.Add(dip);
            }
            this.cnx.Close();
        }
        public void chargerEmployes()
        {
            MySqlCommand cmSql = new MySqlCommand();
            cmSql.Connection = cnx;
            cmSql.CommandText = "select * from employe";
            cmSql.CommandType = CommandType.Text;
            this.cnx.Open();
            MySqlDataReader reader = cmSql.ExecuteReader();
            while (reader.Read())
            {
                Service ser1=null ;
                int i;
                for ( i = 0; i < LesServices.Count; i++)
                {
                    if (LesServices[i].Id == Convert.ToInt32(reader[6]))
                    {
                        ser1 = LesServices[i];
                    }
                }
                Employe emp = new Employe(Convert.ToInt32(reader[0]), Convert.ToString(reader[1]), Convert.ToString(reader[2]), Convert.ToString(reader[3]), Convert.ToByte(reader[4]), Convert.ToDecimal(reader[5]), ser1);
                LesEmployes.Add(emp);
            }
            this.cnx.Close();
        }
        public void chargerServices()
        {
            MySqlCommand cmSql = new MySqlCommand();
            cmSql.Connection = cnx;
            cmSql.CommandText = "select * from service";
            cmSql.CommandType = CommandType.Text;
            this.cnx.Open();
            MySqlDataReader reader = cmSql.ExecuteReader();
            while (reader.Read())
            {
                if ((string)reader[2] == "P")
                {
                    Service ser = new Service(Convert.ToInt32(reader[0]), Convert.ToString(reader[1]), Convert.ToChar(reader[2]), Convert.ToString(reader[3]), Convert.ToInt32(reader[4]));
                    LesServices.Add(ser);
                }
                if ((string)reader[2] == "A")
                {
                    Service ser = new Service(Convert.ToInt32(reader[0]), Convert.ToString(reader[1]), Convert.ToChar(reader[2]), Convert.ToDecimal(reader[5]));
                    LesServices.Add(ser);
                }
            }
            this.cnx.Close();
        }
        public void ToutCharger()
        {
            chargerServices();
            chargerEmployes();
            ChargerlesEmployesDesServices();
            chargerDiplomes();
            ChargerlesDiplomesDesEmployes();
            ChargerlesEmployesTitulaireDesDiplomes(); 
        }
        public void ChargerlesDiplomesDesEmployes()
        {
            MySqlCommand cmdSql = new MySqlCommand();
            cmdSql.Connection = cnx;
            this.cnx.Open();
            MySqlDataReader reader;
            Diplome unDiplome;
            int i;
            for (int y = 0 ; y < LesEmployes.Count ;y++)
            {

                cmdSql.CommandText = "select * from posseder where pos_employe =" + LesEmployes[y].Id + "; ";
                cmdSql.CommandType = CommandType.Text;
                reader = cmdSql.ExecuteReader();
                while (reader.Read())
                {
                    i = 0;
                    unDiplome = null;
                    while (i < LesDiplomes.Count && LesDiplomes[i].Id != Convert.ToInt32(reader[0]))
                    {
                        i++;
                    }
                    if (i < LesDiplomes.Count && LesDiplomes[i].Id == Convert.ToInt32(reader[0]))
                    {
                        unDiplome = LesDiplomes[i];
                        LesEmployes[y].LesDiplomes.Add(unDiplome);
                    }
                }
                reader.Close();
                this.cnx.Close();
            }
        }
        public void ChargerlesEmployesDesServices()
        {
            MySqlCommand cmdSql = new MySqlCommand();
            cmdSql.Connection = cnx;
            this.cnx.Open();
            MySqlDataReader reader;
            Employe unEmployer;
            int i;
            for (int y = 0; y < LesServices.Count; y++)
            {

                cmdSql.CommandText = "select * from employe where emp_service =" + LesServices[y].Id + "; ";
                cmdSql.CommandType = CommandType.Text;
                reader = cmdSql.ExecuteReader();
                while (reader.Read())
                {
                    i = 0;
                    unEmployer = null;
                    while (i < LesEmployes.Count && LesEmployes[i].Id != Convert.ToInt32(reader[0]))
                    {
                        i++;
                    }
                    if (i < LesEmployes.Count && LesEmployes[i].Id == Convert.ToInt32(reader[0]))
                    {
                        unEmployer = LesEmployes[i];
                    }
                    LesServices[y].LesEmployeDuService.Add(unEmployer);
                }
                reader.Close();
                this.cnx.Close();
            }
        }
        public void ChargerlesEmployesTitulaireDesDiplomes()
        {
            MySqlCommand cmdSql = new MySqlCommand();
            cmdSql.Connection = cnx;
            MySqlDataReader reader;
            Employe unEmploye;
            int i;
            this.cnx.Open();
            for (int y = 0; y < LesDiplomes.Count; y++)
            {
                cmdSql.CommandText = "select * from posseder where pos_diplome = " + LesDiplomes[y].Id + ";";
                cmdSql.CommandType = CommandType.Text;
                reader = cmdSql.ExecuteReader();
                while (reader.Read())
                {
                    i = 0;
                    unEmploye = null;
                    while (i < LesEmployes.Count && LesEmployes[i].Id != Convert.ToInt32(reader[1]))
                    {
                        i++;
                    }
                    if (i < LesEmployes.Count && LesEmployes[i].Id == Convert.ToInt32(reader[1]))
                    {
                        unEmploye = LesEmployes[i];
                        LesDiplomes[y].LesEmployes.Add(unEmploye);
                    }
                }
                reader.Close();
            }
            this.cnx.Close();
            
        }
        public void ToutSauvegarder()
        {
            this.cnx.Open();
            MySqlCommand cmdSql = new MySqlCommand();
            cmdSql.Connection = cnx;
            cmdSql.CommandText = "RemiseAZero";
            cmdSql.CommandType = CommandType.StoredProcedure;
            cmdSql.Prepare();
            cmdSql.ExecuteNonQuery();
            MySqlParameter serId = new MySqlParameter("serId", MySqlDbType.Int32);
            cmdSql.Parameters.Add(serId);
            MySqlParameter serDesignation = new MySqlParameter("serDesignation", MySqlDbType.VarChar);
            cmdSql.Parameters.Add(serDesignation);
            MySqlParameter serType = new MySqlParameter("serType", MySqlDbType.VarChar);
            cmdSql.Parameters.Add(serType);
            MySqlParameter serProduit = new MySqlParameter("serProduit", MySqlDbType.VarChar);
            cmdSql.Parameters.Add(serProduit);
            MySqlParameter serCapacite = new MySqlParameter("serCapacite", MySqlDbType.Int32);
            cmdSql.Parameters.Add(serCapacite);
            MySqlParameter serBudget = new MySqlParameter("serBudget", MySqlDbType.Decimal);
            cmdSql.Parameters.Add(serBudget);

            for (int i = 0; i < LesServices.Count; i++)
            {
                serId.Value = LesServices[i].Id;
                serDesignation.Value = LesServices[i].Designation;
                serType.Value = LesServices[i].Type;
                serProduit.Value = LesServices[i].Produit;
                serCapacite.Value = LesServices[i].Capacite;
                serBudget.Value = LesServices[i].Budget;

                if((char)LesServices[i].Type == 'P')
                {
                    cmdSql.CommandText = "insert into service(ser_designation,ser_type,ser_produit,ser_capacite,ser_budget) values (@serDesignation,@serType,@serProduit,@serCapacite,0)";
                    cmdSql.CommandType = CommandType.Text;
                    cmdSql.Prepare();
                    cmdSql.ExecuteNonQuery();
                }
                if ((char)LesServices[i].Type == 'A')
                {
                    cmdSql.CommandText = "insert into service(ser_designation,ser_type,ser_produit,ser_capacite,ser_budget) values (@serDesignation,@serType,0,0,@serBudget)";
                    cmdSql.CommandType = CommandType.Text;
                    cmdSql.Prepare();
                    cmdSql.ExecuteNonQuery();
                }

            }
            MySqlParameter dipId = new MySqlParameter("dipId", MySqlDbType.Int32);
            cmdSql.Parameters.Add(dipId);
            MySqlParameter dipLibelle = new MySqlParameter("dipLibelle", MySqlDbType.VarChar);
            cmdSql.Parameters.Add(dipLibelle);

            for (int i = 0; i < LesDiplomes.Count; i++)
            {
                dipId.Value = LesDiplomes[i].Id;
                dipLibelle.Value = LesDiplomes[i].Libelle;

                cmdSql.CommandText = "insert into diplome(dip_id,dip_libelle) values (@dipId,@dipLibelle)";
                cmdSql.CommandType = CommandType.Text;
                cmdSql.Prepare();
                cmdSql.ExecuteNonQuery();
            }



            MySqlParameter empId = new MySqlParameter("empId", MySqlDbType.Int32);
            cmdSql.Parameters.Add(empId);
            MySqlParameter empNom = new MySqlParameter("empNom", MySqlDbType.VarChar);
            cmdSql.Parameters.Add(empNom);
            MySqlParameter empPrenom = new MySqlParameter("empPrenom", MySqlDbType.VarChar);
            cmdSql.Parameters.Add(empPrenom);
            MySqlParameter empSexe = new MySqlParameter("empSexe", MySqlDbType.VarChar);
            cmdSql.Parameters.Add(empSexe);
            MySqlParameter empCadre = new MySqlParameter("empCadre", MySqlDbType.Bit);
            cmdSql.Parameters.Add(empCadre);
            MySqlParameter empSalaire = new MySqlParameter("empSalaire", MySqlDbType.Decimal);
            cmdSql.Parameters.Add(empSalaire);
            MySqlParameter empService = new MySqlParameter("empService", MySqlDbType.Int32);
            cmdSql.Parameters.Add(empService);

            for (int i = 0; i < LesEmployes.Count; i++)
            {
                empId.Value = LesEmployes[i].Id;
                empNom.Value = LesEmployes[i].Nom;
                empPrenom.Value = LesEmployes[i].Prenom;
                empSexe.Value = LesEmployes[i].Sexe;
                empCadre.Value = LesEmployes[i].Cadre;
                empSalaire.Value = LesEmployes[i].Salaire;
                empService.Value = LesEmployes[i].Service.Id;
                cmdSql.CommandText = "insert into employe(emp_id,emp_nom,emp_prenom,emp_sexe,emp_cadre,emp_salaire,emp_service) values (@empId,@empNom,@empPrenom,@empSexe,@empCadre,@empSalaire,@empService)";
                cmdSql.CommandType = CommandType.Text;
                cmdSql.Prepare();
                cmdSql.ExecuteNonQuery();
            }




            cnx.Close();




        }


    }

}
