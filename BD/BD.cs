using System.Collections;
using System.Collections.Generic;
using Mono.Data.Sqlite;
using System.Data;
using System;
using UnityEngine;
using System.IO;

public class BD 
{

    string conn;
    IDbConnection dbconn;
    public Nivel[] niveles;



    // Use this for initialization
    public void inicializar()
    {
        niveles = new Nivel[9];
        Connection();
        ObtenerNiveles();

    }


    public void ObtenerNiveles()
    {

        //Open connection to the database.
        dbconn.Open();

        //Crear los comando a la base de datos
        IDbCommand dbcmd = dbconn.CreateCommand();
        string sqlQuery = "SELECT * FROM Level";
        dbcmd.CommandText = sqlQuery;

        // Lector, donde se va a guardar la informacion obtenida.
        IDataReader reader = dbcmd.ExecuteReader();
        int cont = 0;


        //Leemos la informacion obtenida
        while (reader.Read())
        {
            int nivel = reader.GetInt32(0);

            int tmpUnlocked = reader.GetInt32(1);

            bool unlocked;

            if(tmpUnlocked == 1){

                unlocked = true;
            
            } else {
               
                unlocked = false;
            
            }

            int bonesstars = reader.GetInt32(2);

            int promedio = reader.GetInt32(3);

            int fallosPorNivel = reader.GetInt32(4);

            int aciertosPorNivel = reader.GetInt32(5);

            Nivel level = new Nivel(nivel,unlocked,bonesstars,promedio,fallosPorNivel,aciertosPorNivel);
           
            if(cont <= 9){
                
               niveles[cont] = level;
                cont++;  
            
            }

        }


        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
    }


    public void Connection()
    {

        // conn = "URI=file:" + Application.dataPath + "/Myassets/Plugins/tobaBd.db";
		

         if (Application.platform != RuntimePlatform.Android)
        {

            conn = "URI=file:" + Application.dataPath + "/Myassets/Plugins/tobaBd.db";
        }
        else
        {

            conn = Application.persistentDataPath + "/Myassets/Plugins/tobaBd.db";

            if (!File.Exists(conn))
            {
                WWW load = new WWW("jar:file://" + Application.dataPath + "!/assets/" + "tobaBd.db");

                while (!load.isDone) { }

                File.WriteAllBytes(conn, load.bytes);
            }
        }


        dbconn = (IDbConnection)new SqliteConnection(conn);

    }

    public void UpdateLevel(){
        
    }

      public void GuardarNiveles(Nivel[] niveles){

        foreach(Nivel nivel in niveles){

            GuardarNivel(nivel);
        }

    }

   public void GuardarNivel(Nivel nivel){

         // Create a conexion
         Connection();

        //Open connection to the database.
        dbconn.Open();

        //Crear los comando a la base de datos
        IDbCommand dbcmd = dbconn.CreateCommand();

        int flag = 0;

        if(nivel.unlocked){
            flag = 1;
        }


        string sqlQuery = "Update Level set unLocked = " + flag + ",boneStars = " + nivel.bonesStars +
                                                              ", promedio = " + nivel.promedio + ", fallosPorNivel = " + nivel.fallosPorNivel + 
                                                              ", aciertosPorNivel = " + nivel.aciertosPorNivel + " where nivel = " + nivel.nivel;
        dbcmd.CommandText = sqlQuery;
        dbcmd.ExecuteNonQuery();
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
    }








}
