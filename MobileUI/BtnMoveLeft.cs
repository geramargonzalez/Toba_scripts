using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnMoveLeft : MonoBehaviour {

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

			menuCtrl.MoveLeft();
		}

	}

	*/

    public void MouseClick()
    {
       
      menuCtrl.MoveLeft();

    }



    public void OnMouse()
    {
        menuCtrl.Stop();
    }

}
