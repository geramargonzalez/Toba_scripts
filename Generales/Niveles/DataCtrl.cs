using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;



public class DataCtrl : MonoBehaviour {


	public static DataCtrl instance = null;
	public GameData data;
	public bool devMode;
	string dataFilePath;	
	BinaryFormatter bf;

    public BD dataBase;

	void Awake(){

		if (instance == null) {

			instance = this;
			DontDestroyOnLoad (gameObject);

		} 


		bf = new BinaryFormatter ();
		dataFilePath = Application.persistentDataPath + "/game.dat";

      
	}


    private void Start()
    {
        dataBase = new BD();
        dataBase.inicializar();
        data.niveles = dataBase.niveles;

    }



    public void RefreshData(){

		if(File.Exists(dataFilePath)){

			FileStream fs = new FileStream (dataFilePath, FileMode.Open);

			data = (GameData)bf.Deserialize (fs);

			fs.Close ();

		}
       
	}


	void OnEnable(){
        
        CheckDB();
	
	}

	void OnDisable(){
      
        SaveData();
	}


	public void SaveData (){

        //Guardo los niveles en la base de datos
        SalvarNiveles(data.niveles);
       
		FileStream fs = new FileStream (dataFilePath, FileMode.Create);

		bf.Serialize (fs, data);

		fs.Close (); 	
	
	}





	// Copiar para la batallas de las tablas
	public void SaveData(GameData pdata){

        //Guardo los niveles en la base de datos
        SalvarNiveles(pdata.niveles);
       

		FileStream fs = new FileStream (dataFilePath, FileMode.Create);

        bf.Serialize (fs, pdata);

		fs.Close (); 	

	}

    // Guarda solo la GameData
    public void SaveDataNoBD(GameData pdata)
    {

        FileStream fs = new FileStream(dataFilePath, FileMode.Create);

        bf.Serialize(fs, pdata);

        fs.Close();

    }


	public bool IsUnlocked(int levelNumber){

        bool level = data.niveles[levelNumber].unlocked;
       // SaveData();
        return level;
	}


	public int GetStars(int levelNumber){

		return data.niveles[levelNumber].bonesStars;
	}

	public void CheckDB(){

		if (!File.Exists (dataFilePath)) {

            #if UNITY_ANDROID

				CopyDB();

			#endif

		} else {

			//Pregunta si es una apliacion de escritorio
			if (SystemInfo.deviceType == DeviceType.Desktop) {

				string dstFile = Path.Combine(Application.streamingAssetsPath, "game.dat");
				File.Delete (dstFile);
				File.Copy (dataFilePath,dstFile);

			}

            if(devMode){

                //Pregunta si es una aplicacion movil
                //Debug.Log(" Entro al modo dev");
                if (SystemInfo.deviceType == DeviceType.Handheld) {
                    File.Delete (dataFilePath);
                    CopyDB ();

                } 
            }

            RefreshData();

		}
	}

	public void CopyDB(){

		string srcFile = System.IO.Path.Combine(Application.streamingAssetsPath, "game.dat");

		WWW downloader = new WWW(srcFile);

		while(!downloader.isDone){

		}

		// Then save to Aplication.
		File.WriteAllBytes(dataFilePath,downloader.bytes);
		RefreshData();
	}


    //Reseteo a 0 el juego.
	public void ResetData(){

		FileStream fs = new FileStream (dataFilePath, FileMode.Create);

        ResetAllLevel();

		bf.Serialize (fs, data);

		fs.Close ();	

	}



	public void SetearNumeroNivel(){

        SetearNivelACtual();
	
	}



    public void SalvarNiveles(Nivel[] niveles)
    {

        dataBase.GuardarNiveles(niveles);
        

    }


    // ***  METODOS *** //
    public void GuardarPosicionInicial()
    {
        data.x = 3.3f;
        data.y = -26.0f;
        data.z = 0f;
    }


    // Setea el tiempo de cada Pantalla
    public float ResetTime()
    {
        
        if (data.nivel == 0)
        {

            data.tiempoActual = 600;


        }
        else if (data.nivel == 1)
        {

            data.tiempoActual = 500;


        }
        else if (data.nivel == 2)
        {

            data.tiempoActual = 450;


        }
        else if (data.nivel >= 4)
        {

            data.tiempoActual = 400;


        }


        return data.tiempoActual;

    }

    // Sube el nivel del juego.
    public void subirNivel()
    {
       

        //Compara el nivel alcanzado con el mayor alcanzado, ya que este ultimo es el que define el mayor alcanzado. 
        //Tiene que ser nivelMaximo+1 para que suba el nivel
        if((data.nivel + 1) > data.nivelMaximo){

            SetearNivelACtual();
            data.nivel++;
            data.nivelMaximo = data.nivel;
            data.yaJugo = false;
            data.niveles[data.nivelMaximo].unlocked = true;
       
            // Si no es mayor deberia comparar los valores anteriores y si son mejores, actualizarlos.
        } else {
            
        
        }

        //Imprimo todo en la base de datos
        SaveData(data);

    }


