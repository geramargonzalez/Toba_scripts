using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuertePorCaida : MonoBehaviour {


	public Transform volver;
	GameObject camera;
	MainCamera mainCamera;
    bool soloUnavez;

    //GameObject dog;


	void Start () {
		
		camera = GameObject.Find("Main Camera");
		mainCamera = camera.GetComponent<MainCamera> ();
        soloUnavez = false;


	}


	void OnTriggerEnter2D(Collider2D other) {
		
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Animales") {

            if(!soloUnavez){
                mainCamera.QuitarYCamera();
                SistemaDejuego.instance.PlayerDiesCorrutine(other.gameObject);
                soloUnavez = true;
            }
			
           
		}


	}
}


