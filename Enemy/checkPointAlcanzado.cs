using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkPointAlcanzado : MonoBehaviour {


	Transform position;
    BoxCollider2D bg;

	void Start(){

		position = this.transform;
        bg = GetComponent<BoxCollider2D>();
	
	}


	void OnTriggerEnter2D(Collider2D other) {

		if(other.gameObject.CompareTag("Player")){

            StartCoroutine(desactivarBox(other));

		}
	}

    IEnumerator desactivarBox(Collider2D other){
        bg.enabled = false;
        AudioCtrl.instance.CheckPoint(other.gameObject.transform);
        SistemaDejuego.instance.CheckPointReached(position);
        SistemaDejuego.instance.checkPointTXT();
        yield return new WaitForSeconds(4.0f);
        bg.enabled = true;
    }

}
