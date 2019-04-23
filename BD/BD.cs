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
    string strConexion;
    IDbConnection dbconn;
   
    string tobaBd = "tobaBd.db";
    public Nivel[] niveles;

    // Use this for initialization
    public void inicializar()
    {
        niveles = new Nivel[9];
        Connection();
        ObtenerNiveles();

    }


    public void Connection()
    {
        // Crear y abrir la conexion
        //Compruebo que plataforma es
        // Si es PC mantengo la ruta
        if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor)
        {
            conn = Application.dataPath + "/StreamingAssets/" + tobaBd;
        }
        else if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            conn = Application.dataPath + "/Raw/" + tobaBd;
        }
        // Si es android
        else if (Application.platform == RuntimePlatform.Android)
        {
            conn = Application.persistentDataPath + "/" + tobaBd;

            // Comprobar si el archivo se encuantra almacenado en persistant data
            if (!File.Exists(conn))
            {
                WWW loadDB = new WWW("jar:file://" + Application.dataPath + "!/assets/" + tobaBd);
                while (!loadDB.isDone)
                { }
                File.WriteAllBytes(conn, loadDB.bytes);
            }
        }

        strConexion = "URI=file:" + conn;
        dbconn = (IDbConnection)new SqliteConnection(strConexion);

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
            int puntosPorNivel = reader.GetInt32(6);
            int cantidadvejecesjugadas = reader.GetInt32(7);
            //Debug.Log(cantidadvejecesjugadas);
            int fallosMultiplicacion = reader.GetInt32(8);
            int fallosSuma = reader.GetInt32(9);
            int fallosResta = reader.GetInt32(10);
            int fallosDivision = reader.GetInt32(11);
            int aciertosMultiplicacion = reader.GetInt32(12);
            int aciertosSuma = reader.GetInt32(13);
            int aciertosResta = reader.GetInt32(14);
            int aciertosDivision = reader.GetInt32(15);
            int ultimoEnemigo = reader.GetInt32(16);

            Nivel level = new Nivel(nivel,unlocked,bonesstars,promedio,fallosPorNivel,aciertosPorNivel,puntosPorNivel,cantidadvejecesjugadas,
                                    fallosMultiplicacion,fallosSuma,fallosResta,fallosDivision,aciertosMultiplicacion,aciertosSuma,aciertosResta,aciertosDivision,ultimoEnemigo);
            
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

        string sqlQuery = "Update Level set unLocked = " + flag + ", boneStars = " + nivel.bonesStars +
                          ", promedio = " + nivel.promedio + ", fallosPorNivel = " + nivel.fallosPorNivel +
                          ", aciertosPorNivel = " + nivel.aciertosPorNivel + ", puntosPorNivel = "
                          + nivel.puntosPorNivel + ", cantidadVecesJugadas = " + nivel.cantVecesJugadas;

        sqlQuery += ", fallosMultiplicacion = " + nivel.fallosMultiplicacion + ", fallosSuma = " + nivel.fallosSuma + ", fallosResta = " + nivel.fallosResta +
                                                       ", fallosDivision = " + nivel.fallosDivision;

        sqlQuery += ", aciertosMultiplicacion = " + nivel.aciertosMultiplicacion + ", aciertosSuma = " + nivel.aciertosSuma + ", aciertosResta = " + nivel.aciertosResta +
                    ", aciertosDivision = " + nivel.aciertosDivision +", ultimoEnemy = " + nivel.ultimoEnemigo +" where nivel = " + nivel.nivel;

        dbcmd.CommandText = sqlQuery;
        dbcmd.ExecuteNonQuery();
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
    }

    public void EditarEstadoNivel(Nivel nivel, int num)
    {
        // Create a conexion
        Connection();
        //Open connection to the database.
        dbconn.Open();
        //Crear los comando a la base de datos
        IDbCommand dbcmd = dbconn.CreateCommand();

        string sqlQuery = "Update Level set status = " + num +
         " where nivel = " + nivel.nivel;

        dbcmd.CommandText = sqlQuery;
        dbcmd.ExecuteNonQuery();
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
    }


    public void guardarFallosYAciertos(Nivel nivel)
    {

        // Create a conexion
        Connection();
        //Open connection to the database.
        dbconn.Open();
        //Crear los comando a la base de datos
        IDbCommand dbcmd = dbconn.CreateCommand();
        string sqlQuery = "Update Level set fallosMultiplicacion = " + nivel.fallosMultiplicacion + ", fallosSuma = " + nivel.fallosSuma + ", fallosResta = " + nivel.fallosResta +
                                                       ", fallosDivision = " + nivel.fallosDivision;

        sqlQuery += ", aciertosMultiplicacion = " + nivel.aciertosMultiplicacion + ", aciertosSuma = " + nivel.aciertosSuma + ", aciertosResta = " + nivel.aciertosResta +
                     ", aciertosDivision = " + nivel.aciertosDivision + " where nivel = " + nivel.nivel;
        
        dbcmd.CommandText = sqlQuery;
        dbcmd.ExecuteNonQuery();
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
    }

    public void GuardarUltimaPosicionEnemigo(Nivel nivel,int num)
    {
        // Create a conexion
        Connection();
        //Open connection to the database.
        dbconn.Open();
        //Crear los comando a la base de datos
        IDbCommand dbcmd = dbconn.CreateCommand();

        string sqlQuery = "Update Level set ultimoEnemy = " + num +
         " where nivel = " + nivel.nivel;

        dbcmd.CommandText = sqlQuery;
        dbcmd.ExecuteNonQuery();
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
    }

}
