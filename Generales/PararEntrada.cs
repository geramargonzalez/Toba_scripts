using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PararEntrada : MonoBehaviour {
    

    Animator anim;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        anim.SetBool("entrar", true);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PararAnimator(){
        anim.SetBool("entrar", false);
    }

}
