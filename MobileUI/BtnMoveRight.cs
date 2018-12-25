using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnMoveRight : MonoBehaviour {

	MobileCtrlUI menuCtrl;


	// Use this for initialization
	void Start () {
		menuCtrl = GameObject.Find ("MobileControllersUI").GetComponent<MobileCtrlUI> ();

	}

	// Update is called once per frame
	void Update () {

	}

	/*void OnMouseOver(){

		if (Input.GetMouseButtonDown (0)) {
			menuCtrl.MoveRight ();
		}

	}*/

    public void MouseClick()
    {

        menuCtrl.MoveRight();

    }

	public void OnMouse(){
		menuCtrl.Stop();
	}

}
