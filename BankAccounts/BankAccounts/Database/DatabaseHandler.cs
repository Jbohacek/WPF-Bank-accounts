using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using SQLite;

namespace System
{
    public class DatabaseHandler
    {
        /*
         Aby tento kod fungoval je potřeba, aby bylo nainstalován nuget packpage
            "sqlite-net-pcl"
            https://www.nuget.org/packages/sqlite-net-pcl/1.8.116?_src=template
            
         */


        public string DatabaseName { get; private set; }
        public string Cesta { get; private set; }

       /// <summary>
       /// Pokud false tak nebude vypisovat do debugu
       /// </summary>
        public bool CreateDebugMessages = true;

        /// <summary>
        /// Vloží objekt do tabulky
        /// </summary>
        /// <typeparam name="T">Typ klasy, který chceme uložit</typeparam>
        /// <param name="Hodnota">Co chceme do ní vložit</param>
        public void InsertValue<T>(T Hodnota)
        {
            CreateTable<T>();
            using (SQLiteConnection connection = new SQLiteConnection(Cesta))
            {
                connection.Insert(Hodnota);
                Send<T>("Data has been inserted");
            }
        }
        /// <summary>
        /// Vloží list objektů do tabulky
        /// </summary>
        /// <typeparam name="T">Typ klasy, který chceme uložit</typeparam>
        /// <param name="Hodnota">Co chceme do ní vložit</param>
        public void InsertValue<T>(List<T> Hodnota)
        {
            CreateTable<T>();
            using (SQLiteConnection connection = new SQLiteConnection(Cesta))
            {
                connection.InsertAll(Hodnota);
                Send<T>("Data has been inserted");
            }
        }

        /// <summary>
        /// Načte do listu všechny hodnoty v dané tabulce dle klasy
        /// </summary>
        /// <typeparam name="T">Typ klasy, který chceme načíst</typeparam>
        /// <returns></returns>
        public List<T> Load<T>() where T : new()
        {
            CreateTable<T>();
            using (SQLiteConnection connection = new SQLiteConnection(Cesta))
            {
                var mapping = connection.GetMapping<T>();
                var result = connection.Query<T>(String.Format("select * from {0}", mapping.TableName));
                Send<T>("Data has been loaded");
                return result;
            }
            
        }
        /// <summary>
        /// Odstrani řádek v tabulce dle jeho ID
        /// </summary>
        /// <typeparam name="T">Typ klasy, který chceme odstranit</typeparam>
        /// <param name="ID">Id objektu co chceme odstranit</param>
        /// <param name="Success">Vraci true pokud uspěl</param>
        public void RemoveByID<T>(int ID, out bool Success)
        {
            CreateTable<T>();
            using (SQLiteConnection connection = new SQLiteConnection(Cesta))
            {
                var x = connection.Delete<T>(ID);
                if (x == 0) Success = false; else Success = false;

                if (x != 0)
                    Send<T>("ID: " + ID + " has been removed from table");
                else
                    Send<T>("ID: " + ID + " has NOT been removed from table > could NOT find ID what are you looking for");
            }
        }
        /// <summary>
        /// Odstrani řádek v tabulce dle jeho ID
        /// </summary>
        /// <typeparam name="T">Typ klasy, který chceme odstranit</typeparam>
        /// <param name="ID">Id objektu co chceme odstranit</param>
        public void RemoveByID<T>(int ID)
        {
            CreateTable<T>();
            using (SQLiteConnection connection = new SQLiteConnection(Cesta))
            {
                var x = connection.Delete<T>(ID);
                if (x != 0)
                    Send<T>("ID: " + ID + " has been removed from table");
                else
                    Send<T>("ID: " + ID + " has NOT been removed from table > could NOT find ID what are you looking for");
            }
        }

        public void RemoveByObject<T>(T ObjectToRemove)
        {
            CreateTable<T>();
            using (SQLiteConnection connection = new SQLiteConnection(Cesta))
            {
                var x = connection.Delete<T>(ObjectToRemove);
                if (x != 0)
                    Send<T>("Object: " + ObjectToRemove + " has been removed from table");
                else
                    Send<T>("Object: " + ObjectToRemove + " has NOT been removed from table > could NOT find ID what are you looking for");
            }
        }

        public void UpdateByObject<T>(T ObjectToUpdate)
        {
            CreateTable<T>();
            using (SQLiteConnection connection = new SQLiteConnection(Cesta))
            {
                connection.Update(ObjectToUpdate);
            }
        }

        /// <summary>
        /// Vytvoří kopii database
        /// </summary>
        /// <param name="BackUpDataBasePath">Je netuné, aby na konci byla přípona .db </param>
        public void CreateBackupDatabase(string BackUpDataBasePath)
        {
            using (SQLiteConnection connection = new SQLiteConnection(Cesta))
            {
                connection.Backup(BackUpDataBasePath);
                Send("Backup created on: " + BackUpDataBasePath);
            }
        }
        /// <summary>
        /// Vytovří prazdnou tabulky dle objektu v T
        /// NENÍ nutné vyvolávat, protože každý přikaz je ochráněn vůči neexistujicí tabulce
        /// Tento příkaz nemaže tabulku, pouze vytváří a pokud je již vytvořená nic nedělá
        /// </summary>
        /// <typeparam name="T">Objekt podle, kterého se bude vytvářet tablulka</typeparam>
        public void CreateTable<T>()
        {
            if (!File.Exists(Cesta)) Send<T>("Database has not been created yet, creating now...");
            using (SQLiteConnection connection = new SQLiteConnection(Cesta))
            {
                connection.CreateTable<T>();
            }
        }
        
        
        /// <summary>
        /// Načte / vytvoří databazi
        /// </summary>
        /// <param name="_DatabaseName"> NENÍ nutné přidávat koncovku .db </param>
        public DatabaseHandler(string _DatabaseName) 
        {
            this.DatabaseName = _DatabaseName + ".db";
            Cesta = Path.Combine(Environment.CurrentDirectory, DatabaseName);
        }

        private void Send<T>(string message)
        {
            if(CreateDebugMessages)
            Debug.WriteLine($"SQlite - DatabaseHandler - {DatabaseName}<{typeof(T).Name}>:\t" + message);
        }
        private void Send(string message)
        {
            if (CreateDebugMessages)
                Debug.WriteLine($"SQlite - DatabaseHandler - {DatabaseName}:\t" + message);
        }


    }
}
