using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour {


	Transform objetivoCamara;
	bool agreY;
	float tmp;
	float yPos;


	public float yOff;

	void Start(){

		objetivoCamara = GameObject.Find ("DogAdvanced").transform;

        // Sacarle los comentarios 
        if(DataCtrl.instance.data.nivel >= 6 || DataCtrl.instance.data.nivel == 2){

            if (SistemaDejuego.instance.posicionXActual < 3283.7)
            {

                agreY = false;

            }
            else
            {

                agreY = true;

            }
        
        } else {
        
            agreY = false;
        
        }

		yPos = this.transform.position.y;

       
	}

	// Update is called once per frame
	void Update () {

		if (!agreY) {
		
			transform.position = new Vector3 (objetivoCamara.position.x, yPos, transform.position.z);
		
		} else {

			transform.position = new Vector3 (objetivoCamara.position.x, objetivoCamara.transform.position.y + yOff, transform.position.z);
	

		}
	
	}
		

	public void setearYCamera(){
	
		agreY = true;
	
	}

	public void QuitarYCamera(){

		agreY = false;

	}

}
