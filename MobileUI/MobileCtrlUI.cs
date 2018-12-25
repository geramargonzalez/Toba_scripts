using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MobileCtrlUI : MonoBehaviour {

	
	public GameObject player;


	PlayerControllerAdvanced plScript;
	

	void Start () {

		player = GameObject.Find ("DogAdvanced");
        plScript = player.GetComponent<PlayerControllerAdvanced>();
		

	}


	public void MoveRight(){
		

			plScript.MoveRight ();

		
	}
	public void MoveLeft(){
		

			plScript.MoveLeft ();

		
	}
	public void StopFlyJump(){

        plScript.StopJump();
	}


	public void MobileJump(){
        plScript.MobileJump();
	}

    public void MobileFly()
    {
        plScript.FlyMobile();
    }

    public void Stop(){

        plScript.Stop();
    }
}
