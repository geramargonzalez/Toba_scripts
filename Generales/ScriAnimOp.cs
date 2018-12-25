using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriAnimOp : MonoBehaviour {

    Animator animator;

	// Use this for initialization
	void Start () {
       
        animator = GetComponent<Animator>();
	
    }
	
	// Update is called once per frame
	void Update () {
		
	    
    
    }

    public void Entrada(){
        animator.SetBool("entrar", false);
    }
}
