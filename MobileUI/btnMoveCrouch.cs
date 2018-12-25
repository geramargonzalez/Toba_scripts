using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btnMoveCrouch : MonoBehaviour {

	MobileCtrlUI menuCtrl;


	void Start () {
		menuCtrl = GameObject.Find ("MobileControllersUI").GetComponent<MobileCtrlUI> ();


	}

	// Update is called once per frame
	void Update () {

	}

	void OnMouseOver(){

		if (Input.GetMouseButtonDown (0)) {

			//menuCtrl.MobileCrouch();

		}

	}

	void OnMouseUp(){
		menuCtrl.Stop();
	}

}
