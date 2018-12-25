using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointBola : MonoBehaviour {

	private LanzarBola bolaController;
    public int pos;

	void Start(){
		
        bolaController = GameObject.Find ("GObjGeneradorBola").GetComponent<LanzarBola>();
	}

	void OnTriggerEnter2D(Collider2D coll) {
		
        if (coll.gameObject.tag == "Player") {

            this.gameObject.SetActive(false);
            bolaController.Lanzar(pos);
		}
	}


}
