using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btnJump : MonoBehaviour {

	MobileCtrlUI menuCtrl;


	// Use this for initialization
	void Start () {
		menuCtrl = GameObject.Find ("MobileControllersUI").GetComponent<MobileCtrlUI> ();

	}

	// Update is called once per frame
	void Update () {

	}



    public void MouseClick(){
       
            menuCtrl.MobileJump();


    }


    public void OnMouseRealese()
    {
        menuCtrl.StopFlyJump();
    }
   

   


}
