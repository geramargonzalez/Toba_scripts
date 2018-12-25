using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour {

	public void LoadScene(string sceneName){
        SceneManager.LoadScene (sceneName);
	}

	public void CurrenteScene(){
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
	}

    //Setea el juego al comienzo
	public void CurrenteSceneParaComenzarDeNuevo(){
		DataCtrl.instance.ResetData();
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
	}

    public void LoadSceneForPausa(string sceneName)
    {
        //SistemaDejuego.instance.MenuForPausa();
        SceneManager.LoadScene(sceneName);
    }


    public void Salir()
    {
        Application.Quit();
        
    }




    public void jugarDeNuevo(string sceneName){

        GameData data = DataCtrl.instance.data;

        int nivelActual = 0;
       
        int.TryParse(sceneName, out nivelActual);

        data.nivel = nivelActual;

        SceneManager.LoadScene(data.nivel.ToString());
       
    }

  
   

}
