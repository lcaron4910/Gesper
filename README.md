# Gesper

Présentation du projet: le but est de réaliser une bibliothèque de classes et une application qui utilise la bibliotheque créée pour gérer le personnel.

Les outils mes en oeuvre :

* git.
* Visual studio.
* mysql.

## Conception ##
1. Il est possible d’éditer chaque élément de l’application : services, diplômes et employés.
2. Les objets créés sont manipulés en mémoire à l’aide de collections.
3. Les données sont stockées dans la base de données gesper.
4. On utilisera une bibliothèque de classes libGesper.
5. On utilisera une application console testLibGesper
6. Chaque table de la base de données Gesper donne lieu à la création d’une classe.
7. Chaque occurrence de table de la base de données Gesper est instanciée sous forme d’objet et stockée dans une collection.   

### Diagramme de Classe ###
![DiagrammeClasse.png](http://image.noelshack.com/fichiers/2019/13/6/1553959685-sans-titre.png)

## bibliothèque de classes ##

La classe Données comprend 3 listes (pour les services, employés, diplômes).
Elle comprendra les méthodes  charger  et sauvegarder  pour manipuler la base de données.

![3Classe.png](http://image.noelshack.com/fichiers/2019/13/7/1554026232-capture12.png)

Exemple de code pour Charger les Employes:
```cs
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
```
## Utilisation de la bibliothèque ##
Pour Tester la Bibliothèque LibGesper nous utilisons la référence dans une application console:

![Reference.png](http://image.noelshack.com/fichiers/2019/13/7/1554027127-capture684864.png)