    //Seteo las estadisticas del nivel
    public void SetearNivelACtual()
    {
        
        data.niveles[data.nivelMaximo].promedio = data.niveles[data.nivelMaximo].promedio / cantidadEnemigoPorNivel();
        data.niveles[data.nivelMaximo].fallosPorNivel = data.fallos;

        if (data.nivel == 3)
        {

            data.niveles[data.nivelMaximo].aciertosPorNivel = 8;
        }

        if (data.nivel == 5)
        {
            data.niveles[data.nivelMaximo].aciertosPorNivel = 12;
        }


    }

    // Seteo la cantidad de niveles.
     void SetearNumeroDeNiveles()
    {

        for (int i = 0; i < data.niveles.Length; i++)
        {

            data.niveles[i].nivel = i;

        }

    }

    //Desbloqueo la cantidad de niveles
    public void UnLockedNivel()
    {

        for (int i = 0; i < data.niveles.Length; i++)
        {

            if (data.niveles[i].nivel <= data.nivel)
            {

                data.niveles[i].unlocked = true;

                data.niveles[i].aciertosPorNivel = 0;

                data.niveles[i].fallosPorNivel = 0;

                data.niveles[i].bonesStars = 0;

                data.niveles[i].fallosMultiplicacion = 0;

                data.niveles[i].fallosSuma = 0;

                data.niveles[i].fallosResta = 0;

                data.niveles[i].fallosDivision = 0;

                data.niveles[i].aciertosMultiplicacion = 0;

                data.niveles[i].aciertosSuma = 0;

                data.niveles[i].aciertosResta = 0;

                data.niveles[i].aciertosDivision = 0;

                data.niveles[i].promedio = 0;


            }
            else if (data.niveles[i].nivel > data.nivel)
            {

                data.niveles[i].unlocked = false;

                data.niveles[i].bonesStars = 0;

                data.niveles[i].aciertosPorNivel = 0;

                data.niveles[i].fallosPorNivel = 0;

                data.niveles[i].fallosMultiplicacion = 0;

                data.niveles[i].fallosSuma = 0;

                data.niveles[i].fallosResta = 0;

                data.niveles[i].fallosDivision = 0;

                data.niveles[i].aciertosMultiplicacion = 0;

                data.niveles[i].aciertosSuma = 0;

                data.niveles[i].aciertosResta = 0;

                data.niveles[i].aciertosDivision = 0;

                data.niveles[i].promedio = 0;

            }


        }

        dataBase.GuardarNiveles(data.niveles);
    }



        public int NivelLogrado()
    {
        
        return data.nivel;

    }




    // Reseteo todos los niveles.
    public void ResetAllLevel()
    {

        data.nivel = 0;

        data.nivelMaximo = 0;

        data.vidas = 5;

        data.bones = 0;

        data.puntos = 0;

        data.fallos = 0;

        UnLockedNivel();

        data.tiempoActual = ResetTime();

        data.yaJugo = false;

        data.posActualEnemigo = 0;


    }

    //Reseteo el nivel por Perder
    public void ResetLevelGameOver(GameData gdata)
    {

        if(gdata.nivel <= gdata.nivelMaximo){

            gdata.vidas = 5;

            gdata.bones = 0;

            gdata.puntos = 0;

            gdata.fallos = 0;

            gdata.tiempoActual = ResetTime();

            gdata.yaJugo = false;

            gdata.posActualEnemigo = 0; 
        }


        SaveDataNoBD(gdata);


    }


    public int cantidadEnemigoPorNivel()
    {

        int cantEnem = 0;


        if (data.nivel == 0)
        {

            cantEnem = 8;
           


        }
        if (data.nivel == 1)
        {

            cantEnem = 7;

        }
        if (data.nivel == 2)
        {

            cantEnem = 9;

        }

        if (data.nivel == 3)
        {

            cantEnem = 8;
           

        }


        if (data.nivel == 4)
        {

            cantEnem = 13;
           

        }
        if (data.nivel == 5)
        {

            cantEnem = 12;


        }
        if (data.nivel == 6)
        {

            cantEnem = 14;
           

        }
        if (data.nivel == 7)
        {

            cantEnem = 16;


        }

        data.cantidadTrolls = cantEnem;

        return cantEnem;

    }

    public int cantidadOrquitosPorNivel()
    {

        int cantEnem = 0;


        if (data.nivel == 0)
        {

            cantEnem = 10;


        }
        if (data.nivel == 1)
        {

            cantEnem = 21;

        }
        if (data.nivel == 2)
        {

            cantEnem = 21;

        }

        if (data.nivel == 4)
        {

            cantEnem = 21;

        }

        if (data.nivel == 6)
        {

            cantEnem = 15;

        }
        if (data.nivel == 7)
        {

            cantEnem = 22;

        }

        return cantEnem;
    }


    public void PuntosPorStars(int stars)
    {

        if (stars == 1)
        {

            data.puntos += 2500;

        }
        else if (stars == 2)
        {

            data.puntos += 5000;

        }
        else if (stars == 3)
        {

            data.puntos += 10000;

        }

    }


    public int SetStarsAwarded(int levelNumber, int stars)
    {

        return data.niveles[levelNumber].bonesStars = stars;

    }

    public void Unlocked(int levelNumber)
    {

        data.niveles[levelNumber].unlocked = true;
    }
  


}
